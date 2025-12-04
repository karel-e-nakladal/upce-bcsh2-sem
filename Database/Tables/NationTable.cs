using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Entities.RealEntities;

namespace WpfApp1.Database.Tables
{
    public class NationTable
    {
        private DB _db = DB.GetInstance();
        public Nation Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM nations WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            return Format(cmd.ExecuteReader());
        }

        public Nation Add(Nation item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO nations (world_id, readable_id, name, description, icon, map, flag) VALUES ($world_id, $readable_id, $name, $description, $icon, $map, $flag)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$flag", item.Flag);
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

        public Nation Update(Nation item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE nation SET world_id = $world_id, readable_id = $readable_id, name = $name, description = $description, icon = $icon, map = $map, flag = $flag WHERE id = $id";

            cmd.Parameters.AddWithValue("world_id", item.World);
            cmd.Parameters.AddWithValue("$readanle_id", item.ReadableId);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon);
            cmd.Parameters.AddWithValue("$map", item.Map);
            cmd.Parameters.AddWithValue("$flag", item.Flag);
            cmd.Parameters.AddWithValue("$id", item.Id);

            cmd.ExecuteNonQuery();

            return item;
        }

        public Nation Remove(int id)
        {
            Nation removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM nations WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static Nation Format(SqliteDataReader reader)
        {
            return new Nation
            {
                Id = reader.GetInt32(0),
                World = reader.GetInt32(1),
                ReadableId = reader.GetString(2),
                Name = reader.GetString(3),
                Description = reader.GetString(4),
                Icon = reader.GetString(5),
                Map = reader.GetString(6),
                Flag = reader.GetString(7),
            };
        }
    }
}
