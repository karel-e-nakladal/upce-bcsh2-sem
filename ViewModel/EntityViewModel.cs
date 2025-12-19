using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    public partial class EntityViewModel : ObservableObject
    {

        [ObservableProperty]
        private RealEntity? selectedEntity;

        [ObservableProperty]
        private FlowDocument page = new();

        [ObservableProperty]
        private bool hasMap;

        [ObservableProperty]
        private BitmapImage mapBitmap;

        public IRelayCommand BackCommand { get;}
        public IRelayCommand EditEntityCommand { get;}
        public RelayCommand EditCommand { get; }

        public EntityViewModel(RealEntity entity) {
            selectedEntity = entity;
            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(Edit);
            EditEntityCommand = new RelayCommand(EditEntity);
            initialize();
        }
        public EntityViewModel(EntityType type, int id) {

            var tmp = Manager.GetInstance().GetWorld().Children[type][id];

            

            if(tmp is null)
                return;

            selectedEntity = tmp;

            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(Edit);
            EditEntityCommand = new RelayCommand(EditEntity);
            initialize();
        }

        private void initialize()
        {
            if (SelectedEntity is null || SelectedEntity == null)
                Back();

            if (selectedEntity.Type == EntityType.Location)
            {
                HasMap = true;
                MapBitmap = ((Location)selectedEntity).MapBitmap;
            }else if(selectedEntity.Type == EntityType.Nation)
            {
                HasMap = true;
                MapBitmap = ((Nation)selectedEntity).MapBitmap;
            }

                Page.Blocks.AddRange(SelectedEntity.Content.Build());
        }


        private void Back()
        {
            Manager.GetInstance().GetWorld().Load();
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new WorldView());
        }

        public void EditEntity()
        {
            var dia = new AddEntityView(selectedEntity);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);
            
            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                var vm = (AddEntityViewModel)dia.DataContext;
                selectedEntity.Name = vm.Name;
                selectedEntity.Description = vm.Description;
                selectedEntity.Icon = vm.IconPath;

                switch (SelectedEntity.Type)
                {
                    case EntityType.Location:
                        ((Location)selectedEntity).Map = vm.MapPath;
                        break;
                    case EntityType.Nation:
                        ((Nation)selectedEntity).Flag = vm.FlagPath;
                        ((Nation)selectedEntity).Map = vm.MapPath;
                        break;
                    case EntityType.Character:
                        ((Character)selectedEntity).Strength = (int)vm.Strength;
                        ((Character)selectedEntity).Dexterity = (int)vm.Dexterity;
                        ((Character)selectedEntity).Constitution = (int)vm.Constitution;
                        ((Character)selectedEntity).Intelligence = (int)vm.Intelligence;
                        ((Character)selectedEntity).Wisdom = (int)vm.Wisdom;
                        ((Character)selectedEntity).Charisma = (int)vm.Charisma;
                        break;
                    case EntityType.Item:
                        ((Item)selectedEntity).Value = (int)vm.Value;
                        break;

                }
                selectedEntity.Update();
                Manager.GetInstance().MainWindow.MainFrame.Navigate(new EntityView(selectedEntity));
            }
        }

        public void Edit()
        {
            var dia = new RichTextEditorWindowView(selectedEntity);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                selectedEntity.Content = ((RichTextEditorViewModel)dia.RichText.DataContext).GetContnet();
                selectedEntity.Content.Update();
                Page.Blocks.Clear();
                Page.Blocks.AddRange(selectedEntity.Content.Build());
            }
        }
    }
}
