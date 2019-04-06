using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.data;
using timesheet.dto;
using timesheet.model;

namespace timesheet.business
{
    public interface IEmployeeService
    {
        IList<EmployeeDTO> GetEmployees();
    }
}
