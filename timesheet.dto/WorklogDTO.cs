using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.dto
{
    public class WorklogDTO
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public decimal Hours { get; set; }

        public EmployeeDTO Employee { get; set; }

        public int TaskId { get; set; }

        public TaskDTO Task { get; set; }
    }
}
