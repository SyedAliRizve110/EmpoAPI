using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmpMigrations2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkedMinutes",
                schema: "employee",
                table: "Attendence");

            migrationBuilder.AddColumn<double>(
                name: "WorkedHours",
                schema: "employee",
                table: "Attendence",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkedHours",
                schema: "employee",
                table: "Attendence");

            migrationBuilder.AddColumn<int>(
                name: "WorkedMinutes",
                schema: "employee",
                table: "Attendence",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
