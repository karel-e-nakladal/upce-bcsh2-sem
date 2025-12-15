using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
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

        public IRelayCommand BackCommand { get;}
        public IRelayCommand EditEntityCommand { get;}
        public IRelayCommand EditCommand { get;}

        public EntityViewModel(int id) {
            selectedEntity = Manager.GetInstance().GetWorld().GetChildById(id);
            BackCommand = new RelayCommand(Back);
            EditEntityCommand = new RelayCommand(EditEntity);
            EditCommand = new RelayCommand(Edit);
            initialize();
        }
        public EntityViewModel(string readableId) {
            selectedEntity = Manager.GetInstance().GetWorld().GetChildByReadableId(readableId);
            BackCommand = new RelayCommand(Back);
            EditEntityCommand = new RelayCommand(EditEntity);
            EditCommand = new RelayCommand(Edit);

            initialize();
        }

        private void initialize()
        {
            if (SelectedEntity == null)
                Back();
            Page.Blocks.AddRange(SelectedEntity.Content.Build());
        }


        private void Back()
        {
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new WorldView());
        }

        public void EditEntity()
        {
            switch (SelectedEntity.Type)
            {
                case EntityType.Location:
                    break;
                case EntityType.Nation:
                    break;
                case EntityType.Character:
                    break;
                case EntityType.Item:
                    break;

            }
        }

        public void Edit()
        {
            var dia = new RichTextEditorWindowView(selectedEntity.Content);

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
