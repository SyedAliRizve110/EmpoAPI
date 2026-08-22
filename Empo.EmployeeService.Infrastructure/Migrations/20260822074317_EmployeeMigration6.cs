using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Empo.EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeMigration6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "employee",
                table: "Designation",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "employee",
                table: "Branch",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                schema: "employee",
                table: "Branch",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<string>(
                name: "BranchCode",
                schema: "employee",
                table: "Branch",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "employee",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "employee",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "employee",
                        principalTable: "Employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_User_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "employee",
                        principalTable: "Role",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "employee",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModifieed = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: false),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_User_UserId",
                        column: x => x.UserId,
                        principalSchema: "employee",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "Address",
                columns: new[] { "Id", "Address1", "Address2", "City", "Country", "CreatedBy", "DateCreated", "DateModifieed", "ModifiedBy", "State", "ZipCode" },
                values: new object[] { new Guid("0f45a845-485c-41c6-a2e8-ede512c2cba5"), "22k/5b", "Kareli", "Prayagraj", "India", new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), "UP", "211016" });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "Permission",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModifieed", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("7845a4bc-aebb-4627-b307-558342fab6b5"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee Permission", true, new Guid("00000000-0000-0000-0000-000000000000"), "Employee.Get" },
                    { new Guid("8be3f794-30f4-485d-907a-78f389884cda"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee Permission", true, new Guid("00000000-0000-0000-0000-000000000000"), "Employee.Update" },
                    { new Guid("e01e7324-2d10-45ed-b8b7-a15ced9320c5"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee Permission", true, new Guid("00000000-0000-0000-0000-000000000000"), "Employee.Create" },
                    { new Guid("f5079067-b225-4a2f-a876-ce01cc41f6cf"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee Permission", true, new Guid("00000000-0000-0000-0000-000000000000"), "Employee.List" }
                });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "Role",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModifieed", "Description", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { new Guid("30f6262e-0b43-481a-a284-3f060c924525"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Department based access", true, new Guid("00000000-0000-0000-0000-000000000000"), "Manager" },
                    { new Guid("364b4583-c5cf-468e-a269-fbef8e683738"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "self service access", true, new Guid("00000000-0000-0000-0000-000000000000"), "Employee" },
                    { new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full access", true, new Guid("00000000-0000-0000-0000-000000000000"), "Admin" },
                    { new Guid("eaaf0947-b11c-4c4c-ad36-66facc063803"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Partial access", true, new Guid("00000000-0000-0000-0000-000000000000"), "HR" }
                });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "Employee",
                columns: new[] { "Id", "AddressId", "BranchId", "CreatedBy", "DateCreated", "DateModifieed", "DateOfBirth", "DepartmentId", "DesignationId", "Email", "EmployeeRole", "FirstName", "IsActive", "LastName", "ModifiedBy" },
                values: new object[] { new Guid("31f29e8c-1ccb-400b-99f8-9d03194ebc23"), new Guid("0f45a845-485c-41c6-a2e8-ede512c2cba5"), null, new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2000, 2, 1), null, null, "alirizvi9721@gmail.com", 1, "Mohd", true, "Ali Rizvi", new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "Role_Permission",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModifieed", "ModifiedBy", "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("0a396c94-6ded-4a69-a472-8c8968a957b1"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("7845a4bc-aebb-4627-b307-558342fab6b5"), new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c") },
                    { new Guid("812a175a-4342-44cb-95c4-11914d3038eb"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("e01e7324-2d10-45ed-b8b7-a15ced9320c5"), new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c") },
                    { new Guid("a127cfbf-7643-4905-b5b1-0a2cb6766cd1"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("8be3f794-30f4-485d-907a-78f389884cda"), new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c") },
                    { new Guid("b759aee7-aeb8-429c-ac1a-31e203c38c96"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("00000000-0000-0000-0000-000000000000"), new Guid("f5079067-b225-4a2f-a876-ce01cc41f6cf"), new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c") }
                });

            migrationBuilder.InsertData(
                schema: "employee",
                table: "User",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateModifieed", "Email", "EmailConfirmed", "EmployeeId", "IsActive", "LastLoginAt", "ModifiedBy", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RoleId", "UserName" },
                values: new object[] { new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new Guid("c0138cd8-98c8-44c1-aa79-ac489e57d1b5"), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@empo.com", true, new Guid("31f29e8c-1ccb-400b-99f8-9d03194ebc23"), true, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000000"), "$2a$12$GHDrilbduUGPyqdyNMuueOz2ervhawUXCCkF32Ve2.Ezpq2M2yuga", "9721974817", true, new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c"), "admin1" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "employee",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_PermissionId",
                schema: "employee",
                table: "Role_Permission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_RoleId",
                schema: "employee",
                table: "Role_Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_User_EmployeeId",
                schema: "employee",
                table: "User",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_User_RoleId",
                schema: "employee",
                table: "User",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Role_Permission",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "User",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "employee");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "employee");

            migrationBuilder.DeleteData(
                schema: "employee",
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("31f29e8c-1ccb-400b-99f8-9d03194ebc23"));

            migrationBuilder.DeleteData(
                schema: "employee",
                table: "Address",
                keyColumn: "Id",
                keyValue: new Guid("0f45a845-485c-41c6-a2e8-ede512c2cba5"));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "employee",
                table: "Designation",
                type: "character varying(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "employee",
                table: "Branch",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<Guid>(
                name: "ManagerId",
                schema: "employee",
                table: "Branch",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BranchCode",
                schema: "employee",
                table: "Branch",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }
    }
}
