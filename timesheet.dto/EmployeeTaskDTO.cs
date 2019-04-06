using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.dto
{
    public class EmployeeTaskDTO
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public decimal Day1Effort { get; set; }

        public decimal Day2Effort { get; set; }

        public decimal Day3Effort { get; set; }

        public decimal Day4Effort { get; set; }

        public decimal Day5Effort { get; set; }

        public decimal Day6Effort { get; set; }

        public decimal Day7Effort { get; set; }
    }
}
