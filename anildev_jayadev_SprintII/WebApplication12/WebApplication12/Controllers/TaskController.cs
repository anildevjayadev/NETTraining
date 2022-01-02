using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Repository;
using Repository.Models;
using Microsoft.AspNetCore.Http;

namespace WebApplication12.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class TasksController : ControllerBase
        {
            public string message;
            private readonly ITask _repository;
            public TasksController(ITask repository)
            {
                _repository = repository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                var tasks = _repository.GetAllTasks();
                return Ok(tasks);
            }

            [HttpGet("{taskid}")]
            public IActionResult Get(int taskid)
            {
                var task = _repository.GetTaskById(taskid);
                if (task == null)
                    return BadRequest("Task not found.");
                return Ok(task);
            }

            [HttpPost]
            public IActionResult Post(TasksModel newtask)
            {
                var taskalreadyExists = _repository.GetTaskById(newtask.ID);
                if (taskalreadyExists != null)
                {
                    message = "Task already exists(existing task assigned to userid:{0})";
                    int data = taskalreadyExists.TaskassignedtoUserId;
                    message = string.Format(message, data);
                    return StatusCode(409, message);
                }
                _repository.AddTask(newtask); 
            
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host
                    + HttpContext.Request.Path + "/" + newtask.ID, newtask);
            }

            [HttpPut]
            public IActionResult Put(TasksModel task)
            {
                var taskExists = _repository.GetTaskById(task.ID);
                if (taskExists == null)
                    return NotFound();
                taskExists.ProjectId = task.ProjectId;
                taskExists.Status = task.Status;
                taskExists.TaskassignedtoUserId = task.TaskassignedtoUserId;
                taskExists.Details = task.Details;
                var updatedTask = _repository.UpdateTask(taskExists);               
                return Ok(updatedTask);
            }
        }
    }
