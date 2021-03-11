using CaseStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.Entity.Concrete
{
    public class Status:TEntity
    {
        public int Id { get; set; }
        public string StatusDesc { get; set; }
    }
}
