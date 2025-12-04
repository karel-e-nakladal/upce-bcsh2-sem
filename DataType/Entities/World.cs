using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType.Database.Table;
using WpfApp1.DataType.Entities.RealEntities;

namespace WpfApp1.DataType.Entities
{
    public class World : Entity
    {
    

        public string ?Description { set; get; }
        public string ?Icon { set; get; }
        public string ?Map { set; get; }

        private Dictionary<EntityType, Dictionary<int, RealEntity>> ?Children;

        public World()
        {
            Type = EntityType.World;
        }

        public void Update()
        {
            Manager.GetInstance().Database.World.Update(this);
        }

        public void Remove()
        {
            Manager.GetInstance().Database.World.Remove(Id);
            Manager.GetInstance().RemoveWorld(this);
        }

        public void Load()
        {
            Children = new Dictionary<EntityType, Dictionary<int, RealEntity>>();
            
            foreach (var item in Manager.GetInstance().Database.GetLocationsByWorld(Id))
            {
                Children[EntityType.Location][item.Id] = item;   
            }
            
            foreach (var item in Manager.GetInstance().Database.GetNationsByWorld(Id))
            {
                Children[EntityType.Nation][item.Id] = item;   
            }
        }

        public void UnLoad()
        {
            Children = null;
        }

        public RealEntity GetByTypeId(EntityType type, int id)
        {
            if (Children is null)
                return null;
            return (RealEntity)Children[type][id];
        }
    }
}
