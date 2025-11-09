using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;
using WpfApp1.DataType.Database.Entities;

namespace WpfApp1.Database.Tables
{
    class WorldsTable : Table<World>
    {
        public WorldsTable()
        {
        }
        
        public override World[] GetAll()
        {
            var conn = db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = $"SELECT * FROM worlds";

            SqliteDataReader reader = cmd.ExecuteReader();
            var tmp = new List<World>();

            while (reader.Read())
            {
                tmp.Add(Format(reader));
            }
            return tmp.ToArray();

        }
        public override World Get(int id)
        {
            var conn = db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM worlds WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            return Format(cmd.ExecuteReader());
        }

        public override World Add(World item)
        {
            var conn = db.getConnection();

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

        public override World Update(World item)
        {
            var conn = db.getConnection();

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

        public override World Remove(int id)
        {
            World removed = Get(id);

            var conn = db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM worlds WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }
        
        protected override World Format(SqliteDataReader reader)
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
