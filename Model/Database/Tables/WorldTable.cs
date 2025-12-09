using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using System.Windows.Controls;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Contents;

namespace WpfApp1.Model.Database.Tables
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

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return Format(reader);
        }

        public World Add(World item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            int pageId = new PageTable().Add(item.Content ?? new EntityPage()).Id; // Creating Page

            cmd.CommandText = "INSERT INTO worlds (name, description, icon, map, page_id) VALUES ($name, $description, $icon, $map, $page_id)";

            cmd.Parameters.AddWithValue("$name", item.Name ?? "");
            cmd.Parameters.AddWithValue("$description", item.Description ?? "");
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");
            cmd.Parameters.AddWithValue("$map", item.Map ?? "");
            cmd.Parameters.AddWithValue("$page_id", pageId);

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

            item.Content.Update(); // Updating Page

            cmd.ExecuteNonQuery();

            return item;
        }

        public World Remove(int id)
        {
            World removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            new PageTable().Remove(Get(id).Content.Id); // Deleting Page

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
                Icon = reader.GetString(3),
                Map = reader.GetString(4),
                Content = new PageTable().Get(reader.GetInt32(5)) // Getting Page
            };
        }
    }
}
