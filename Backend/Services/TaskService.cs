using ToDoAPI.Data;
using ToDoAPI.Models;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Dtos;

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
            response.Message = "Todos as tarefas foram encontradas";
            return response;

        } catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<TaskModel>> GetTaskById(int idTask)
    {
        ResponseModel<TaskModel> response = new ResponseModel<TaskModel>();
        try
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(taskData =>
                taskData.Id == idTask); 

            if (task == null)
            {
                response.Message = "Nenhuma tarefa encontrada";
                return response;
            }
            
            response.Dados = task;
            response.Message = "Tarefa encontrada";
            
            return response;
            
        } catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<List<TaskModel>>> CreateTask(CreateTaskDto createTaskDto)
    {
        ResponseModel<List<TaskModel>> response = new ResponseModel<List<TaskModel>>();
        try
        {
            var task = new TaskModel() // objeto de task
            {
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = createTaskDto.Status,
            };
            
            _context.Add(task);
            await _context.SaveChangesAsync(); // Para adicionar no banco
            
            response.Dados = await _context.Tasks.ToListAsync();
            response.Message = "Tarefa criada com sucesso";
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            return response;
        }
    }
}