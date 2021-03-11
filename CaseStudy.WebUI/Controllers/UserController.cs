using AutoMapper;
using CaseStudy.Business.Abstract;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }


        public async Task<IActionResult> AddUser(SaveUserResource resource)
        {
            var user = _mapper.Map<SaveUserResource, User>(resource);

            var addResult = await _userService.AddAsync(user);
            if (!addResult.Success)
            {
                return BadRequest(addResult.Message);
            }
            return Json(addResult.Resource);
        }
        public async Task<IActionResult> UpdateUser(UpdateUserResource resource)
        {
            var user = _mapper.Map<UpdateUserResource, User>(resource);

            var updateResult = await _userService.UpdateAsync(user);
            if (!updateResult.Success)
            {
                return BadRequest(updateResult.Message);
            }
            return Json(updateResult.Resource);
        }

        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Json(result.Resource);
        }

    }
}
