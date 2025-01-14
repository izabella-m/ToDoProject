using ToDoAPI.Models; // Importa a enumeração TaskStatus do modelo

namespace ToDoAPI.Dtos;

public class CreateTaskDto
{
    public string Title { get; set; } 
    
    public string? Description { get; set; } 
    
    public TaskModel.TaskStatus Status { get; set; } // Usa a enumeração definida no TaskModel
}