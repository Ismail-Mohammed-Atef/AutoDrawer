﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Draw2.Views
{
    /// <summary>
    /// Interaction logic for AddPolygonView.xaml
    /// </summary>
    public partial class AddPolygonView : Window
    {
        public AddPolygonView()
        {
            InitializeComponent();
        }
        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext is ICanCloseWindow closeable)
            {
                closeable.RequestClose += (s, args) => this.Close();
            }
        }
    }
}
