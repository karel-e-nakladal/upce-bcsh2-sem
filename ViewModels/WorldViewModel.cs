using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Database.Entities;

namespace WpfApp1.ViewModels
{
    class WorldViewModel
    {
        public World SelectedWorld { get; }
        public WorldViewModel(World world)
        {
            SelectedWorld = world;
        }
    }
}
