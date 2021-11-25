using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETTraining.Models
{
    public class TasksModels
    {

        public int TaskID { get; set; }
        public int ProjectId { get; set; }
        public int TaskassignedtoUserId { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
