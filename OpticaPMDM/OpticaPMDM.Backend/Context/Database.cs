using OpticaPMDM.Backend.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OpticaPMDM.Backend.Context
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public static string DBPATH;

        static Database _instance;

        public static Database INSTANCE
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Database();
                }
                return _instance;
            }
        }

        public Database()
        {
            try
            {
                _database = new SQLiteAsyncConnection(DBPATH);
                _database.CreateTableAsync<Lentillas>().Wait();
                _database.CreateTableAsync<Pedidos>().Wait();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<List<T>> GetAll<T>() where T : class, new()
        {
            return _database.Table<T>().ToListAsync();
        }

        public Task<List<T>> GetAll<T>(Expression<Func<T, bool>> whereClause) where T : class, new()
        {
            return _database.Table<T>()
                .Where(whereClause)
                .ToListAsync();
        }

        public Task<T> GetSingle<T>(Expression<Func<T, bool>> whereClause) where T : class, new()
        {
            return _database.Table<T>()
                .Where(whereClause)
                .FirstOrDefaultAsync();
        }

        public Task<int> Add<T>(T item) where T : class, new()
        {
            return _database.InsertAsync(item);
        }

        public Task<int> Delete<T>(T item) where T : class, new()
        {
            return _database.DeleteAsync(item);
        }

        public Task<int> Update<T>(T item) where T : class, new()
        {
            return _database.UpdateAsync(item);
        }
    }
}
