using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Database;
using WpfApp1.DataType;
using WpfApp1.Model.DataType.Contents;
using WpfApp1.Model.DataType.Entities.RealEntities;

namespace WpfApp1.Model.Database.Tables
{
    public class CharacterTable
    {
        private DB _db = DB.GetInstance();
        public Character Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM characters WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return null;

            return Format(reader);
        }

        public Character Add(Character item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            int pageId = new PageTable().Add(item.Content ?? new EntityPage()).Id; // Creating Page

            cmd.CommandText = "INSERT INTO characters (world_id, readable_id, name, description, icon, strength, dexterity, constitution, intelligence, wisdom, charisma, page_id) VALUES ($world_id, $readable_id, $name, $description, $icon, $strength, $dexterity, $constitution, $intelligence, $wisdom, $charisma, $page_id)";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description ?? "");
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? Manager.GetInstance().ImageManager.GetDefaultImage());

            cmd.Parameters.AddWithValue("$strength", item.Strength);
            cmd.Parameters.AddWithValue("$dexterity", item.Dexterity);
            cmd.Parameters.AddWithValue("$constitution", item.Constitution);
            cmd.Parameters.AddWithValue("$intelligence", item.Intelligence);
            cmd.Parameters.AddWithValue("$wisdom", item.Wisdom);
            cmd.Parameters.AddWithValue("$charisma", item.Charisma);

            cmd.Parameters.AddWithValue("$page_id", pageId);

            cmd.ExecuteNonQuery();

            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM characters ORDER BY id DESC LIMIT 1";

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return Format(reader);
            }
            return null;
        }

        public Character Update(Character item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE characters SET world_id = $world_id, readable_id = $readable_id, name = $name, description = $description, icon = $icon, strength = $strength, dexterity = $dexterity, constitution = $constitution, intelligence = $intelligence, wisdom = $wisdom, charisma = $charisma WHERE id = $id";

            cmd.Parameters.AddWithValue("$world_id", item.World);
            cmd.Parameters.AddWithValue("$readable_id", item.ReadableId ?? item.Name.ToLower().Replace(" ", "_"));
            cmd.Parameters.AddWithValue("$name", item.Name);
            cmd.Parameters.AddWithValue("$description", item.Description);
            cmd.Parameters.AddWithValue("$icon", item.Icon ?? "");

            cmd.Parameters.AddWithValue("$strength", item.Strength);
            cmd.Parameters.AddWithValue("$dexterity", item.Dexterity);
            cmd.Parameters.AddWithValue("$constitution", item.Constitution);
            cmd.Parameters.AddWithValue("$intelligence", item.Intelligence);
            cmd.Parameters.AddWithValue("$wisdom", item.Wisdom);
            cmd.Parameters.AddWithValue("$charisma", item.Charisma);

            cmd.Parameters.AddWithValue("$id", item.Id);

            item.Content.Update(); // Updating Page

            cmd.ExecuteNonQuery();

            return item;
        }

        public Character Remove(int id)
        {
            Character removed = Get(id);

            Manager.GetInstance().ImageManager.DeleteEntity(removed.Type, removed.World, removed.Id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            new PageTable().Remove(Get(id).Content.Id); // Deleting Page

            cmd.CommandText = "DELETE FROM characters WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static Character Format(SqliteDataReader reader)
        {
            return new Character
            {
                Id = reader.GetInt32(0),
                World = reader.GetInt32(1),
                ReadableId = reader.GetString(2),
                Name = reader.GetString(3),
                Description = reader.GetString(4),
                Icon = reader.GetString(5),
                Strength = reader.GetInt32(6),
                Dexterity = reader.GetInt32(7),
                Constitution = reader.GetInt32(8),
                Intelligence = reader.GetInt32(9),
                Wisdom = reader.GetInt32(10),
                Charisma = reader.GetInt32(11),
                Content = new PageTable().Get(reader.GetInt32(12)) // Getting Page
            };
        }
    }
}
