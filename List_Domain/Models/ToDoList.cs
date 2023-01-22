namespace List_Domain.Models
{
    public class ToDoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<ToDoTask>? Tasks { get; set; } = new();
    }
}
