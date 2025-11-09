using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.DataType.Database;

namespace WpfApp1.Database
{
    public abstract class Table<T> where T : DatabaseObject<T>
    {
        protected DB db = DB.GetInstance();

        /// <summary>
        /// Returns all objects in the table
        /// </summary>
        /// <exception cref="">
        /// </exception>
        /// <returns></returns>
        public abstract T[] GetAll() ;
        /// <summary>
        /// Returns the object with specified id from the table
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="">
        /// </exception>
        /// <returns></returns>
        public abstract T Get(int id) ;
        /// <summary>
        /// Adds the object in to the table
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="">
        /// </exception>
        public abstract T Add(T item);
        /// <summary>
        /// Updates the object in the table
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="">
        /// </exception>
        public abstract T Update(T item);
        /// <summary>
        /// Removes the object from the table
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="">
        /// </exception>
        /// <returns></returns>
        public abstract T Remove(int id);
        /// <summary>
        /// Returns the specified object from database reader
        /// </summary>
        /// <param name="reader"></param>
        /// <exception cref="">
        /// </exception>
        /// <returns></returns>
        protected abstract T Format(SqliteDataReader reader);
    }
}
