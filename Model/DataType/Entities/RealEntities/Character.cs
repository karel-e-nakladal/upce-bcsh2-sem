using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType;

namespace WpfApp1.Model.DataType.Entities.RealEntities
{
    public class Character: RealEntity
    {
        public int Strength { get; set; } = 0;
        public int Dexterity { get; set; } = 0;
        public int Constitution { get; set; } = 0;
        public int Intelligence { get; set; } = 0;
        public int Wisdom { get; set; } = 0;
        public int Charisma { get; set; } = 0;

        public Character()
        {
            Type = EntityType.Character;
        }

        public void Update()
        {
            Manager.GetInstance().Database.Character.Update(this);
        }

        public void Remove()
        {
            Manager.GetInstance().Database.Character.Remove(Id);
            Manager.GetInstance().GetWorld().Load();
        }
    }
}
