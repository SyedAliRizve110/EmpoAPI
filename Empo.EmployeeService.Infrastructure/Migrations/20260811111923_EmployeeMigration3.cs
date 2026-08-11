using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId",
                schema: "employee",
                table: "Employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Department",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DepartmentId",
                schema: "employee",
                table: "Employee",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                schema: "employee",
                table: "Employee",
                column: "DepartmentId",
                principalSchema: "employee",
                principalTable: "Department",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Department_DepartmentId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Department",
                schema: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DepartmentId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                schema: "employee",
                table: "Employee");
        }
    }
}
