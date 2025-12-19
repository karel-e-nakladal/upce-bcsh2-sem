using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;

namespace WpfApp1.Model.DataType.Entities
{
    public abstract partial class Entity: ObservableObject
    {
        public EntityType Type { init; get; }
        public int Id { init; get; }

        public string ?Name { get; set; }

        public string? Description { get; set; }

        private string? icon;

        public string? Icon
        {
            get => icon;
            set
            {
                if (SetProperty(ref icon, value))
                {
                    OnPropertyChanged(nameof(IconBitmap));
                }
            }
        }

        public BitmapImage IconBitmap
        {
            get => Manager.GetInstance().ImageManager.LoadBitmap(Icon);
        }

        public EntityPage Content { get; set; } = new();

        public string? Created { init; get; }
    }
}
