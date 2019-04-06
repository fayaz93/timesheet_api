using System;

namespace timesheet.dto
{
    public class EmployeeDTO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime StartDate{ get; set; }

        public DateTime EndDate { get; set; }

        public decimal? TotalWeeklyEffort { get; set; }

        public decimal? AvgWeeklyEffort { get; set; }
    }
}
