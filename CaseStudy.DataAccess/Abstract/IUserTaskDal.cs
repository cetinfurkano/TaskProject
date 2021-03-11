using CaseStudy.Core.DataAccess;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.DataAccess.Abstract
{
    public interface IUserTaskDal:IRepository<UserTask>
    {
        Task<List<TaskDetailResource>> GetTasksDetailsAsync();
        Task<TaskDetailResource> GetTaskDetailByIdAsync(int id);
    }
}
