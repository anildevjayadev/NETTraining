using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Repository
{
   public interface ITask
    {
        List<TasksModel> GetAllTasks();
        TasksModel GetTaskById(int id);
        TasksModel AddTask(TasksModel tasksmodel);
        TasksModel UpdateTask(TasksModel tasksmodel);
    }
}
