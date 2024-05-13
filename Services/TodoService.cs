using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly SQLiteAsyncConnection _connection;
        private readonly TodoTagService _todoTagService;

        public TodoService(DataService dataService, TodoTagService todoTagService)
        {
            _connection = dataService.GetConnection();
            _todoTagService = todoTagService;
        }

        //public async Task<Todo> GetTodoById(Todo todo)
        //{
        //    var foundTodo = await _connection.Table<Todo>().FirstOrDefaultAsync(t => t.Id == todo.Id);

        //    return foundTodo;
        //}

        public async Task AddTodo(Todo todo)
        {
            await _connection.InsertAsync(todo);
        }

        public async Task AddTodoWithTagsAsync(Todo todo, List<int> tagIds)
        {
            await AddTodo(todo);

            foreach (var tagId in tagIds)
            {
                var todoTag = new TodoTag
                {
                    TodoId = todo.Id,
                    TagId = tagId
                };

                await _todoTagService.AddTodoTagAsync(todoTag);
            }
        }

        private void DeleteNullRel()
        {
            _connection.Table<TodoTag>().Where(t => t.TodoId == null || t.TagId == null || t.TodoId == 0 || t.TagId == 0).DeleteAsync();
        }

        public async Task DeleteTodo(Todo todo)
        {
            await _connection.DeleteAsync(todo);
            await _connection.Table<TodoTag>().Where(t => t.TodoId == todo.Id).DeleteAsync();
            DeleteNullRel();
        }


        public async Task<List<Todo>> GetAll()
        {
            var todos = await _connection.Table<Todo>().ToListAsync();

            foreach (var todo in todos)
            {
                var status = await _connection.Table<Status>().Where(s => s.Id == todo.StatusId).FirstOrDefaultAsync();
                if (status != null)
                {
                    todo.StatusName = status.Name;
                }

                var todoTags = await _connection.Table<TodoTag>().Where(t => t.TodoId == todo.Id).ToListAsync();
                todo.TagName = new List<string>();
                foreach (var todoTag in todoTags)
                {
                    var tag = await _connection.Table<Tag>().Where(t => t.Id == todoTag.TagId).FirstOrDefaultAsync();
                    if (tag != null)
                    {
                        todo.TagName.Add(tag.Name);
                    }
                }
            }

            return todos;
        }

        public async Task UpdateTodo(Todo todo, List<int> tagIds)
        {

            _todoTagService.DeleteTagRel(todo.Id);

            var todoTag = new TodoTag();

            foreach (var tagId in tagIds)
            {
                todoTag.TodoId = todo.Id;
                todoTag.TagId = tagId;
                
                await _todoTagService.AddTodoTagAsync(todoTag);                
            }


            await _connection.UpdateAsync(todo);

        }


    }
}
