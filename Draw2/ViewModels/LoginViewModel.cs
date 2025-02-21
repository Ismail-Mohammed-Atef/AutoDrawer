using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Draw2.Entities;
using Draw2.Service;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Draw2.ViewModels
{
    public class LoginViewModel : ObservableObject, ICanCloseWindow
    {
        public CadDbContext context { get; set; } = new CadDbContext();
        public int Id { get; set; }
        public LoginViewModel()
        {
            CheckUserCommand = new RelayCommand(this.check);
            GoToRegisterCommand = new RelayCommand(this.GoToRegister);
            User = new AppUser();
        }

        private void GoToRegister()
        {
            var windowService = new WindowService();
            var vm = new RegisterViewModel();
            var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

            windowService.ShowDialog(vm);

            System.Windows.Application.Current.Dispatcher.InvokeAsync(() => currentWindow?.Close(), System.Windows.Threading.DispatcherPriority.Background);
        }

        private void check()
        {
            var user = context.AppUsers.FirstOrDefault(a => a.Username == User.Username);
            if (user == null)
            {
                MessageBox.Show("No user with given username");
            }
            else
            {
                if (user.Password == User.Password)
                {
                    var window = new WindowService();
                    var id = (context.AppUsers.FirstOrDefault(a => a.Username == User.Username)).Id;
                    var userpagevm = new UserMainPageViewModel(id);
                    var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

                    window.ShowDialog(userpagevm);

                    System.Windows.Application.Current.Dispatcher.InvokeAsync(() => currentWindow?.Close(), System.Windows.Threading.DispatcherPriority.Background);
                }
            }
        }
        private AppUser _user;

        public event EventHandler RequestClose;

        public AppUser User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }
        public ICommand CheckUserCommand { get; set; }
        public ICommand GoToRegisterCommand { get; set; }
    }
}
