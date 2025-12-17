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
    public partial class WorldViewModel : ObservableObject
    {
        [ObservableProperty]
        private World _world;

        [ObservableProperty]
        private FlowDocument page = new();


        private IEnumerable<KeyValuePair<EntityType, Dictionary<int, RealEntity>>> _childrenForBinding;
        public IEnumerable<KeyValuePair<EntityType, Dictionary<int, RealEntity>>> ChildrenForBinding
        {
            get => _childrenForBinding;
            set
            {
                _childrenForBinding = value;
                OnPropertyChanged(nameof(ChildrenForBinding));
            }
        }

        public IRelayCommand BackCommand { get; }
        public IRelayCommand EditCommand { get; }
        public IRelayCommand AddCommand { get; }
        public IRelayCommand OpenEntityCommand { get; }

        public WorldViewModel()
        {
            World = Manager.GetInstance().GetWorld();
            Page.Blocks.AddRange(World.Content.Build());

            BackCommand = new RelayCommand(Back);
            EditCommand = new RelayCommand(Edit);
            AddCommand = new RelayCommand(Add);
            OpenEntityCommand = new RelayCommand<RealEntity>(OpenEntity);
            ChildrenForBinding = Manager.GetInstance().GetWorld().Children;
        }


        public void OpenEntity(RealEntity entity)
        {
            Manager.GetInstance().GetWorld().Load();
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new EntityView(entity));
        }
        public void Back()
        {
            Manager.GetInstance().GetWorld().UnLoad();
            Manager.GetInstance().MainWindow.MainFrame.Navigate(new WorldListView());
        }
        public void Edit()
        {
            var dia = new RichTextEditorWindowView(World);

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                World.Content = ((RichTextEditorViewModel)dia.RichText.DataContext).GetContnet();
                World.Content.Update();
                Page.Blocks.Clear();
                Page.Blocks.AddRange(World.Content.Build());
            }
        }

        public void Add()
        {
            var dia = new AddEntityView();

            var position = Mouse.GetPosition(Manager.GetInstance().MainWindow);
            var point = Manager.GetInstance().MainWindow.PointToScreen(position);

            dia.Left = point.X;
            dia.Top = point.Y;

            if (dia.ShowDialog() == true)
            {
                var vm = ((AddEntityViewModel)dia.DataContext);
                EntityType type = vm.SelectedType;
                switch (type)
                {
                    case EntityType.Location:
                        var location = new Location()
                        {
                            Name = vm.Name,
                            Description = vm.Description,
                            //Icon = dia.Icon.ToString(),
                            ReadableId = vm.ReadableId,
                            World = Manager.GetInstance().GetWorld().Id
                        };
                        Manager.GetInstance().Database.Location.Add(location);
                        break;
                    case EntityType.Nation:
                        var nation = new Nation()
                        {
                            Name = vm.Name,
                            Description = vm.Description,
                            //Icon = dia.Icon.ToString(),
                            ReadableId = vm.ReadableId,
                            World = Manager.GetInstance().GetWorld().Id
                        };
                        Manager.GetInstance().Database.Nation.Add(nation);
                        break;
                    case EntityType.Character:
                        var character = new Character()
                        {
                            Name = vm.Name,
                            Description = vm.Description,
                            //Icon = dia.Icon.ToString(),
                            ReadableId = vm.ReadableId,
                            World = Manager.GetInstance().GetWorld().Id,
                            Strength = vm.Strength ?? 0,
                            Dexterity = vm.Dexterity ?? 0,
                            Constitution = vm.Constitution ?? 0,
                            Intelligence = vm.Intelligence ?? 0,
                            Wisdom = vm.Wisdom ?? 0,
                            Charisma = vm.Charisma ?? 0
                        };
                        Manager.GetInstance().Database.Character.Add(character);
                        break;
                    case EntityType.Item:
                        var item= new Item()
                        {
                            Name = vm.Name,
                            Description = vm.Description,
                            //Icon = dia.Icon.ToString(),
                            ReadableId = vm.ReadableId,
                            World = Manager.GetInstance().GetWorld().Id,
                            Value = vm.Value ?? 0
                        };
                        Manager.GetInstance().Database.Item.Add(item);
                        break;
                }
                Manager.GetInstance().GetWorld().Load();
                ChildrenForBinding = Manager.GetInstance().GetWorld().Children;
            }
        }

        public void Remove()
        {
            Manager.GetInstance().GetWorld().Load();
            ChildrenForBinding = Manager.GetInstance().GetWorld().Children;
        }


    }
}
