using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.Model.Database.Tables;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.Model.DataType.Database
{
    public class Crawler
    {
        private DB _db = DB.GetInstance();

        public WorldTable World { init; get; }

        public LocationTable Location { init; get; }
        public NationTable Nation { init; get; }
        public PageTable Page{ init; get; }
        public CharacterTable Character { init; get; }
        public ItemTable Item { init; get; }
        public Crawler()
        {
            World = new();
            Location = new();
            Nation = new();
            Page = new();
            Character = new();
            Item = new();
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
        public List<Character> GetCharactersByWorld(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM characters WHERE world_id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();

            var result = new List<Character>();

            while (reader.Read())
            {
                result.Add(CharacterTable.Format(reader));
            }

            return result;
        }
        public List<Item> GetItemsByWorld(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM items WHERE world_id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();

            var result = new List<Item>();

            while (reader.Read())
            {
                result.Add(ItemTable.Format(reader));
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
