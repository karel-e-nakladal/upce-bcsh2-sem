using System;
using System.Collections.Generic;
using System.Linq;
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
            return connection;
        }

        private void Clear()
        {
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
        }
        
        private void Setup()
        {

            // Worlds
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
                "description TEXT NOT NULL," + // description
                "icon VARCHAR(100) NOT NULL," + // description
                "map VARCHAR(150)" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }

        private void CreateLocations()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS locations (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," +
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT NOT NULL," + // description
                "icon VARCHAR(100) NOT NULL," + // description
                "map VARCHAR(150)," +
                "FOREIGN KEY(world_id) REFERENCES worlds(id)" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }

        private void CreateNations()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS nations (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," +
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT NOT NULL," + // description
                "icon VARCHAR(100) NOT NULL," + // description
                "map VARCHAR(150)," +
                "FOREIGN KEY(world_id) REFERENCES worlds(id)" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }
        private void CreateCharacters()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS characters (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," +
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT NOT NULL," + // description
                "icon VARCHAR(100) NOT NULL," + // description
                "map VARCHAR(150)," +
                "FOREIGN KEY(world_id) REFERENCES worlds(id)" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }
        private void CreateItems()
        {
            var cmd = connection.CreateCommand();

            cmd.CommandText = "CREATE TABLE IF NOT EXISTS items (" +
                "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                "world_id INTEGER," + // world
                "readable_id VARCHAR(50)," +
                "name VARCHAR(100) NOT NULL," + // name
                "description TEXT NOT NULL," + // description
                "icon VARCHAR(100) NOT NULL," + // description
                "map VARCHAR(150)," +
                "FOREIGN KEY(world_id) REFERENCES worlds(id)" + // link to map image
                ");";
            cmd.ExecuteNonQuery();
        }


    }
}
