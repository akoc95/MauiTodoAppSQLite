using SQLite;


namespace TodoApp.Models
{
    [Table("Todos")]
    public class Todo
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }= DateTime.Now;
        public int StatusId { get; set; }

        [Ignore]
        public string? StatusName { get; set; }

        [Ignore]
        public List<string>? TagName { get; set; }

        [Ignore]
        public string? TagNameAsString
        {
            get
            {
                return string.Join(",", TagName);
            }
        }

        [Ignore]
        public string? DateAsString
        {
            get
            {
                return DateCreated.ToString("dd/MM/yyyy");
            }
        }




    }
}
