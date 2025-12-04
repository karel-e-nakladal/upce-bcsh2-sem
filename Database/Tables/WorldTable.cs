using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType.Entity;

namespace WpfApp1.DataType.Database.Table
{
    public class WorldTable
    {

        private DB _db = DB.GetInstance();
        public World Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM worlds WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            return Format(cmd.ExecuteReader());
        }

        public World Add(World item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO worlds (name, description, icon, map) VALUES ($name, $description, $icon, $map)";

            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$map", item.Map);

            cmd.ExecuteNonQuery();

            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM worlds ORDER BY id DESC LIMIT 1";

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return Format(reader);
            }
            return null;
        }

        public World Update(World item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE worlds SET name = $name, description = $description, icon = $icon, map = $map WHERE id = $id";

            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$map", item.Map);
            cmd.Parameters.AddWithValue("$id", item.Id);

            cmd.ExecuteNonQuery();

            return item;
        }

        public World Remove(int id)
        {
            World removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM worlds WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static World Format(SqliteDataReader reader)
        {
            return new World
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Description = reader.GetString(2),
                Map = reader.GetString(3)
            };
        }
    }
}
