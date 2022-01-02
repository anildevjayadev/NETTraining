using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Repository.Models;

namespace WebApplication12.Controllers
{

        [Route("api/[controller]")]
        [ApiController]
        public class ProjectsController : ControllerBase
        {
            private readonly IProjects _repository;
            public ProjectsController(IProjects repository)
            {
                _repository = repository;
            }

            [HttpGet]
            public IActionResult Get()
            {
                var projects = _repository.GetAllProjects();
                return Ok(projects);
            }

            [HttpGet("{projectid}")]
            public IActionResult Get(int projectid)
            {
                var project = _repository.GetProjectById(projectid);
                if (project == null)
                {
                    return NoContent();
                }
                return Ok(project);
            }

            [HttpPost]
            public IActionResult Post(Project projects)
            {
                var projectIdExists = _repository.GetProjectById(projects.ProjectId);
                if (projectIdExists != null)
                    return BadRequest("Project already exists.");
                
                var newProject = _repository.AddProject(projects);
                return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host
                    + HttpContext.Request.Path + "/" + projects.ProjectId, projects);
            }

            [HttpPut]
            public IActionResult Put(Project projects)
            {
                var updateProject = _repository.GetProjectById(projects.ProjectId);
                if (updateProject == null)
                {
                    return NotFound();
                }
                updateProject.ProjectName = projects.ProjectName;
                updateProject.ProjectName = projects.ProjectName;
                return Ok(updateProject);
            }
        }
    }
