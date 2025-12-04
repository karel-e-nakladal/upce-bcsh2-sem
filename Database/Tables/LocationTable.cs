using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Entity.RealEntities;

namespace WpfApp1.Database.Tables
{
    public class LocationTable
    {
        private DB _db = DB.GetInstance();
        public Location Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM locations WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            return Format(cmd.ExecuteReader());
        }

        public Location Add(Location item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO locations (world_id, readable_id, name, description, icon, map) VALUES ($world_id, $readable_id, $name, $description, $icon, $map)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$map", item.Map);

            cmd.ExecuteNonQuery();

            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM locations ORDER BY id DESC LIMIT 1";

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return Format(reader);
            }
            return null;
        }

        public Location Update(Location item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE locations SET world_id = $world_id, readable_id = $readable_id, name = $name, description = $description, icon = $icon, map = $map WHERE id = $id";

            cmd.Parameters.AddWithValue("world_id", item.World);
            cmd.Parameters.AddWithValue("$readanle_id", item.ReadableId);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$map", item.Map);
            cmd.Parameters.AddWithValue("$id", item.Id);

            cmd.ExecuteNonQuery();

            return item;
        }

        public Location Remove(int id)
        {
            Location removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM locations WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static Location Format(SqliteDataReader reader)
        {
            return new Location
            {
                Id = reader.GetInt32(0),
                World = reader.GetInt32(1),
                ReadableId = reader.GetString(2),
                Name = reader.GetString(3),
                Description = reader.GetString(4),
                Icon = reader.GetString(5),
                Map = reader.GetString(6),
            };
        }
    }
}
