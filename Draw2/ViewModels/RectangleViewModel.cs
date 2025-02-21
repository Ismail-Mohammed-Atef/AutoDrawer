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
    internal class RectangleViewModel : ObservableObject,ICanCloseWindow
    {
        public event EventHandler RequestClose;
        public MyRectangle rectangle { get; }

        public RectangleViewModel(MyRectangle rectangle)
        {
            this.rectangle = rectangle;
            this.IsCanceled = true;
            this.AddRectangleCommand = new RelayCommand(this.AddRectangle);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }


        private void Cancel()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void AddRectangle()
        {
            this.IsCanceled = false;
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public bool IsCanceled { get; set; }
        public ICommand AddRectangleCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
