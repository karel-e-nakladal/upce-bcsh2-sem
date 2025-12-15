using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.Model.Database.Tables
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

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return Format(reader);
        }

        public Nation Add(Nation item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            int pageId = new PageTable().Add(item.Content ?? new EntityPage()).Id; // Creating Page

            cmd.CommandText = "INSERT INTO nations (world_id, readable_id, name, description, icon, map, flag, page_id) VALUES ($world_id, $readable_id, $name, $description, $icon, $map, $flag, $page_id)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");
            cmd.Parameters.AddWithValue("$flag", item.Flag ?? "");
            cmd.Parameters.AddWithValue("$map", item.Map ?? "");
            cmd.Parameters.AddWithValue("$page_id", pageId);

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
            cmd.Parameters.AddWithValue("$readanle_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");
            cmd.Parameters.AddWithValue("$map", item.Map ?? "");
            cmd.Parameters.AddWithValue("$flag", item.Flag ?? "");
            cmd.Parameters.AddWithValue("$id", item.Id);

            item.Content.Update(); // Updating Page

            cmd.ExecuteNonQuery();

            return item;
        }

        public Nation Remove(int id)
        {
            Nation removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            new PageTable().Remove(Get(id).Content.Id); // Deleting Page

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
                Content = new PageTable().Get(reader.GetInt32(8)) // Getting Page
            };
        }
    }
}
