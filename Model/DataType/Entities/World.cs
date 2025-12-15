using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.Model.DataType.Entities
{
    public class World : Entity
    {
        public string ?Map { set; get; }

        public Dictionary<EntityType, Dictionary<int, RealEntity>> ?Children;

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
                if (!Children.TryGetValue(EntityType.Location, out var dict))
                {
                    dict = new Dictionary<int, RealEntity>();
                    Children[EntityType.Location] = dict;
                }
                dict[item.Id] = item;
            }

            foreach (var item in Manager.GetInstance().Database.GetNationsByWorld(Id))
            {
                if (!Children.TryGetValue(EntityType.Nation, out var dict))
                {
                    dict = new Dictionary<int, RealEntity>();
                    Children[EntityType.Nation] = dict;
                }
                dict[item.Id] = item;
            }

            foreach (var item in Manager.GetInstance().Database.GetCharactersByWorld(Id))
            {
                if (!Children.TryGetValue(EntityType.Character, out var dict))
                {
                    dict = new Dictionary<int, RealEntity>();
                    Children[EntityType.Character] = dict;
                }
                dict[item.Id] = item;
            }

            foreach (var item in Manager.GetInstance().Database.GetItemsByWorld(Id))
            {
                if (!Children.TryGetValue(EntityType.Item, out var dict))
                {
                    dict = new Dictionary<int, RealEntity>();
                    Children[EntityType.Item] = dict;
                }
                dict[item.Id] = item;
            }
        }


        public void UnLoad()
        {
            Children = null;
        }

        public RealEntity? GetChildById(int id)
        {
            Load();
            foreach (var kvp in Children)
            {
                var dict = kvp.Value;
                if (dict is not null && dict.TryGetValue(id, out RealEntity entity))
                {
                    return entity;
                }
            }

            return null;
        }

        public RealEntity? GetChildByReadableId(string readableId)
        {
            Load();
            if (string.IsNullOrEmpty(readableId))
                return null;

            foreach (var kvp in Children)
            {
                var dict = kvp.Value;
                if (dict == null)
                    continue;

                foreach (var entity in dict.Values)
                {
                    if (entity.ReadableId == readableId)
                        return entity;
                }
            }

            return null;
        }
    }
}
