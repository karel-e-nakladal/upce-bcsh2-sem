using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DataType.Entity
{
    public abstract class Entity
    {
        public EntityType Type { init; get; }
        public int Id { init; get; }

        public string ?Name { get; set; }
        
        public Page ?Content { get; set; }
    }
}
