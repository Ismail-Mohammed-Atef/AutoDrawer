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
    internal class SquareViewModel :ObservableObject,ICanCloseWindow
    {
        public event EventHandler RequestClose;
        public MySquare square { get; }

        public SquareViewModel(MySquare square)
        {
            this.square = square;
            this.IsCanceled = true;
            this.AddSquareCommand = new RelayCommand(this.AddSquare);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }


        private void Cancel()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void AddSquare()
        {
            this.IsCanceled = false;
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        
        public bool IsCanceled { get; set; }
        public ICommand AddSquareCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
