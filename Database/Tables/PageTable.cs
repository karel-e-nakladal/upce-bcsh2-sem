using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Contents;
using WpfApp1.DataType.Entities;

namespace WpfApp1.Database.Tables
{
    public class PageTable
    {

        private DB _db = DB.GetInstance();
        public EntityPage Get(int id)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM pages WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            var reader = cmd.ExecuteReader();
            if(!reader.Read())
                return null;

            return Format(reader);
        }

        public EntityPage Add(EntityPage item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "INSERT INTO pages (content) VALUES ($content)";

            cmd.Parameters.AddWithValue("$content", item.JsonSerialize() ?? "");

            cmd.ExecuteNonQuery();

            cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM pages ORDER BY id DESC LIMIT 1";

            var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return Format(reader);
            }
            return null;
        }

        public EntityPage Update(EntityPage item)
        {
            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE pages SET content = $content WHERE id = $id";

            cmd.Parameters.AddWithValue("$content", item.JsonSerialize());

            cmd.ExecuteNonQuery();

            return item;
        }

        public EntityPage Remove(int id)
        {
            EntityPage removed = Get(id);

            var conn = _db.getConnection();

            var cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM pages WHERE id = $id";

            cmd.Parameters.AddWithValue("$id", id);

            cmd.ExecuteNonQuery();

            return removed;

        }

        public static EntityPage Format(SqliteDataReader reader)
        {
            var id = reader.GetInt32(0);
            var content = reader.GetString(1);

            return new EntityPage
            {
                Id = id,
                Content = EntityPage.JsonDeSerialize(content)
            };
        }
    }
}
