using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using timesheet.business;
using timesheet.dto;

namespace timesheet.api.controllers
{
    [Route("api/v1/Worklog")]
    [ApiController]
    public class WorklogController : ControllerBase
    {
        private readonly IWorklogService worklogService;

        public WorklogController(IWorklogService worklogService)
        {
            this.worklogService = worklogService;
        }

        [HttpGet("{employeeId}/{startDate}/{endDate}")]
        public IActionResult Get(int employeeId, DateTime startDate, DateTime endDate)
        {
            var items = this.worklogService.GetEmployeeWorklogs(employeeId, startDate, endDate);
            return new ObjectResult(items);
        }

        [HttpPost("save")]
        public IActionResult Save([FromBody] List<WorklogDTO> worklogs)
        {
            this.worklogService.SaveWorklogs(worklogs);
            return new ObjectResult(true);
        }
    }
}