using Microsoft.EntityFrameworkCore.Migrations;

namespace timesheet.data.Migrations
{
    public partial class Worklog_seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                INSERT INTO Worklog Values(1, 1, '2019-04-09', 2)
                INSERT INTO Worklog Values(1, 2, '2019-04-09', 1)
                INSERT INTO Worklog Values(1, 3, '2019-04-09', 2)
                INSERT INTO Worklog Values(1, 4, '2019-04-09', 2)
                INSERT INTO Worklog Values(1, 5, '2019-04-09', 2)
                  GO  ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
