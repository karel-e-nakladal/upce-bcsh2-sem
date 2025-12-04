using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Contents;

namespace WpfApp1.DataType.Entities
{
    public abstract class Entity
    {
        public EntityType Type { init; get; }
        public int Id { init; get; }

        public string ?Name { get; set; }
        
        public EntityPage? Content { get; set; }
    }
}
