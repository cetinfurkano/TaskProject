using CaseStudy.Business.Abstract;
using CaseStudy.Business.ValidationRules;
using CaseStudy.Core.Communication;
using CaseStudy.Core.DataAccess;
using CaseStudy.Core.Validation;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy.Business.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUserDal userDal, IUnitOfWork unitOfWork)
        {
            _userDal = userDal;
            _unitOfWork = unitOfWork;
        }


        public async Task<OperationResponse<User>> AddAsync(User user)
        {

            var validationResult = ValidationTool.Validate(new UserValidator(), user);
            if (!validationResult.IsValid)
            {
                return OperationResponse<User>.CreateFailure("Girdiğiniz veriler geçersiz.");
            }

            try
            {
                await _userDal.AddAsync(user);
                await _unitOfWork.CompleteAsync();

                return OperationResponse<User>.CreateSuccesResponse(user);
            }
            catch (Exception ex)
            {
                return OperationResponse<User>.CreateFailure($"Kullanıcı eklenirken bir hata meydana geldi: {ex.Message}");
            }
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            return await _userDal.GetAllAsync(filter);
        }

        public async Task<OperationResponse<User>> GetByIdAsync(int id)
        {
            var result = await _userDal.GetAsync(u => u.Id == id); 

            if(result == null)
            {
                return OperationResponse<User>.CreateFailure("Böyle bir kullanıcı bulunamadı.");
            }
            return OperationResponse<User>.CreateSuccesResponse(result);
        }

        public async Task<OperationResponse<User>> RemoveAsync(User user)
        {
            var existingUser = await _userDal.GetAsync(u => u.Id == user.Id);
            if(existingUser == null)
            {
                return OperationResponse<User>.CreateFailure("Kullanıcı bulunamadı");
            }

            try
            {
                _userDal.Remove(existingUser);
                await _unitOfWork.CompleteAsync();

                return OperationResponse<User>.CreateSuccesResponse(existingUser);
            }
            catch (Exception ex)
            {

                return OperationResponse<User>.CreateFailure($"Silmeye çalışırken bir hata meydana geldi{ex.Message}");
            }
        }

        public async Task<OperationResponse<User>> UpdateAsync(User user)
        {
            var validationResult = ValidationTool.Validate(new UserValidator(), user);
            if (!validationResult.IsValid)
            {
                return OperationResponse<User>.CreateFailure("Girdiğiniz veriler yanlış!");
            }

            var existingUser = await _userDal.GetAsync(u => u.Id == user.Id);
            if (existingUser == null)
            {
                return OperationResponse<User>.CreateFailure("Böyle bir kullanıcı bulunamadı");
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;
            try
            {
                await _unitOfWork.CompleteAsync();

                return OperationResponse<User>.CreateSuccesResponse(existingUser);
            }
            catch (Exception ex)
            {
                return OperationResponse<User>.CreateFailure($"Güncelleme esnasında br hata oluştu:{ex.Message}");
            }

        }
    }
}
