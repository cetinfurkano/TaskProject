using CaseStudy.Business.Abstract;

using CaseStudy.Business.ValidationRules;
using CaseStudy.Core.Communication;
using CaseStudy.Core.DataAccess;
using CaseStudy.Core.Validation;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class StatusService : IStatusService
    {
        private readonly IStatusDal _statusDal;
        private readonly IUnitOfWork _unitOfWork;

        public StatusService(IStatusDal statusDal, IUnitOfWork unitOfWork)
        {
            _statusDal = statusDal;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResponse<Status>> AddAsync(Status status)
        {
            var validationResult = ValidationTool.Validate(new StatusValidator(), status);
            if (!validationResult.IsValid)
            {
                return OperationResponse<Status>.CreateFailure("Girdiğiniz veriler geçersiz.");
            }

            try
            {
                await _statusDal.AddAsync(status);
                await _unitOfWork.CompleteAsync();

                return OperationResponse<Status>.CreateSuccesResponse(status);
            }
            catch (Exception ex)
            {
                return OperationResponse<Status>.CreateFailure($"Status eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<List<Status>> GetAllAsync(Expression<Func<Status, bool>> filter = null)
        {
            return await _statusDal.GetAllAsync(filter);
        }

        public async Task<OperationResponse<Status>> GetByIdAsync(int id)
        {
            var result = await _statusDal.GetAsync(s => s.Id == id);

            if (result == null)
            {
                return OperationResponse<Status>.CreateFailure("Status bulunamadı!");
            }
            return OperationResponse<Status>.CreateSuccesResponse(result);
        }

        public async Task<OperationResponse<Status>> RemoveAsync(Status status)
        {
            var existingStatus = await _statusDal.GetAsync(s => s.Id == status.Id);
            if (existingStatus == null)
            {
                return OperationResponse<Status>.CreateFailure("Status bulunamadı!");
            }

            try
            {
                _statusDal.Remove(existingStatus);
                await _unitOfWork.CompleteAsync();
                return OperationResponse<Status>.CreateSuccesResponse(existingStatus);
            }
            catch (Exception ex)
            {
                return OperationResponse<Status>.CreateFailure($"Status silinirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<OperationResponse<Status>> UpdateAsync(Status status)
        {
            var validationResult = ValidationTool.Validate(new StatusValidator(), status);
            if (!validationResult.IsValid)
            {
                return OperationResponse<Status>.CreateFailure("Girdiğiniz veriler geçersiz.");
            }


            var existingStatus = await _statusDal.GetAsync(s => s.Id == status.Id);
            if (existingStatus == null)
            {
                return OperationResponse<Status>.CreateFailure("Status bulunamadı!");
            }

            existingStatus.StatusDesc = status.StatusDesc;

            try
            {
                await _unitOfWork.CompleteAsync();

                return OperationResponse<Status>.CreateSuccesResponse(existingStatus);
            }
            catch (Exception ex)
            {
                return OperationResponse<Status>.CreateFailure($"Status güncellenirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
