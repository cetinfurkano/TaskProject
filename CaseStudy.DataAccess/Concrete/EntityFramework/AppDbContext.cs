using CaseStudy.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CaseStudy.DataAccess.Concrete
{
   public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=Furkan-Pc;Initial Catalog=CaseStudy;Integrated Security=True");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
    }
}
