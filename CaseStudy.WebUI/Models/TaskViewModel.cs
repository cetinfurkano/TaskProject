using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebUI.Models
{
    public class TaskViewModel
    {
        public List<TaskDetailResource> TaskDetails { get; set; }
        public List<User> Users { get; set; }
        public List<Status> Statuses { get; set; }
    }
}
