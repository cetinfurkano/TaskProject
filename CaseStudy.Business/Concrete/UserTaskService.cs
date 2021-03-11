using CaseStudy.Business.Abstract;
using CaseStudy.Business.ValidationRules;
using CaseStudy.Core.Communication;
using CaseStudy.Core.DataAccess;
using CaseStudy.Core.Validation;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class UserTaskService : IUserTaskService
    {
        private readonly IUserTaskDal _userTaskDal;
        private readonly IUnitOfWork _unitOfWork;

        public UserTaskService(IUserTaskDal userTaskDal, IUnitOfWork unitOfWork)
        {
            _userTaskDal = userTaskDal;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperationResponse<UserTask>> AddAsync(UserTask userTask)
        {

            var validationResult = ValidationTool.Validate(new UserTaskValidator(), userTask);

            if (!validationResult.IsValid)
            {
                return OperationResponse<UserTask>.CreateFailure("Girdiğiniz veriler geçersiz.");
            }

            try
            {
                await _userTaskDal.AddAsync(userTask);
                await _unitOfWork.CompleteAsync();

                return OperationResponse<UserTask>.CreateSuccesResponse(userTask);
            }
            catch (Exception ex)
            {
                return OperationResponse<UserTask>.CreateFailure($"UserTask eklenirken bir hata oluştu: {ex.Message}");
            }
        }

        public async Task<List<UserTask>> GetAllAsync(Expression<Func<UserTask, bool>> filter = null)
        {
            return await _userTaskDal.GetAllAsync(filter);
        }

        public async Task<OperationResponse<UserTask>> GetByIdAsync(int id)
        {
            var result = await _userTaskDal.GetAsync(u => u.Id == id);

            if (result == null)
            {
                return OperationResponse<UserTask>.CreateFailure("UserTask bulunamadı!");
            }
            return OperationResponse<UserTask>.CreateSuccesResponse(result);
        }

        public async Task<OperationResponse<TaskDetailResource>> GetTaskDetailByIdAsync(int id)
        {
            var result =  await _userTaskDal.GetTaskDetailByIdAsync(id);

            if (result == null)
            {
                return OperationResponse<TaskDetailResource>.CreateFailure("TaskDetail bulunamadı!");
            }

            return OperationResponse<TaskDetailResource>.CreateSuccesResponse(result);
        }

        public async Task<List<TaskDetailResource>> GetTasksDetailsAsync()
        {
            return await _userTaskDal.GetTasksDetailsAsync();
        }

        public async Task<OperationResponse<UserTask>> RemoveAsync(UserTask userTask)
        {
            var existingTask = await _userTaskDal.GetAsync(u => u.Id == userTask.Id);
            if (existingTask == null)
            {
                return OperationResponse<UserTask>.CreateFailure("UserTask bulunamadı!");
            }

            try
            {
                _userTaskDal.Remove(existingTask);
                await _unitOfWork.CompleteAsync();

                return OperationResponse<UserTask>.CreateSuccesResponse(existingTask);
            }
            catch (Exception ex)
            {

                return OperationResponse<UserTask>.CreateFailure($"Bu görev silinirken bir hata meydana geldi: {ex.Message}");
            }
        }

        public async Task<OperationResponse<UserTask>> UpdateAsync(UserTask userTask)
        {
            var validationResult = ValidationTool.Validate(new UserTaskValidator(), userTask);

            if (!validationResult.IsValid)
            {
                return OperationResponse<UserTask>.CreateFailure("Girdiğiniz veriler uygun değil.");
            }

            var existingTask = await _userTaskDal.GetAsync(u => u.Id == userTask.Id);
            if (existingTask == null)
            {
                return OperationResponse<UserTask>.CreateFailure("Böyle bir görev bulunamadı.");
            }

            existingTask.Priority = userTask.Priority;
            existingTask.StartingDate = userTask.StartingDate;
            existingTask.Title = userTask.Title;
            existingTask.StatusId = userTask.StatusId;
            existingTask.AppointedId = userTask.AppointedId;
            existingTask.AssignedId = userTask.AssignedId;
            existingTask.DueDate = userTask.DueDate;
            existingTask.ExpectedDueDate = userTask.ExpectedDueDate;
            existingTask.Description = userTask.Description;

            try
            {
                await _unitOfWork.CompleteAsync();

                return OperationResponse<UserTask>.CreateSuccesResponse(existingTask);
            }
            catch (Exception ex)
            {
                return OperationResponse<UserTask>.CreateFailure($"Görev güncellenirken bir hata meydana geldi{ex.Message}");
            }
        }
    }
}
