using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class StatusService
    {
       
        private readonly SQLiteAsyncConnection _connection;


        public StatusService(DataService dataService)
        {
           
            _connection = dataService.GetConnection();

        }

        public async Task AddStatus(Status status)
        {

            await _connection.InsertAsync(status);

        }

        public async Task DeleteStatus(Status status)
        {
            await _connection.DeleteAsync(status);
        }

        public async Task<List<Status>> GetAll()
        {
            return await _connection.Table<Status>().ToListAsync();
        }

        public async Task UpdateStatus(Status status)
        {
            await _connection.UpdateAsync(status);
        }

        public async Task<Status> FindStatusById(int id)
        {
            var existStatus = await _connection.Table<Status>().FirstOrDefaultAsync(x => x.Id == id);

            return existStatus;
        }
    }
}
