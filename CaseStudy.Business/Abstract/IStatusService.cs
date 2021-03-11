
using CaseStudy.Core.Communication;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Abstract
{
    public interface IStatusService
    {
        Task<List<Status>> GetAllAsync(Expression<Func<Status, bool>> filter = null);
        Task<OperationResponse<Status>> GetByIdAsync(int id);
        Task<OperationResponse<Status>> AddAsync(Status status);
        Task<OperationResponse<Status>> UpdateAsync(Status status);
        Task<OperationResponse<Status>> RemoveAsync(Status status);
        
    }
}
