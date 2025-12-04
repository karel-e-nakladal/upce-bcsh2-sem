using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType;
using WpfApp1.DataType.Entities;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public partial class WorldViewModel : ObservableObject
    {
        [ObservableProperty]
        private World _world;

        public IRelayCommand BackCommand { get; }
        public IRelayCommand EditCommand { get; }
        public IRelayCommand AddCommand { get; }

        public WorldViewModel()
        {
            World = Manager.GetInstance().GetWorld();

            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);
        }


        public void Back()
        {
            Manager.GetInstance().GetWorld().UnLoad();
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new WorldListView());
        }
        public void Edit()
        {

        }

        public void Add()
        {
            var dia = new AddEntityView();

            if(dia.ShowDialog() == true)
            {

            }
        }


    }
}
