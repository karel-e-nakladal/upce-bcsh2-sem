using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Entities.RealEntities;

namespace WpfApp1.DataType.Entities.Relations
{
    public class Relation
    {
        public RealEntity From { init; get; }
        public RealEntity To { get; set; }
        public RelationType Type { get; set; }

        public Relation(RealEntity from, RealEntity to, RelationType type)
        {
            From = from;
            To = to;
            Type = type;
        }

        public void Update()
        {

        }
    }
}
