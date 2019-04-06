using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace timesheet.data.Migrations
{
    public partial class Worklog_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Worklog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Hours = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Worklog", x => x.Id);                    
                    table.ForeignKey(
                        "FK_Worklog_EmployeeId_Employee_Id",
                        x => x.EmployeeId,
                        "Employees",
                        "Id",
                        "dbo");
                    table.ForeignKey(
                        "FK_Worklog_TaskId_Task_Id",
                        x => x.TaskId,
                        "Tasks",
                        "Id",
                        "dbo");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Worklog");
        }
    }
}
