using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using timesheet.data;
using timesheet.dto;
using timesheet.model;

namespace timesheet.business
{
    public class EmployeeService: IEmployeeService
    {
        public TimesheetDb db { get; }
        public IWorklogService worklogService;

        public EmployeeService(TimesheetDb dbContext, IWorklogService worklogService)
        {
            this.db = dbContext;
            this.worklogService = worklogService;
        }

        public IList<EmployeeDTO> GetEmployees()
        {
            var weekDeatils = GetWeekDetails(DateTime.Today);
            return GetEmployees(weekDeatils.startDate, weekDeatils.endDate);
        }

        public IList<EmployeeDTO> GetEmployees(DateTime startDate, DateTime endDate)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeDTO>();
            });
            var mapper = config.CreateMapper();

            var empModel = this.db.Employees;
            var empDTO = mapper.Map<IList<EmployeeDTO>>(empModel);

            foreach (var emp in empDTO)
            {
                emp.StartDate = startDate;
                emp.EndDate = endDate;
                emp.TotalWeeklyEffort = GetTotalWeeklyEffort(emp.Id, startDate, endDate);
                emp.AvgWeeklyEffort = GetAvgWeeklyEffort(emp.Id, endDate);
            }

            return empDTO;
        }

        private (DateTime startDate, DateTime endDate) GetWeekDetails(DateTime date)
        {
            var startDate = date.AddDays(DayOfWeek.Sunday - date.DayOfWeek);
            var endDate = startDate.AddDays(6);
            return (startDate, endDate);
        }

        private decimal? GetTotalWeeklyEffort(int empId, DateTime startDate, DateTime endDate)
        {
            var x = worklogService.GetWorklogs(empId, startDate, endDate);
            return decimal.Round(x.Sum(t => t.Hours) / 7, 2, MidpointRounding.AwayFromZero);
        }

        private decimal? GetAvgWeeklyEffort(int empId, DateTime endDate)
        {
            var x = worklogService.GetWorklogs(empId, null, endDate);
            return decimal.Round(x.Sum(t => t.Hours) / (decimal)(endDate - new DateTime(endDate.Year, 1, 1)).TotalDays, 2, MidpointRounding.AwayFromZero);
        }
    }
}
