using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database.Tables;

namespace WpfApp1.DataType.Database.Entities
{
    public class World : DatabaseObject<World>
    {
        private WorldsTable Table = new WorldsTable();
        public string Name { get; set;}
        public string Description { get; set; } = "";
        public string Icon { get; set; } = "";
        public string Map { get; set; } = "";
        public World()
        {
        }


        public override World Get()
        {
            return Table.Get(Id);
        }
        public override World Add()
        {
            return Table.Add(this);
        }
        public override void Update()
        {
            Table.Update(this);
        }

        public override World Remove()
        {
            return Table.Remove(Id);
        }

    }
}
