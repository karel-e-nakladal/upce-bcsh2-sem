using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Database;
using WpfApp1.DataType.Entity;

namespace WpfApp1.DataType
{
    public class Manager
    {

        private static Manager ?_instance;

        private List<World> _worldList;

        public Crawler Database = new Crawler();

        private World ?_selectedWorld;

        public MainWindow MainWindow = MainWindow.Instance;

        private Manager()
        {
            _worldList = new List<World>();

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
            _worldList.Remove(world);
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

        public void AddWorld(World world)
        {
            _worldList.Add(world);
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
