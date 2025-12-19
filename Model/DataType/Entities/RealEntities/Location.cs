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
    public class Location : RealEntity
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


        public Location()
        {
            Type = EntityType.Location;
        }

        public void Update()
        {
            Manager.GetInstance().Database.Location.Update(this);
        }

        public void Remove()
        {
            Manager.GetInstance().Database.Location.Remove(Id);
            Manager.GetInstance().GetWorld().Load();
        }

    }
}
