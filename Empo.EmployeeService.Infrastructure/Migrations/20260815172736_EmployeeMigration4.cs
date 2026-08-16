using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DesignationId",
                schema: "employee",
                table: "Employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "employee",
                table: "Department",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "employee",
                table: "Department",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Designation",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designation", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DesignationId",
                schema: "employee",
                table: "Employee",
                column: "DesignationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Designation_DesignationId",
                schema: "employee",
                table: "Employee",
                column: "DesignationId",
                principalSchema: "employee",
                principalTable: "Designation",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Designation_DesignationId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Designation",
                schema: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DesignationId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "employee",
                table: "Department",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "employee",
                table: "Department",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);
        }
    }
}
