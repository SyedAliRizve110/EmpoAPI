using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "employee");

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address1 = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Address2 = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    City = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    State = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    ZipCode = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EmployeeRole = table.Column<int>(type: "integer", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employee_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "employee",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendence",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    ClockInTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ClockOutTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    WorkedMinutes = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClockInIP = table.Column<string>(type: "text", nullable: true),
                    ClockOutIP = table.Column<string>(type: "text", nullable: true),
                    ClockInDevice = table.Column<string>(type: "text", nullable: true),
                    ClockOutDevice = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendence_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CountryCode = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phone_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendence_EmployeeId",
                schema: "employee",
                table: "Attendence",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_AddressId",
                schema: "employee",
                table: "Employee",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_EmployeeId",
                schema: "employee",
                table: "Phone",
                column: "EmployeeId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendence",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Phone",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Employee",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Address",
                schema: "employee");
        }
    }
}
