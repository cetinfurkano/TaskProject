
using CaseStudy.Core.Communication;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllAsync(Expression<Func<User, bool>> filter = null);
        Task<OperationResponse<User>> GetByIdAsync(int id);
        Task<OperationResponse<User>> AddAsync(User user);
        Task<OperationResponse<User>> UpdateAsync(User user);
        Task<OperationResponse<User>> RemoveAsync(User user);
    }
}
