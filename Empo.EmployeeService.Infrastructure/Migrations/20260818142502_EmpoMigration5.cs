using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmpoMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BranchId",
                schema: "employee",
                table: "Employee",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Branch",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    BranchCode = table.Column<string>(type: "text", nullable: false),
                    AddressId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ManagerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Branch_Address_AddressId",
                        column: x => x.AddressId,
                        principalSchema: "employee",
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_BranchId",
                schema: "employee",
                table: "Employee",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_Branch_AddressId",
                schema: "employee",
                table: "Branch",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Branch_BranchId",
                schema: "employee",
                table: "Employee",
                column: "BranchId",
                principalSchema: "employee",
                principalTable: "Branch",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Branch_BranchId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropTable(
                name: "Branch",
                schema: "employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_BranchId",
                schema: "employee",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "employee",
                table: "Employee");
        }
    }
}
