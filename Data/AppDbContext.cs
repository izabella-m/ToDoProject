using Microsoft.EntityFrameworkCore;
using ToDoAPI.Models;

namespace ToDoAPI.Data;

public class AppDbContext : DbContext
{
    // "Tasks" no banco de dados, mapeia a entidade TaskModel
    public DbSet<TaskModel> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=TasksData.db"); // configurar cache
        base.OnConfiguring(optionsBuilder);
    }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}