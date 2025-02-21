using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Draw2.Entities;
using Draw2.Models;
using Draw2.Service;
using Draw2.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Draw2.ViewModels
{
    public class UserMainPageViewModel : ObservableObject
    {

        public int Id { get; set; }
        public UserMainPageViewModel(int id)
        {
            Id = id;
            AddNewTabCommand = new RelayCommand(this.AddNewTab);
            CloseCommand = new RelayCommand<ObservableObject>(this.close);
            LogoutCommand = new RelayCommand(this.Logout);
            WeakReferenceMessenger.Default.Register<MessageService>(this, (r, m) =>
            {
                ReceivedData = m.Value;
                var vm = new MainViewModel()
                {
                    project = new Project() { userId = this.Id ,Name = ReceivedData.Name},
                    Header = ReceivedData.Name,
                    Content = new DrawView()
                };
                vm.LoadCommand.Execute(vm);
                SelectedTab = vm;
                ViewModels.Add(vm);
            });
            var MTVM = new MainTabViewModel(id)
            {
                Header = "Main tab",
                Content = new MainTabUserControl(),
            };

            ViewModels = new ObservableCollection<ObservableObject>
            {
               MTVM
            };
        }

        private void Logout()
        {
            WindowService windowService = new WindowService();
            LoginViewModel viewModel = new LoginViewModel();
            windowService.ShowDialog(viewModel);
        }

        private void close(ObservableObject tab)
        {
            if (SelectedTab != null && ViewModels.Contains(tab))
            {
                ViewModels.Remove(tab);
            }
        }

       
        public CadDbContext context { get; set; }
        public ICommand AddNewTabCommand { get; set; }
        public ICommand LogoutCommand { get; set; }


        private void AddNewTab()
        {
            // Create and add a new tab
            var newTab = new MainViewModel()
            {
                project = new Project() { Name = ProjectName , userId = Id},
                Header = ProjectName,
                Content = new DrawView() // Replace with your desired view
            };
            ViewModels.Add(newTab);

            // Optionally, set the new tab as selected
            SelectedTab = newTab;
        }
        private ObservableCollection<ObservableObject> _ViewModels;

        public ObservableCollection<ObservableObject> ViewModels
        {
            get => _ViewModels;
            set => SetProperty(ref _ViewModels, value);
        }
        public ICommand CloseCommand { get; }

        private Project _receivedData;
        public Project ReceivedData
        {
            get => _receivedData;
            set => SetProperty(ref _receivedData, value);
        }
        private ObservableObject _selectedTab;
        public ObservableObject SelectedTab
        {
            get => _selectedTab;
            set => SetProperty(ref _selectedTab, value);
        }

        public string ProjectName { get; set; }
    }
}
