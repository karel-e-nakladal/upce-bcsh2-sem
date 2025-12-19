using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.Relations;

namespace WpfApp1.Model.DataType.Entities.RealEntities
{
    public class RealEntity : Entity
    {
        public int World { init; get; }

        private List<Relation> relations = new();


        public void Update()
        {
            switch (Type)
            {
                case EntityType.Location:
                    var tmpL = (Location)this;
                    Manager.GetInstance().Database.Location.Update(tmpL);
                    break;
                case EntityType.Nation:
                    var tmpN = (Nation)this;
                    Manager.GetInstance().Database.Nation.Update(tmpN);
                    break;
                case EntityType.Character:
                    var tmpC = (Character)this;
                    Manager.GetInstance().Database.Character.Update(tmpC);
                    break;
                case EntityType.Item:
                    var tmpI = (Item)this;
                    Manager.GetInstance().Database.Item.Update(tmpI);
                    break;
            }
        }
    }
}
