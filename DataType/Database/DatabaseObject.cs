using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType.Database.Entities;

namespace WpfApp1.DataType.Database
{
    public abstract class DatabaseObject<D>
    {
        public int Id { get; init; }

        public abstract D Get();
        public abstract D Add();
        public abstract void Update();

        public abstract D Remove();
    }
}
