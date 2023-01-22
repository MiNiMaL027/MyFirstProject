using System.Data;

namespace List_Domain.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }
        public int Importance { get; set; }
        public string? Description { get; set; }
        public string Title { get; set; }
        public DateTime? DueData { get; set; }     
        public bool IsCompleted { get; set; }
        public bool IsFavorites { get; set; }
        public bool IsDeleted { get; set; }
        public int ToDoListId { get; set; }
        public ToDoList? ToDoList { get; set; }
    }
}
