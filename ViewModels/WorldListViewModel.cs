using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Database.Tables;
using WpfApp1.DataType.Database.Entities;
using WpfApp1.View;

namespace WpfApp1.ViewModels
{
    public partial class WorldlistViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<World> _worlds;

        [ObservableProperty]
        private  World selectedWorld;

        private WorldsTable worldsTable;
        public IRelayCommand CreateWorldCommand { get; }
        public IRelayCommand SelectWorldCommand { get; }

        public WorldlistViewModel()
        {
            worldsTable = new WorldsTable();
            _worlds = new ObservableCollection<World>(worldsTable.GetAll());

            CreateWorldCommand = new RelayCommand(CreateWorld);
            SelectWorldCommand = new RelayCommand(SelectWorld);

        }

        private void CreateWorld()
        {
            var dialog = new NewWorldWindow();
            if( dialog.ShowDialog() == true)
            {
                string name = dialog.WorldName.Text;

                var newWorld = new World { Name = name, Description = "", Map = "" };
                
                newWorld = worldsTable.Add(newWorld);
                Worlds.Add(newWorld);
            
            }
        }

        private void SelectWorld()
        {
            if (SelectedWorld == null)
                return;
            var page = new WorldPage
            {
                DataContext = new WorldViewModel(SelectedWorld)
            };

            var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            mainWindow?.MainFrame.Navigate(page);
        }   

        partial void OnSelectedWorldChanged(World value)
        {
            Console.WriteLine(value.Id);
        }
    }
}
