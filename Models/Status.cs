using SQLite;


namespace TodoApp.Models
{
    [Table("Statuses")]
    public class Status
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
