using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.Model.DataType.Entities.RealEntities
{
    public class Nation : RealEntity
    {
        private string? map;

        public string? Map
        {
            get => map;
            set
            {
                if (SetProperty(ref map, value))
                {
                    OnPropertyChanged(nameof(MapBitmap));
                }
            }
        }

        public BitmapImage MapBitmap
        {
            get => Manager.GetInstance().ImageManager.LoadBitmap(Map);
        }

        private string? flag;

        public string? Flag
        {
            get => flag;
            set
            {
                if (SetProperty(ref flag, value))
                {
                    OnPropertyChanged(nameof(FlagBitmap));
                }
            }
        }

        public BitmapImage FlagBitmap
        {
            get => Manager.GetInstance().ImageManager.LoadBitmap(Flag);
        }

        public Nation()
        {
            Type = EntityType.Nation;
        }
    }
}
