using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.Model.Database.Tables
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

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return Format(reader);
        }

        public Location Add(Location item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            int pageId = new PageTable().Add(item.Content ?? new EntityPage()).Id; // Creating Page

            cmd.CommandText = "INSERT INTO locations (world_id, name, description, icon, map, page_id) VALUES ($world_id, $name, $description, $icon, $map, $page_id)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description ?? "");
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? Manager.GetInstance().ImageManager.GetDefaultImage());
            cmd.Parameters.AddWithValue("$map", item.Map ?? Manager.GetInstance().ImageManager.GetDefaultImage());
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

        public Location Update(Location item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE locations SET world_id = $world_id, name = $name, description = $description, icon = $icon, map = $map WHERE id = $id";

            cmd.Parameters.AddWithValue("world_id", item.World);
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? Manager.GetInstance().ImageManager.GetDefaultImage());
            cmd.Parameters.AddWithValue("$map", item.Map ?? Manager.GetInstance().ImageManager.GetDefaultImage());
            cmd.Parameters.AddWithValue("$id", item.Id);

            item.Content.Update(); // Updating Page

            cmd.ExecuteNonQuery();

            return item;
        }

        public Location Remove(int id)
        {
            Location removed = Get(id);

            Manager.GetInstance().ImageManager.DeleteEntity(EntityType.Location, removed.World, id);

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
                Name = reader.GetString(2),
                Description = reader.GetString(3),
                Icon = reader.GetString(4),
                Map = reader.GetString(5),
                Content = new PageTable().Get(reader.GetInt32(6)) // Getting Page
            };
        }
    }
}
