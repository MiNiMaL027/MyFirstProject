using List_Dal;
using List_Dal.Interfaces;
using List_Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using List_Service.Services;
using List_Service.Interfaces;
using List_Domain.Models;

namespace MyFirstProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));
            builder.Services.AddScoped<IToDoListRepository<ToDoList?> , ToDoListRepository>();
            builder.Services.AddScoped<IToDoTaskRepository<ToDoTask?> , ToDoTaskRepository>();
            builder.Services.AddScoped<ToDoListService>();
            builder.Services.AddScoped<ToDoTaskService>();


            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}