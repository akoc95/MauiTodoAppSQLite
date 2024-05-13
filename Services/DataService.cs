using SQLite;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class DataService
    {
        private const string DB_NAME = "app_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public DataService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Todo>();
            _connection.CreateTableAsync<Status>();
            _connection.CreateTableAsync<Tag>();
            _connection.CreateTableAsync<TodoTag>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _connection;
        }
    }
}