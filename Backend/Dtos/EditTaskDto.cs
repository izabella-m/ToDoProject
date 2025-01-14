using ToDoAPI.Models;   

namespace ToDoAPI.Dtos;

public class EditTaskDto
{
    public int Id { get; set; } // Identificador da tarefa a ser editada
    
    public string Title { get; set; } 
    
    public string? Description { get; set; } 
    
    public TaskModel.TaskStatus Status { get; set; } // Usa a enumeração do modelo
}