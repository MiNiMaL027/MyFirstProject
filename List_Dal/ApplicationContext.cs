using List_Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace List_Dal
{
    public class ApplicationContext : DbContext
    {
        public DbSet<ToDoList> Lists { get; set; }
        public DbSet<ToDoTask> Tasks { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {
            Database.EnsureCreated();
        }
        //public override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer()
        //}
    }
}