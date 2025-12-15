using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType;

namespace WpfApp1.Model.DataType.Entities.RealEntities
{
    public class Item: RealEntity
    {
        public int Value { get; set; } = 0;

        public Item()
        {
            Type = EntityType.Item;
        }

        public void Update()
        {
            Manager.GetInstance().Database.Item.Update(this);
        }

        public void Remove()
        {
            Manager.GetInstance().Database.Item.Remove(Id);
            Manager.GetInstance().GetWorld().Load();
        }
    }
}
