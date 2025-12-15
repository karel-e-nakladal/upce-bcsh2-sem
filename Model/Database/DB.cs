using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WpfApp1.Database
{
    public class DB
    {
        private static DB instance = null;

        protected SqliteConnection connection = null;

        private string dbName = "dndapp";

        private DB()
        {
            connection = new SqliteConnection($"Data Source={dbName}.db");
            connection.Open();

            var pragma = connection.CreateCommand();
            pragma.CommandText = "PRAGMA foreign_keys = ON;";
            pragma.ExecuteNonQuery();
            
            
            
            //Clear(); // uncomment to clear all data from database
            
            
            
            
            Setup(); // creates necessary tables if they dont exist already 
        }

        public static DB GetInstance()
        {
            if (instance == null)
            {
                instance = new DB();
            }

            return instance;
        }

        public SqliteConnection getConnection()
        {
            return connection; // return connection.open with pragma to use it like (using var conn = getConnection())
        }

        private void Clear()
        {
            /*
            // UNCOMENT WHEN THERE ARE NO FOREIGN KEYS 
            var tableNames = new List<string>();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name NOT LIKE 'sqlite_%';";
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    tableNames.Add(reader.GetString(0));
            }
            foreach (var table in tableNames)
            {
                using var dropCmd = connection.CreateCommand();
                dropCmd.CommandText = $"DROP TABLE IF EXISTS {table};";
                dropCmd.ExecuteNonQuery();
            }
            */
            /*
            */
            using var dropCmd = connection.CreateCommand();
            dropCmd.CommandText = $"DROP TABLE IF EXISTS worlds;";
            dropCmd.ExecuteNonQuery();
        }
        
        private void Setup()
        {

            // Worlds
            CreatePages();
            CreateWorlds();
            CreateLocations();
            CreateNations();
            CreateCharacters();
            CreateItems();
        }

        private void CreateWorlds()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS worlds (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT," + // description
                "icon VARCHAR(100)," + // description
                "map VARCHAR(150)," + // link to map image
                "page_id INTEGER NOT NULL,"+ // id of page
                "FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE" + // link to EntityPage
                ");";
            cmd.ExecuteNonQuery();
        }

        private void CreateLocations()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS locations (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," + // id used for hyperlink
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT," + // description
                "icon VARCHAR(100)," + // icon link
                "map VARCHAR(150)," + // map link
                "page_id INTEGER NOT NULL," + // page
                "FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE," + // link to EntityPage
                "FOREIGN KEY(world_id) REFERENCES worlds(id) ON DELETE CASCADE" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }

        private void CreateNations()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS nations (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," + // id used for hyperlink
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT," + // description
                "icon VARCHAR(100)," + // icon link
                "map VARCHAR(150)," + // map link
                "page_id INTEGER NOT NULL," + // page
                "FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE," + // link to EntityPage
                "FOREIGN KEY(world_id) REFERENCES worlds(id) ON DELETE CASCADE" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }
        private void CreateCharacters()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS characters (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," + // id used for hyperlink
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT," + // description
                "icon VARCHAR(100)," + // icon link
                "strength INTEGER NOT NULL," + // Strenght stat
                "dexterity INTEGER NOT NULL," + // Dexterity stat
                "constitution INTEGER NOT NULL," + // Constitution stat
                "intelligence INTEGER NOT NULL," + // Inteligence stat
                "wisdom INTEGER NOT NULL," + // Wisdom stat
                "charisma INTEGER NOT NULL," + // Charisma stat
                "page_id INTEGER NOT NULL," + // page
                "FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE," + // link to EntityPage
                "FOREIGN KEY(world_id) REFERENCES worlds(id) ON DELETE CASCADE" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }
        private void CreateItems()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS items (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," + // id used for hyperlink
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT," + // description
                "icon VARCHAR(100)," + // icon link
                "page_id INTEGER NOT NULL," + // page
                "FOREIGN KEY (page_id) REFERENCES pages(id) ON DELETE CASCADE," + // link to EntityPage
                "FOREIGN KEY(world_id) REFERENCES worlds(id) ON DELETE CASCADE" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }
        private void CreatePages()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS pages (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "content TEXT NOT NULL" + // List<PageBlock> formated in Json
                ");";
            cmd.ExecuteNonQuery();
        }


    }
}
