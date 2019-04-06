using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.data;
using timesheet.dto;
using timesheet.model;

namespace timesheet.business
{
    public class WorklogService: IWorklogService
    {
        public TimesheetDb db { get; }

        public WorklogService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public EmployeeWorklogDTO GetEmployeeWorklogs(int employeeId, DateTime startDate, DateTime endDate)
        {
            return new EmployeeWorklogDTO
            {
                EmployeeId = employeeId,
                StartDate = startDate,
                EndDate = endDate,
                Tasks = GetEmployeeWorklogTasks(employeeId, startDate, endDate)
            };
        }

        private IList<EmployeeTaskDTO> GetEmployeeWorklogTasks(int employeeId, DateTime startDate, DateTime endDate)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Worklog, WorklogDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Task, TaskDTO>();
            });
            var mapper = config.CreateMapper();

            var worklogModel = this.db.Worklog.Where(w => w.EmployeeId == employeeId && w.Date >= startDate && w.Date <= endDate)
                                .Include(x => x.Employee).Include(x => x.Task);
            var worklogDto =  mapper.Map<IList<WorklogDTO>>(worklogModel);

            var tasks = worklogDto.Select(w => w.Task).Distinct();
            var result = new List<EmployeeTaskDTO>();
            foreach (var task in tasks)
            {
                result.Add(new EmployeeTaskDTO
                {
                    TaskId = task.Id,
                    TaskName = task.Name,
                    Day1Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date).Sum( w => w.Hours),
                    Day2Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(1)).Sum(w => w.Hours),
                    Day3Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(2)).Sum(w => w.Hours),
                    Day4Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(3)).Sum(w => w.Hours),
                    Day5Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(4)).Sum(w => w.Hours),
                    Day6Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(5)).Sum(w => w.Hours),
                    Day7Effort = worklogDto.Where(w => w.TaskId == task.Id && w.Date.Date == startDate.Date.AddDays(6)).Sum(w => w.Hours),
                });
            }
            return result;
        }

        public IList<WorklogDTO> GetWorklogs(int employeeId, DateTime? startDate, DateTime? endDate)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Worklog, WorklogDTO>();
                cfg.CreateMap<Employee, EmployeeDTO>();
                cfg.CreateMap<Task, TaskDTO>();
            });
            var mapper = config.CreateMapper();

            var worklogModel = this.db.Worklog.Where(w => w.EmployeeId == employeeId && (startDate == null || w.Date >= startDate) && w.Date <= endDate);
            return mapper.Map<IList<WorklogDTO>>(worklogModel);
        }

        public void SaveWorklogs(List<WorklogDTO> worklogs)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<WorklogDTO, Worklog>();
                cfg.CreateMap<EmployeeDTO, Employee>();
                cfg.CreateMap<TaskDTO, Task>();
            });
            var mapper = config.CreateMapper();

            var workLogEntities = mapper.Map<IList<Worklog>>(worklogs);
            this.db.Worklog.AddRange(workLogEntities);
            this.db.SaveChanges();
        }
    }
}
