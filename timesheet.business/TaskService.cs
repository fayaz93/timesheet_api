using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.data;
using timesheet.dto;
using timesheet.model;

namespace timesheet.business
{
    public class TaskService: ITaskService
    {
        public TimesheetDb db { get; }

        public TaskService(TimesheetDb dbContext)
        {
            this.db = dbContext;
        }

        public IList<TaskDTO> GetTasks()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Task, TaskDTO>();
            });
            var mapper = config.CreateMapper();

            var taskModel = this.db.Tasks;
            return mapper.Map<IList<TaskDTO>>(taskModel);
        }
    }
}
