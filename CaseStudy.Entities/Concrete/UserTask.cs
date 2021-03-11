using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Entity.Concrete
{
    public class UserTask:TEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Priority { get; set; }
        public string Description { get; set; }
        public int StatusId { get; set; }
        public int AppointedId { get; set; }
        public int AssignedId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpectedDueDate { get; set; }
    }
}
