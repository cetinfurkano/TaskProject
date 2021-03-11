using AutoMapper;
using CaseStudy.Business.Abstract;
using CaseStudy.DataAccess.Abstract;
using CaseStudy.Entity.Concrete;
using CaseStudy.Entity.Resources;
using CaseStudy.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CaseStudy.WebUI.Controllers
{
    public class TaskController : Controller
    {
        private readonly IUserTaskService _taskService;
        private readonly IUserService _userService;
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        public TaskController(IUserTaskService taskService, IUserService userService, IStatusService statusService, IMapper mapper)
        {
            _taskService = taskService;
            _userService = userService;
            _statusService = statusService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var taskViewModel = new TaskViewModel
            {
                TaskDetails = await _taskService.GetTasksDetailsAsync(),
                Users = await _userService.GetAllAsync(),
                Statuses = await _statusService.GetAllAsync()

            };
            return View(taskViewModel);
        }

        public async Task<IActionResult> GetTaskDetail(int id)
        {
            var result = await _taskService.GetTaskDetailByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Json(result.Resource);
        }

        public async Task<IActionResult> AddTask(UserTaskResource resource)
        {
            var userTask = _mapper.Map<UserTaskResource, UserTask>(resource);
            
            var addResult = await _taskService.AddAsync(userTask);
            if (!addResult.Success)
            {
                return BadRequest(addResult.Message);
            }
            var result = await _taskService.GetTaskDetailByIdAsync(addResult.Resource.Id);
            
            return Json(result.Resource);
        }

        public async Task<IActionResult> UpdateTask(UserTaskResource resource)
        {
            var userTask = _mapper.Map<UserTaskResource, UserTask>(resource);

            var updateResult = await _taskService.UpdateAsync(userTask);
            if (!updateResult.Success)
            {
                return BadRequest(updateResult.Message);
            }
            var result = await _taskService.GetTaskDetailByIdAsync(updateResult.Resource.Id);
            
            return Json(result.Resource);

        }

        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleteResult = await _taskService.RemoveAsync(new UserTask { Id = id});

            if (!deleteResult.Success)
            {
                return BadRequest(deleteResult.Message);
            }
            return Json(deleteResult.Resource);
        }

    }
}
