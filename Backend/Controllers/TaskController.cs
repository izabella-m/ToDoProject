using Microsoft.AspNetCore.Mvc;
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
}