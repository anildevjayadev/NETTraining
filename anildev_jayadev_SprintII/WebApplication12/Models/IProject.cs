using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository
{
    public interface IProjects
    {
        List<Project> GetAllProjects();
        Project GetProjectById(int id);
        Project AddProject(Project project);
        Project UpdateProject(Project project);
    }
}
