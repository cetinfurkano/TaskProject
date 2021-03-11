using CaseStudy.Core.DataAccess.EntityFramework;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CaseStudy.DataAccess.Concrete
{
    public class EfUserTaskDal: EfRepositoryBase<UserTask>, IUserTaskDal
    {
        public EfUserTaskDal(AppDbContext context) : base(context)
        {

        }

        public async Task<TaskDetailResource> GetTaskDetailByIdAsync(int id)
        {
            var context = (AppDbContext)_context;
            var result = from userTask in context.UserTasks
                         join appointed in context.Users
                         on userTask.AppointedId equals appointed.Id
                         join assigned in context.Users
                         on userTask.AssignedId equals assigned.Id
                         join status in context.Statuses
                         on userTask.StatusId equals status.Id
                         where userTask.Id == id
                         select new TaskDetailResource
                         {
                             Id = userTask.Id,
                             Title = userTask.Title,
                             AssignedUser = assigned,
                             AppointedUser = appointed,
                             Status = status,
                             Description = userTask.Description,
                             DueDate = userTask.DueDate.ToString("dd.MM.yyyy"),
                             ExpectedDueDate = userTask.ExpectedDueDate.ToString("dd.MM.yyy"),
                             Priority = userTask.Priority,
                             StartingDate = userTask.StartingDate.ToString("dd.MM.yyy")
                         };
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<TaskDetailResource>> GetTasksDetailsAsync()
        {
            var context = (AppDbContext)_context;
            var result = from userTask in context.UserTasks
                         join appointed in context.Users
                         on userTask.AppointedId equals appointed.Id
                         join assigned in context.Users
                         on userTask.AssignedId equals assigned.Id
                         join status in context.Statuses
                         on userTask.StatusId equals status.Id
                         select new TaskDetailResource
                         {
                             Id = userTask.Id,
                             Title = userTask.Title,
                             AssignedUser = assigned,
                             AppointedUser = appointed,
                             Status = status,
                             Description = userTask.Description,
                             DueDate = userTask.DueDate.ToString("dd.MM.yyyy"),
                             ExpectedDueDate = userTask.ExpectedDueDate.ToString("dd.MM.yyy"),
                             Priority = userTask.Priority,
                             StartingDate = userTask.StartingDate.ToString("dd.MM.yyy")
                         };
            return await result.ToListAsync();
        }
    }
}
