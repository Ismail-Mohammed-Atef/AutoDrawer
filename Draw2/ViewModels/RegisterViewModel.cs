using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Draw2.Entities;
using Draw2.Models;
using Draw2.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Draw2.ViewModels
{
    public class RegisterViewModel : ObservableObject,ICanCloseWindow
    {
        public CadDbContext context { get; set; } = new CadDbContext();
        public RegisterViewModel()
        {
            AddUserCommand = new RelayCommand(this.AddUser);
            BackToLoginCommand = new RelayCommand(this.GoToLogin);
            User = new AppUser();
        }

        private void GoToLogin()
        {
            WindowService windowService = new WindowService();
            LoginViewModel viewModel = new LoginViewModel();
            windowService.ShowDialog(viewModel);
        }

        private void AddUser()
        {
            var user = context.AppUsers.FirstOrDefault(a=>a.Username == User.Username);

            if (user == null)
            {
                context.AppUsers.Add(User);
                context.SaveChanges();
                MessageBox.Show("User Added successfully");
                var windowService = new WindowService();
                var vm = new LoginViewModel();
                var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                windowService.ShowDialog(vm);

                System.Windows.Application.Current.Dispatcher.InvokeAsync(() => currentWindow?.Close(), System.Windows.Threading.DispatcherPriority.Background);

            }
            else
            {
                MessageBox.Show("User already exist");
            }
        }

        private ObservableCollection<AppUser> _users;
        public ObservableCollection<AppUser> Users
        {
            get => _users;
            set => SetProperty(ref _users, value);
        }
        private AppUser _user;

        public event EventHandler RequestClose;

        public AppUser User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public ICommand AddUserCommand { get; set; }
        public ICommand BackToLoginCommand { get; set; }
    }
}
