using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Draw2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Draw2.ViewModels
{
    internal class PolygonModelView : ObservableObject, ICanCloseWindow
    {
        public event EventHandler RequestClose;
        public MyPolygon polygon { get; }

        public PolygonModelView(MyPolygon polygon)
        {
            this.polygon = polygon;
            this.IsCanceled = true;
            this.AddPolygonCommand = new RelayCommand(this.AddPolygon);
            this.CancelCommand = new RelayCommand(this.Cancel);
        }


        private void Cancel()
        {
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void AddPolygon()
        {
            this.IsCanceled = false;
            this.RequestClose?.Invoke(this, EventArgs.Empty);
        }

        public bool IsCanceled { get; set; }
        public ICommand AddPolygonCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
