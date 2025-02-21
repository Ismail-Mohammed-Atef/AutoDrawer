using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Draw2.Entities;
using Draw2.Service;
using Draw2.Views;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.ImConjugate;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Draw2.ViewModels
{
    public class MainTabViewModel :ObservableObject
    {
        public CadDbContext context { get; set; } = new CadDbContext();
        public int Id { get; set; }
        public MainTabViewModel(int id)
        {
            Id = id;
            Projects = new ObservableCollection<Project>(context.Projects.Where(p => p.userId == id).ToList());
            RemoveProjectCommand = new RelayCommand<Project>(this.Remove);
            OpenProjectCommand = new RelayCommand<Project>((Project project)=> SendData(project));
            WeakReferenceMessenger.Default.Register<MessageService2>(this, (r, m) =>
            {
                ReceivedData = m.Value;
                Projects = new ObservableCollection<Project>(context.Projects.Where(p => p.userId == id).ToList());
            });
        }

        private void Remove(Project project)
        {
            context.Projects.Remove(project);
            context.SaveChanges();
            Projects.Remove(project);
        }

        public void SendData(Project data)
        {
            WeakReferenceMessenger.Default.Send(new MessageService(data));
            System.Diagnostics.Debug.WriteLine($"Message Sent:");
        }

        private ObservableCollection<Project> _Projects;
        public ObservableCollection<Project> Projects
        {
            get => _Projects;
            set => SetProperty(ref _Projects, value);
        }
        public string Header { get; set; }
        public object Content { get; set; }
        public ICommand OpenProjectCommand { get; set; }
        public ICommand RemoveProjectCommand { get; set; }
        
        private Project _receivedData;
        public Project ReceivedData
        {
            get => _receivedData;
            set => SetProperty(ref _receivedData, value);
        }
    }
}
