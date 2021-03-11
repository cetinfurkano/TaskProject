
using CaseStudy.Core.Communication;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IUserTaskService
    {
        Task<List<UserTask>> GetAllAsync(Expression<Func<UserTask, bool>> filter = null);
        Task<OperationResponse<UserTask>> GetByIdAsync(int id);
        Task<OperationResponse<UserTask>> AddAsync(UserTask userTask);
        Task<OperationResponse<UserTask>> UpdateAsync(UserTask userTask);
        Task<OperationResponse<UserTask>> RemoveAsync(UserTask userTask);
        Task<List<TaskDetailResource>> GetTasksDetailsAsync();
        Task<OperationResponse<TaskDetailResource>> GetTaskDetailByIdAsync(int id);
    }
}
