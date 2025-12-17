using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WpfApp1.DataType;
using WpfApp1.Model;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.ViewModel
{
    public partial class AddImageViewModel: ObservableObject
    {
        [ObservableProperty]
        public string? name;

        [ObservableProperty]
        public string path;

        [ObservableProperty]
        public bool isEdited = false;

        public event Action? RequestClose;

        public RelayCommand SelectImageCommand { get; }

        private Entity selectedEntity;

        public RelayCommand DeleteImageCommand { get; }

        public AddImageViewModel(Entity entity, PageBlock? data)
        {
            selectedEntity = entity;
            SelectImageCommand = new RelayCommand(SelectImage);
            DeleteImageCommand = new RelayCommand(DeleteImage);
            
            if (data is not null && data.Type == ContentType.Image)
            {
                isEdited = true;
                Name = (data.Text);
                Path = (data.Path);
            }
            else
            {
                Path = Manager.GetInstance().ImageManager.GetDefaultImage();
            }
        }

        public void DeleteImage()
        {
            if (selectedEntity.Type == EntityType.World)
            {
                Path = Manager.GetInstance().ImageManager.Delete(selectedEntity.Type, ImageType.Image, selectedEntity.Id, null, int.Parse(Name));
            }
            else
            {
                var tmp = (RealEntity)selectedEntity;
                Path = Manager.GetInstance().ImageManager.Delete(tmp.Type, ImageType.Image, tmp.World, tmp.Id, int.Parse(Name));
            }
            Name = "";
            RequestClose.Invoke();
        }

        public void SelectImage()
        {
            if(selectedEntity.Type == EntityType.World)
            {
                Path = Manager.GetInstance().ImageManager.ShowWindow(selectedEntity.Type, ImageType.Image, selectedEntity.Id);
            }
            else
            {
                var tmp = (RealEntity)selectedEntity;
                Path = Manager.GetInstance().ImageManager.ShowWindow(tmp.Type, ImageType.Image, tmp.World, tmp.Id);
            }
            Name = Manager.GetInstance().ImageManager.IndexFromPath(Path).ToString();
        }
    }
}
