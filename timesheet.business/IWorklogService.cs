using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.data;
using timesheet.dto;
using timesheet.model;

namespace timesheet.business
{
    public interface IWorklogService
    {
        EmployeeWorklogDTO GetEmployeeWorklogs(int employeeId, DateTime startDate, DateTime endDate);
        IList<WorklogDTO> GetWorklogs(int employeeId, DateTime? startDate, DateTime? endDate);
        void SaveWorklogs(List<WorklogDTO> worklogs);
    }
}
