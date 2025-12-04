using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Database
{
    enum Operation
    {
        Select,
        Insert,
        Update,
        Delete
    }
    class QueryBuilder
    {
        /*
        private string command;

        private Operation operation;

        private string table;

        private string columns;

        private string[] parameters;

        private bool isWhere = false;
        private string whereCommand;

        private bool isOrder = false;
        private string orderCommand;

        public QueryBuilder()
        {

        }

        public QueryBuilder Table(string table)
        {
            this.table = table;
            return this;
        }

        public QueryBuilder Select(string columns)
        {

            operation = Operation.Select;
            return this;
        }

        public QueryBuilder Insert(string columns, string[] parameters)
        {
            operation = Operation.Insert;
            return this;
        }

        public QueryBuilder Update(string columns, string[] parameters)
        {
            operation = Operation.Update;
            return this;
        }

        public QueryBuilder Delete()
        {
            operation = Operation.Delete;
            return this;
        }

        public QueryBuilder Where(string column, string parameter)
        {
            isWhere = true;

            return this;
        }

        public QueryBuilder Order(string column, string order = "DESC")
        {
            isOrder = true;
            orderCommand = $"ORDER {column} {order}";
            return this;
        }

        public string Build()
        {
            string query = "";
            switch (operation)
            {
                case Operation.Select:
                    query = $"SELECT ({columns}) FROM {table}" + (isWhere ? whereCommand : "") + (isOrder ? orderCommand : "");
                    break;
                case Operation.Insert:
                    query = $"INSERT INTO {table} ({columns}) VALUES ({parameters.})"
                    break;
                case Operation.Update:
                    break;
                case Operation.Delete:
                    break;
            }

            return query;

        }


    */
    }
}
