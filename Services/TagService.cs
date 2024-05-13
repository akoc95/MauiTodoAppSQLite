using SQLite;
using System.Collections.ObjectModel;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TagService
    {
        private readonly SQLiteAsyncConnection _connection;

        public TagService(DataService dataService)
        {
            _connection = dataService.GetConnection();
        }

        public async Task AddTag(Tag tag)
        {

            await _connection.InsertAsync(tag);

        }


        public async Task DeleteTag(Tag tag)
        {
            await _connection.DeleteAsync(tag);
        }

        public List<Tag> GetAll()
        {
            var tagList = _connection.Table<Tag>().ToListAsync().Result.ToList();
            return tagList;
        }


        //public async Task<ObservableCollection<Tag>> GetAllAsObservableCollection()
        //{
        //    List<Tag> tags = await _connection.Table<Tag>().ToListAsync();
        //    return new ObservableCollection<Tag>(tags);
        //}

        public async Task UpdateTag(Tag tag)
        {
            await _connection.UpdateAsync(tag);
        }
    }
}
