using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp1.DataType;
using WpfApp1.DataType.Entity;
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
            var dialog = new AddWorldView();
            if (dialog.ShowDialog() == true)
            {
                string name = dialog.Name.Text;
                string description = dialog.Description.Text;

                var newWorld = new World { Name = name, Description = description };
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
            var dialog = new AddWorldView();
            dialog.Name.Text = SelectedWorld.Name;
            dialog.Description.Text = SelectedWorld.Description;

            if(dialog.ShowDialog() == true)
            {
                SelectedWorld.Name = dialog.Name.Text;
                SelectedWorld.Description = dialog.Description.Text;
                SelectedWorld.Update();
            }
        }
    }
}
