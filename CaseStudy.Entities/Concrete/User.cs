using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Entity.Concrete
{
   public class User:TEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
