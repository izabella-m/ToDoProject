using ToDoAPI.Dtos;
using ToDoAPI.Models;

namespace ToDoAPI.Services;

public interface ITaskInterface
{
    Task<ResponseModel<List<TaskModel>>> GetAllTasks(); // método retorna lista de tasks
    Task<ResponseModel<TaskModel>> GetTaskById(int idTask);
    Task<ResponseModel<List<TaskModel>>> CreateTask(CreateTaskDto createTaskDto);
    //Task<ResponseModel<TaskModel>> UpdateTask(int idTask, TaskModel task);
}