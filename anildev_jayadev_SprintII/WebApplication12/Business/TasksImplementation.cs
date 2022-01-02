using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Repository.Models;

namespace Business
{
    public class TasksImplementation:ITask
    {
        private readonly AppDbContext _context;
        public TasksImplementation(AppDbContext context)
        {
            _context = context;
        }
        public TasksModel AddTask(TasksModel tasks)
        {
            _context.Tasks.Add(tasks);
            _context.SaveChanges();
            return tasks;
        }

        public List<TasksModel> GetAllTasks()
        {
            return _context.Tasks.ToList();
        }

        public TasksModel GetTaskById(int taskid)
        {
            return _context.Tasks.FirstOrDefault(t => t.ID == taskid);
        }

        public TasksModel UpdateTask(TasksModel tasks)
        {
            _context.Tasks.Update(tasks);
            _context.SaveChanges();
            return tasks;
        }
    }
}
