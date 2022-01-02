using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Models;

namespace Business
{
    public class ProjectImplementation: IProjects
    {
        private readonly AppDbContext _context;
        public ProjectImplementation(AppDbContext context)
        {
            _context = context;
        }

        public Project AddProject(Project project)
        {
            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public Project UpdateProject(Project project)
        {
            _context.Projects.Update(project);
            _context.SaveChanges();
            return project;
        }

        public List<Project> GetAllProjects()
        {
            return _context.Projects.ToList();
        }

        public Project GetProjectById(int projectid)
        {
            return _context.Projects.FirstOrDefault(p => p.ProjectId == projectid);
        }
    }
}
