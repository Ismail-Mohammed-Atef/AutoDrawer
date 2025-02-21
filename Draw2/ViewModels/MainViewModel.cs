using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Draw2.Models;
using Draw2.Service;
using Draw2.Views;
using Draw2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;
using Microsoft.Graph.Models;
using Draw2.Entities;
using CommunityToolkit.Mvvm.Messaging;

namespace Draw2.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        private readonly WindowService windowService;
        private readonly CadDbContext context = new CadDbContext();
        public Project project { get; set; }
        public MainViewModel()
        {
            windowService = new WindowService();
            AddRecCommand = new RelayCommand(this.AddRec);
            AddCircleCommand = new RelayCommand(this.AddCircle);
            AddSquareCommand = new RelayCommand(this.AddSquare);
            AddPolygonCommand = new RelayCommand(this.AddPolygon);
            DeleteSelectedShapeCommand = new RelayCommand(this.DeleteSelectedShape);
            SaveCommand = new RelayCommand(this.Save);
            LoadCommand = new RelayCommand(this.Load);
            shapes = new ObservableCollection<MyShapes>();
        }

        

        private void DeleteSelectedShape()
        {
            if (this.SelectedShape != null)
            {
                this.shapes.Remove(this.SelectedShape);
                this.SelectedShape = null;
            }
        }

        private void Save()
        {
            // Set the folder path inside your project's directory
            //_folderPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectFiles");

            // Ensure the directory exists
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            

            // Define the file path (you can use a name based on the project, date, etc.)
            project.Path = System.IO.Path.Combine(_folderPath, $"{project.Name}.xml");
            
            // Check if the file exists

            // Serialize the data (this will overwrite the file if it already exists)
            SerializeToXml(this.shapes.ToList(), project.Path);
            context.Projects.Add(project);
            context.SaveChanges();
            SendData(project);

        }
        public void SendData(Project data)
        {
            WeakReferenceMessenger.Default.Send(new MessageService2(data));
            System.Diagnostics.Debug.WriteLine($"Message Sent:");
        }
        private void SerializeToXml<T>(T obj, string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var writer = new StreamWriter(filePath, false)) // false ensures it overwrites
                {
                    serializer.Serialize(writer, obj);
                }

                Console.WriteLine($"File saved at: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving the file: {ex.Message}");
            }
        }

        private void Load()
        {
            // Define the file path

            string filePath = System.IO.Path.Combine(_folderPath, $"{project.Name}.xml");

            if (File.Exists(filePath))
            {
                var membersList = DeserializeFromXml<List<MyShapes>>(filePath);
                this.shapes = new ObservableCollection<MyShapes>(membersList);

                // Logic to open the project in a new tab
                //OpenProjectInNewTab();
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        private T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                using (var reader = new StreamReader(filePath))
                {
                    return (T)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading the file: {ex.Message}");
                return default(T);
            }
        }


        private void AddPolygon()
        {
            this.IsAddMenuOpened = false;

            var polygon = new MyPolygon();
            var viewModel = new PolygonModelView(polygon);
            this.windowService.ShowDialog(viewModel);
            if (!viewModel.IsCanceled)
            {
                polygon.Points = new PointCollection();
                

                for (int i = 0; i < polygon.NoOfSides; i++)
                {
                    double angle = i * 2 * Math.PI / polygon.NoOfSides;
                    double x = polygon.Startx+ polygon.Radius * Math.Cos(angle);
                    double y = polygon.Starty+ polygon.Radius * Math.Sin(angle);
                    polygon.Points.Add(new Point(x, y));
                }
                this.shapes.Add(polygon);
            }
        }

        private void AddSquare()
        {
            this.IsAddMenuOpened = false;

            var square = new MySquare();
            var viewModel = new SquareViewModel(square);
            this.windowService.ShowDialog(viewModel);
            if (!viewModel.IsCanceled)
            {
                this.shapes.Add(square);
            }
        }

        private void AddCircle()
        {
            this.IsAddMenuOpened = false;

            var circle = new MyCircle();
            var viewModel = new CircleViewModel(circle);
            this.windowService.ShowDialog(viewModel);
            if (!viewModel.IsCanceled)
            {
                this.shapes.Add(circle);
            }
        }

        private void AddRec()
        {
            this.IsAddMenuOpened = false;

            var rec = new MyRectangle();
            var viewModel = new RectangleViewModel(rec);
            this.windowService.ShowDialog(viewModel);
            if (!viewModel.IsCanceled)
            {
                this.shapes.Add(rec);
            }
        }
        

        private bool _isAddMenuOpened;
        public bool IsAddMenuOpened
        {
            get => _isAddMenuOpened;
            set => SetProperty(ref _isAddMenuOpened, value);
        }
        private MyShapes _selectedShape;
        public MyShapes SelectedShape
        {
            get => _selectedShape;
            set => SetProperty(ref _selectedShape, value);
        }
        private ObservableCollection<MyShapes> _shapes;
        public ObservableCollection<MyShapes> shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }
        public ICommand AddRecCommand { get; }
        public ICommand AddSquareCommand { get; }
        public ICommand AddPolygonCommand { get; }
        public ICommand AddCircleCommand { get; }
        public ICommand DeleteSelectedShapeCommand { get; }

        public ICommand SaveCommand { get; }
        public ICommand LoadCommand { get; }

        public string Header { get; set; }
        public object Content { get; set; }
        public string _folderPath { get; set; } = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ProjectFiles");

    }
}
