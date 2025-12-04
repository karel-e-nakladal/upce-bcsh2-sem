using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DataType.Entities.RealEntities
{
    public class Location : RealEntity
    {
        public string ?Map { get; set; }

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
