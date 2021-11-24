using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using NETTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 

namespace NETTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        string message;

        List<ProjectModels> Projects = new List<ProjectModels> {
            new ProjectModels{ProjectId=1,ProjectName="Student Project", ProjectUseCase ="Project for Student management"},
            new ProjectModels{ProjectId=2,ProjectName="Employee Project", ProjectUseCase ="Project for Employee Management"},
            new ProjectModels{ProjectId=3,ProjectName="Employer Project", ProjectUseCase ="Project for Employer Management"},
        };

        [HttpGet]
        public List<ProjectModels> GetAllProjects()
        {
            return Projects;            
        }

        [Route("{projectId}")]
        [HttpGet]
        public ActionResult<ProjectModels> GetProjectById(int projectId)
        {
            var ProjectSearchedbyId= Projects.FirstOrDefault(p => p.ProjectId == projectId);
            if (ProjectSearchedbyId == null)
            {
                return NoContent();
            }
            return ProjectSearchedbyId;
        }

        [HttpPut]
        public ActionResult<ProjectModels> UpdateProject(ProjectModels project)
        {
            var ProjecttobeUpdated = Projects.FirstOrDefault(p => p.ProjectId == project.ProjectId);

            if (ProjecttobeUpdated == null)
            {
                return NotFound();
            }
            ProjecttobeUpdated.ProjectName = project.ProjectName;
            ProjecttobeUpdated.ProjectUseCase = project.ProjectUseCase;

            return Ok();
        }

        [HttpPost]
        public ActionResult<ProjectModels> CreateNewProject(ProjectModels project)
        {
            if (project.ProjectId == 0 || project.ProjectName == null)
            {
                return StatusCode(400, "Invalid Project details provided");
            }
            var projectalreadyExists = Projects.First(p => p.ProjectId == project.ProjectId);

            if (projectalreadyExists != null)
            {
                message = "Project already exists(existing Project Name:{0})";
                string data = projectalreadyExists.ProjectName;
                message = string.Format(message, data);
                return StatusCode(409, message);
            }
            Projects.Add(project);
            return Ok();
        }

    }
}
