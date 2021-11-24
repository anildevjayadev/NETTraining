using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NETTraining.Models;
using Microsoft.AspNetCore.Http;

namespace NETTraining.Controllers
{
    public class TaskController : ControllerBase
    {
        string message;

        List<TasksModels> Tasks = new List<TasksModels> {
            new TasksModels{TaskID=1,Status=200,UserName ="Mark"},
            new TasksModels{TaskID=2,Status=300,UserName="June"},
            new TasksModels{TaskID=3,Status=409,UserName="Scott"},
        };

        [HttpGet]
        public List<TasksModels> GetAllTasks()
        {
            return Tasks;
        }

        [Route("{taskId}")]
        [HttpGet]
        public ActionResult<TasksModels> GetProjectById(int taskId)
        {
            var taskSearchedbyId = Tasks.FirstOrDefault(p => p.TaskID == taskId);
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
            TasktobeUpdated.Status = task.Status;
            TasktobeUpdated.UserName = task.UserName;

            return Ok();
        }

        [HttpPost]
        public ActionResult<TasksModels> CreateNewTasks(TasksModels project)
        {
            if (project.TaskID == 0 || project.UserName == null)
            {
                return StatusCode(400, "Invalid Task details provided");
            }
            var taskalreadyExists = Tasks.First(p => p.TaskID == project.TaskID);

            if (taskalreadyExists != null)
            {
                message = "Task already exists(existing Task User Name:{0})";
                string data = taskalreadyExists.UserName;
                message = string.Format(message, data);
                return StatusCode(409, message);
            }
            Tasks.Add(project);
            return Ok();
        }
    }
}
