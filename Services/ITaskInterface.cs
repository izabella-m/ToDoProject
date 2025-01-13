using ToDoAPI.Models;

namespace ToDoAPI.Services;

public interface ITaskInterface
{
    Task<ResponseModel<List<TaskModel>>> GetAllTasks(); // retorna lista de tasks
    Task<ResponseModel<TaskModel>> GetTaskById(int idTask);
    //Task<ResponseModel<TaskModel>> CreateTask(TaskModel task);
    //Task<ResponseModel<TaskModel>> UpdateTask(int idTask, TaskModel task);
}