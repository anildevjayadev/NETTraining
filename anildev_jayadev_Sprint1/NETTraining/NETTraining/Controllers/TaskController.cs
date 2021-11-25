using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETTraining.Models;
using Microsoft.AspNetCore.Http;

namespace NETTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        string message;

        List<TasksModels> Tasks = new List<TasksModels> {
            new TasksModels{TaskID=1,ProjectId=1,Status=2,TaskassignedtoUserId =1,Details="This is a test task",CreatedOn=DateTime.Now },
            new TasksModels{TaskID=2,ProjectId=1,Status=3,TaskassignedtoUserId=2,Details="This is a test task",CreatedOn=DateTime.Now},
            new TasksModels{TaskID=3,ProjectId=2,Status=4,TaskassignedtoUserId=2,Details="this is a test task",CreatedOn=DateTime.Now},
        };

        [HttpGet]
        public List<TasksModels> GetAllTasks()
        {
            return Tasks;
        }

        [Route("{taskId}")]
        [HttpGet]
        public ActionResult<TasksModels> GetTaskById(int taskId)
        {
            var taskSearchedbyId = Tasks.FirstOrDefault(t => t.TaskID == taskId);
            if (taskSearchedbyId == null)
            {
                return NoContent();
            }
            return taskSearchedbyId;
        }

        [HttpPut]
        public ActionResult<TasksModels> UpdateProject(TasksModels task)
        {
            var TasktobeUpdated = Tasks.FirstOrDefault(p => p.TaskID == task.TaskID);

            if (TasktobeUpdated == null)
            {
                return NotFound();
            }
            TasktobeUpdated.ProjectId = task.ProjectId;
            TasktobeUpdated.TaskassignedtoUserId = task.TaskassignedtoUserId;
            TasktobeUpdated.Status = task.Status;
            TasktobeUpdated.Details = task.Details;

            return Ok();
        }

        [HttpPost]
        public ActionResult<TasksModels> CreateNewTasks(TasksModels newtask)
        {
            if (newtask.TaskID == 0 || newtask.ProjectId == 0 || newtask.TaskassignedtoUserId ==0)
            {
                return StatusCode(400, "Invalid Task details provided");
            }
            var taskalreadyExists = Tasks.FirstOrDefault(t => t.TaskID == newtask.TaskID);

            if (taskalreadyExists != null)
            {
                message = "Task already exists(existing task assigned to userid:{0})";
                int data = taskalreadyExists.TaskassignedtoUserId;
                message = string.Format(message, data);
                return StatusCode(409, message);
            }
            Tasks.Add(newtask);
            return Ok();
        }
    }
}
