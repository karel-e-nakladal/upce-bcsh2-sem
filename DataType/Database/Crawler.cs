using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.Database.Tables;
using WpfApp1.DataType.Database.Table;
using WpfApp1.DataType.Entity;
using WpfApp1.DataType.Entity.RealEntities;

namespace WpfApp1.DataType.Database
{
    public class Crawler
    {
        private DB _db = DB.GetInstance();

        public WorldTable World { init; get; }

        public LocationTable Location { init; get; }
        public NationTable Nation { init; get; }
        public Crawler()
        {
            World = new WorldTable();
            Location = new LocationTable();
            Nation = new NationTable();
        }

        public List<World> GetWorlds()
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM worlds";

            var reader = cmd.ExecuteReader();

            var result = new List<World>();

            while (reader.Read())
            {
                result.Add(WorldTable.Format(reader));
            }
            return result;
        }

        public List<Location> GetLocationsByWorld(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM locations WHERE world_id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();

            var result = new List<Location>();

            while (reader.Read())
            {
                result.Add(LocationTable.Format(reader));
            }

            return result;
        }
        public List<Nation> GetNationsByWorld(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM nations WHERE world_id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();

            var result = new List<Nation>();

            while (reader.Read())
            {
                result.Add(NationTable.Format(reader));
            }

            return result;
        }
    }
}
