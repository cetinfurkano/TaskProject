using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Entity.Resources
{
    public class UserTaskResource:IResource
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int AppointedId { get; set; }
        public int AssignedId { get; set; }
        public string StartingDate { get; set; }
        public string DueDate { get; set; }
        public string ExpectedDueDate { get; set; }
    }
}
