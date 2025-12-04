using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Entity.Relations;

namespace WpfApp1.DataType.Entity.RealEntities
{
    public class RealEntity : Entity
    {
        public int World { init; get; }
        public string ?ReadableId { init; get; }
        public string? Description { get; set; }
        public string? Icon { get; set; }

        private List<Relation> relations = new();
    }
}
