using SQLite;

namespace TodoApp.Models
{
    [Table("Tags")]
    public class Tag
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }


    }
}
