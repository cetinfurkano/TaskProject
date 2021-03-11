using CaseStudy.Core.Entities;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Entity.Resources
{
    public class TaskDetailResource:IResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public User AppointedUser { get; set; }
        public User AssignedUser { get; set; }
        public string StartingDate { get; set; }
        public string DueDate { get; set; }
        public string ExpectedDueDate { get; set; }
    }
}
