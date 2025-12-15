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
    public class ItemTable
    {
        private DB _db = DB.GetInstance();
        public Item Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM items WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return Format(reader);
        }

        public Item Add(Item item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            int pageId = new PageTable().Add(item.Content ?? new EntityPage()).Id; // Creating Page

            cmd.CommandText = "INSERT INTO items (world_id, readable_id, name, description, icon, value, page_id) VALUES ($world_id, $readable_id, $name, $description, $icon, $value, $page_id)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description ?? "");
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");
            cmd.Parameters.AddWithValue("$value", item.Value);
            cmd.Parameters.AddWithValue("$page_id", pageId);

            cmd.ExecuteNonQuery();

            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM items ORDER BY id DESC LIMIT 1";

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return Format(reader);
            }
            return null;
        }

        public Item Update(Item item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE items SET world_id = $world_id, readable_id = $readable_id, name = $name, description = $description, icon = $icon, value = $value WHERE id = $id";

            cmd.Parameters.AddWithValue("world_id", item.World);
            cmd.Parameters.AddWithValue("$readanle_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");
            cmd.Parameters.AddWithValue("$value", item.Value);
            cmd.Parameters.AddWithValue("$id", item.Id);

            item.Content.Update(); // Updating Page

            cmd.ExecuteNonQuery();

            return item;
        }

        public Item Remove(int id)
        {
            Item removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            new PageTable().Remove(Get(id).Content.Id); // Deleting Page

            cmd.CommandText = "DELETE FROM items WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static Item Format(SqliteDataReader reader)
        {
            return new Item
            {
                Id = reader.GetInt32(0),
                World = reader.GetInt32(1),
                ReadableId = reader.GetString(2),
                Name = reader.GetString(3),
                Description = reader.GetString(4),
                Icon = reader.GetString(5),
                Value = reader.GetInt32(6),
                Content = new PageTable().Get(reader.GetInt32(7)) // Getting Page
            };
        }
    }
}
