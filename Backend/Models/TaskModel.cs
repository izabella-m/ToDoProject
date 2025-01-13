namespace ToDoAPI.Models;

public class TaskModel
{
    public int Id { get; set; }
    
    public string Title { get; set; } 
    
    public string? Description { get; set; } 
    
    public TaskStatus Status { get; set; } 

    public enum TaskStatus
    {
        NotStarted,   // Não iniciado
        InProgress,   // Em andamento
        Completed     // Concluído
    }
}