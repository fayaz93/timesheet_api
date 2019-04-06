using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace timesheet.dto
{
    public class EmployeeWorklogDTO
    {
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime PrevStartDate
        {
            get
            {
                return StartDate.AddDays(-7);
            }
        }

        public DateTime PrevEndDate
        {
            get
            {
                return PrevStartDate.AddDays(6);
            }
        }

        public DateTime NextStartDate
        {
            get
            {
                return EndDate.AddDays(1);
            }
        }
        public DateTime NextEndDate
        {
            get
            {
                return NextStartDate.AddDays(6);
            }
        }

        public IList<EmployeeTaskDTO> Tasks { get; set; }

        public IList<string> Days
        {
            get
            {
                var days = new List<string>();
                for (int i = 0; i < 7; i++)
                {
                    var date = StartDate.AddDays(i);
                    days.Add($"{date.ToString("yyyy-MM-dd")}");
                }
                return days;
            }
        }
    }
}
