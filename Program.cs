using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;
using ToDoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ITaskInterface, TaskService>();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar o DbContext com SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source=TasksData.db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "API de Lista de Tarefas rodando!");

app.Run();

