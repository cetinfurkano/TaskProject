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
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;

        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddStatus(StatusResource resource)
        {
            var status = _mapper.Map<StatusResource, Status>(resource);

            var addResult = await _statusService.AddAsync(status);
            if (!addResult.Success)
            {
                return BadRequest(addResult.Message);
            }
            return Json(addResult.Resource);
        }
    }
}
