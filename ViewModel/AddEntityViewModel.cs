using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.ViewModel
{
    public partial class AddEntityViewModel : ObservableObject
    {

        [ObservableProperty]
        public ObservableCollection<EntityType> typeOptions =
            new(Enum.GetValues(typeof(EntityType))
                .Cast<EntityType>()
                .Where(t => t != EntityType.World));

        [ObservableProperty]
        public EntityType selectedType = EntityType.Character;

        [ObservableProperty]
        public bool isEdited = false;



        private int? id;
        private int? worldId;

        [ObservableProperty]
        private string? name;
        [ObservableProperty]
        private string? description;
        [ObservableProperty]
        private string? readableId;

        [ObservableProperty]
        public BitmapImage? iconBitmap;

        [ObservableProperty]
        private String? iconPath;

        [ObservableProperty]
        private int? strength;
        [ObservableProperty]
        private int? dexterity;
        [ObservableProperty]
        private int? constitution;
        [ObservableProperty]
        private int? intelligence;
        [ObservableProperty]
        private int? wisdom;
        [ObservableProperty]
        private int? charisma;

        [ObservableProperty]
        private int? value;

        public RelayCommand ChangeIconCommand { get; }

        public AddEntityViewModel(RealEntity? data)
        {
            ChangeIconCommand = new RelayCommand(ChangeIcon);

            if (data != null)
            {
                IsEdited = true;
                id = data.Id;
                worldId = data.World;
                Name = data.Name;
                ReadableId = data.ReadableId;
                Description = data.Description;
                IconPath = data.Icon;
                IconBitmap = data.IconBitmap;
                

                selectedType = data.Type;

                switch(data.Type)
                {
                    case EntityType.Location:

                        break;
                    case EntityType.Nation:

                        break;
                    case EntityType.Character:
                        var tmpC = (Character)data;
                        Strength = tmpC.Strength;
                        Dexterity = tmpC.Dexterity;
                        Constitution = tmpC.Constitution;
                        Intelligence = tmpC.Intelligence;
                        Wisdom = tmpC.Wisdom;
                        Charisma = tmpC.Charisma;
                        break;
                    case EntityType.Item:
                        var tmpI = (Item) data;
                        Value = tmpI.Value;
                        break;
                }
            }

        }


        private void ChangeIcon()
        {
            if (!IsEdited || id == null) return;

            IconPath = Manager.GetInstance().ImageManager.ShowWindow(SelectedType, ImageType.Icon, (int)worldId, id);

            IconBitmap = Manager.GetInstance().ImageManager.LoadBitmap(IconPath);
        }

    }
}
