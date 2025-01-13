using ToDoAPI.Data;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Services;

public class TaskService : ITaskInterface // Respeita as regras de task interface
{
    //Acessar o banco de dados
    private readonly AppDbContext _context;
    public TaskService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ResponseModel<List<TaskModel>>> GetAllTasks()
    {
        ResponseModel<List<TaskModel>> response = new ResponseModel<List<TaskModel>>();
        try
        {
            var tasks = await _context.Tasks.ToListAsync(); // Transforma em lista todas as tasks do meu banco
            
            response.Dados = tasks;
            response.Mensagem = "Todos as tarefas foram encontradas";
            return response;
            
        }
        catch (Exception e)
        {
            response.Mensagem = e.Message;
            response.Status = false;
            return response;
        }
    }

    public Task<ResponseModel<TaskModel>> GetTaskById(int idTask)
    {
        throw new NotImplementedException();
    }
}