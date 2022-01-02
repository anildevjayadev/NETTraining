using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Models
{
    public class TasksModel
    {

        public int ID { get; set; }
        public int ProjectId { get; set; }
        public int TaskassignedtoUserId { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
