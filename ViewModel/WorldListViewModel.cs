using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public partial class WorldListViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<World> _worlds;
        
        [ObservableProperty]
        private World ?_selectedWorld;

        public IRelayCommand SelectCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand EditCommand { get; }
        public IRelayCommand DeleteCommand { get; }
        public WorldListViewModel()
        {
            _worlds = new ObservableCollection<World>(Manager.GetInstance().GetWorlds());
            SelectCommand = new RelayCommand(Select);
            AddCommand = new RelayCommand(Add);
            EditCommand = new RelayCommand(Edit);
            DeleteCommand = new RelayCommand(Delete);
        }

        private void Select()
        {
            if (SelectedWorld is null)
                return;
            SelectedWorld.Load();
            Manager.GetInstance().SelectWorld(SelectedWorld);
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new WorldView());
        }
        private void Add()
        {
            var dia = new AddWorldView();

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                string name = dia.Name.Text;
                string description = dia.Description.Text;

                var newWorld = new World { Name = name, Description = description };
                Worlds.Add(Manager.GetInstance().AddWorld(newWorld));
            }
        }
        private void Delete()
        {
            if (SelectedWorld is null)
                return;
            SelectedWorld.Remove();
            _worlds.Remove(SelectedWorld);
            SelectedWorld = null;
        }
        private void Edit()
        {
            if (SelectedWorld is null)
                return;

            var dia = new AddWorldView();

            dia.Name.Text = SelectedWorld.Name;
            dia.Description.Text = SelectedWorld.Description;

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                SelectedWorld.Name = dia.Name.Text;
                SelectedWorld.Description = dia.Description.Text;
                SelectedWorld.Update();
            }
        }
    }
}
