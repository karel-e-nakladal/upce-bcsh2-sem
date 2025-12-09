using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.Model.DataType.Entities.RealEntities
{
    public class Nation : RealEntity
    {
        public string ?Map { get; set; }
        public string ?Flag { get; set; }

        public Nation()
        {
            Type = EntityType.Nation;
        }
    }
}
