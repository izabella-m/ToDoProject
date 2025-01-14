using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Dtos;
using ToDoAPI.Models;
using ToDoAPI.Services;

namespace ToDoAPI.Controllers;

public class TaskController : ControllerBase // or Controller
{
   private readonly ITaskInterface _taskInterface;

   public TaskController(ITaskInterface taskInterface)
   {
      _taskInterface = taskInterface; // acessa métodos da interface
   }

   [HttpGet("GetTasks")]
   public async Task<ActionResult<ResponseModel<List<TaskModel>>>> GetTasks()
   {
      var tasks = await _taskInterface.GetAllTasks(); 
      return Ok(tasks);
   }
   
   [HttpGet("GetTaskById/{idTask}")]
   public async Task<ActionResult<ResponseModel<TaskModel>>> GetTaskById(int idTask)
   {
      var task = await _taskInterface.GetTaskById(idTask); 
      return Ok(task);
   }
   
   [HttpPost("CreateTask")]
   public async Task<ActionResult<ResponseModel<TaskModel>>> CreateTask(CreateTaskDto createTaskDto)
   {
      var task = await _taskInterface.CreateTask(createTaskDto); 
      return Ok(task);
   }
}