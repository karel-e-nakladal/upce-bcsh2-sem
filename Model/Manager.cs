using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WpfApp1.Model;
using WpfApp1.Model.DataType.Database;
using WpfApp1.Model.DataType.Entities;

namespace WpfApp1.DataType
{
    public class Manager
    {

        private static Manager ?_instance;

        private List<World> _worldList; // TODO změnit na observable collection pro lepší manipulaci

        public Crawler Database = new();

        private World ?_selectedWorld;

        public MainWindow MainWindow = MainWindow.Instance;

        public ImageManager ImageManager = new();

        private Manager()
        {
            _worldList = Database.GetWorlds();
        }

        public static Manager GetInstance()
        {
            if(_instance == null)
            {
                _instance = new Manager();
            }

            return _instance;
        }

        public void RemoveWorld(World world)
        {
            _worldList.Remove(Database.World.Remove(world.Id));
            if (_selectedWorld == world)
                _selectedWorld = null;
        }

        public List<World> GetWorlds()
        {
            return _worldList;
        }

        public World GetWorld()
        {
            return _selectedWorld;
        }

        public World AddWorld(World world)
        {

            World tmp = Database.World.Add(world);
            _worldList.Add(tmp);
            return tmp;
        }
        public void SelectWorld(int id)
        {
            _selectedWorld = Database.World.Get(id);
        }
        public void SelectWorld(World world)
        {
            _selectedWorld = world;
        }


    }
}
