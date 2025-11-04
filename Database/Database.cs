using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace WpfApp1.Database
{
    class Database
    {
        private static Database instance = null;

        protected SqliteConnection connection = null;

        private Database()
        {
            connection = new SqliteConnection("Data Source=dndapp.db");
        }

        public static Database getInstance()
        {
            if(instance== null)
            {
                instance = new Database();
            }

            return instance;
        }
        
        public SqliteDataReader executeCommand(String command)
        {
            SqliteCommand tmp =  connection.CreateCommand();
            tmp.CommandText =command;
            return tmp.ExecuteReader();
        }
    }

}
