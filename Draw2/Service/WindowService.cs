using Draw2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Draw2.Service
{
    internal class WindowService
    {

        public void ShowDialog(object viewModel)
        {
            if (viewModel is CircleViewModel)
            {
                var window = new Views.AddCircleView();
                window.DataContext = viewModel;

                window.ShowDialog();
            }
            else if (viewModel is SquareViewModel)
            {
                var window = new Views.AddSquareView();
                window.DataContext = viewModel;

                window.ShowDialog();
            }
            else if (viewModel is RectangleViewModel)
            {
                var window = new Views.AddRectangleView();
                window.DataContext = viewModel;

                window.ShowDialog();
            }
            else if (viewModel is PolygonModelView)
            {
                var window = new Views.AddPolygonView();
                window.DataContext = viewModel;

                window.ShowDialog();
            }
            else if (viewModel is LoginViewModel)
            {
                var window = new Views.LoginView();
                window.DataContext = viewModel;

                Application.Current.Windows[0]?.Close();
                window.ShowDialog();
            }
            else if (viewModel is RegisterViewModel)
            {
                var window = new Views.RegisterView();
                window.DataContext = viewModel;

                Application.Current.Windows[0]?.Close();
                window.ShowDialog();
            }
            else if (viewModel is UserMainPageViewModel)
            {
                var window = new Views.UserMainPageView();
                window.DataContext = viewModel;

                Application.Current.Windows[0]?.Close();
                window.ShowDialog();
            }
            else if (viewModel is MainTabViewModel)
            {
                var window = new Views.MainTabView();
                window.DataContext = viewModel;

                Application.Current.Windows[0]?.Close();
                window.ShowDialog();
            }
            else if (viewModel is MainViewModel)
            {
                var window = new Views.MainView();
                window.DataContext = viewModel;

                window.ShowDialog();
                Application.Current.Windows[0]?.Close();
            }
        }
    }

}

