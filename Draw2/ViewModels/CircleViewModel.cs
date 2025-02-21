using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Draw2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Draw2.ViewModels
{
    internal class CircleViewModel : ObservableObject, ICanCloseWindow
    {
        public event EventHandler RequestClose;
        public MyCircle circle { get; }

        public CircleViewModel(MyCircle circle)
        {
            this.circle = circle;
            this.IsCanceled = true;
            this.AddCircleCommand = new RelayCommand(this.AddCircle);
            this.HideCommand = new RelayCommand(this.HideDetails);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }

        private void HideDetails()
        {
            this.Hide = false;
        }
        private void Cancel()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void AddCircle()
        {
            this.IsCanceled = false;
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }
        private bool _Hide;
        public bool Hide
        {
            get => _Hide;
            set => SetProperty(ref _Hide, value);
        }
        public bool IsCanceled { get; set; }
        public ICommand AddCircleCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand HideCommand { get; }

    }
}
