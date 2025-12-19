using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.ViewModel
{
    public partial class AddWorldViewModel: ObservableObject
    {

        private int? id;

        [ObservableProperty]
        private string? name;

        [ObservableProperty]
        private string? description;

        [ObservableProperty]
        public BitmapImage? iconBitmap;

        [ObservableProperty]
        private String? iconPath;

        [ObservableProperty]
        public BitmapImage? mapBitmap;

        [ObservableProperty]
        private String? mapPath;

        [ObservableProperty]
        private bool isEdited = false;

        public RelayCommand ChangeIconCommand { get; }
        public RelayCommand DeleteIconCommand { get; }

        public RelayCommand ChangeMapCommand { get; }
        public RelayCommand DeleteMapCommand { get; }


        public AddWorldViewModel(World? data)
        {
            ChangeIconCommand = new RelayCommand(ChangeIcon);
            DeleteIconCommand = new RelayCommand(DeleteIcon);
            ChangeMapCommand = new RelayCommand(ChangeMap);
            DeleteMapCommand = new RelayCommand(DeleteMap);
            if (data != null)
            {
                isEdited = true;
                id = data.Id;
                name = data.Name;
                description = data.Description;
                iconBitmap = data.IconBitmap;
                iconPath = data.Icon;
                mapBitmap = data.MapBitmap;
                mapPath = data.Map;
            }
        }

        private void ChangeIcon()
        {
            if (!IsEdited || id == null) return;

            IconPath = Manager.GetInstance().ImageManager.ShowWindow(EntityType.World, ImageType.Icon, (int)id);

            IconBitmap = Manager.GetInstance().ImageManager.LoadBitmap(IconPath);
        }
        private void DeleteIcon()
        {
            if (!IsEdited || id == null) return;

            IconPath = Manager.GetInstance().ImageManager.Delete(EntityType.World, ImageType.Icon, (int)id);

            IconBitmap = Manager.GetInstance().ImageManager.LoadBitmap(IconPath);
        }
        private void ChangeMap()
        {
            if (!IsEdited || id == null) return;

            MapPath = Manager.GetInstance().ImageManager.ShowWindow(EntityType.World, ImageType.Map, (int)id);

            MapBitmap = Manager.GetInstance().ImageManager.LoadBitmap(MapPath);
        }
        private void DeleteMap()
        {
            if (!IsEdited || id == null) return;

            MapPath = Manager.GetInstance().ImageManager.Delete(EntityType.World, ImageType.Map, (int)id);

            MapBitmap = Manager.GetInstance().ImageManager.LoadBitmap(MapPath);
        }
    }
}
