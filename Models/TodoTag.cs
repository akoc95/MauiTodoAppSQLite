using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models
{
    [Table("TodoTags")]
    public class TodoTag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int TodoId { get; set; }
        public int TagId { get; set; }


    }
}
