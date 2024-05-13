using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoTagService
    {
        private readonly SQLiteAsyncConnection _connection;

        public TodoTagService(DataService dataService)
        {
            _connection = dataService.GetConnection();
        }

        public async Task AddTodoTagAsync(TodoTag todoTag)
        {
            await _connection.InsertAsync(todoTag);
        }

        public void DeleteTagRel(int Id)
        {
           _connection.Table<TodoTag>().Where(t=> t.TodoId == Id).DeleteAsync();
        }

    }
}
