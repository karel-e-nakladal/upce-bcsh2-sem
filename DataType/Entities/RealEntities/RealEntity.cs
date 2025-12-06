using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Entities.Relations;

namespace WpfApp1.DataType.Entities.RealEntities
{
    public class RealEntity : Entity
    {
        public int World { init; get; }
        public string ?ReadableId { init; get; }

        private List<Relation> relations = new();
    }
}
