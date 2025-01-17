using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Models;

public class TaskModel
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty; // Garante valor padrão vazio
    
    public string? Description { get; set; } 
    
    public TaskStatus Status { get; set; } 

    public enum TaskStatus
    {
        NotStarted,   // Não iniciado
        InProgress,   // Em andamento
        Completed     // Concluído
    }
}