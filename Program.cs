using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskInterface, TaskService>(); // Implementando interface no serviço

// Configurar o DbContext com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=TasksData.db");
});

// Add services to the container.
builder.Services.AddControllers(); // Habilita controllers
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers(); // Mapeia endpoints dos controllers

app.Run();

