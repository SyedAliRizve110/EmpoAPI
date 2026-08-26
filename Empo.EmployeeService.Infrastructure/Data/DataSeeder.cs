using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data;

public static class DataSeeder
{
    public static void SeedDataBase(this ModelBuilder modelBuilder)
    {
        SeedRoleData(modelBuilder);
        CreateAdmin(modelBuilder);
        SeedPermissionData(modelBuilder);
    }

    //Creating Roles
    private static void SeedRoleData(this ModelBuilder builder)
    {
        builder.Entity<RoleEntity>().HasData(new RoleEntity { Id = new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c"), Name = "Admin", Description = "Full access", IsActive = true, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = Constants.AdminUserId });
        builder.Entity<RoleEntity>().HasData(new RoleEntity { Id = new Guid("eaaf0947-b11c-4c4c-ad36-66facc063803"), Name = "HR", Description = "Partial access", IsActive = true, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = Constants.AdminUserId });
        builder.Entity<RoleEntity>().HasData(new RoleEntity { Id = new Guid("30f6262e-0b43-481a-a284-3f060c924525"), Name = "Manager", Description = "Department based access", IsActive = true, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = Constants.AdminUserId });
        builder.Entity<RoleEntity>().HasData(new RoleEntity { Id = new Guid("364b4583-c5cf-468e-a269-fbef8e683738"), Name = "Employee", Description = "self service access", IsActive = true, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc), CreatedBy = Constants.AdminUserId });
    }
    // Creating admin
    private static void CreateAdmin(this ModelBuilder builder)
    {
        //Add Employee
        builder.Entity<EmployeeEntity>().HasData(new EmployeeEntity
        {
            Id = Constants.AdminEmployeeId,
            FirstName = "Mohd",
            LastName = "Ali Rizvi",
            Email = "alirizvi9721@gmail.com",
            IsActive = true,
            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedBy = Constants.AdminUserId,
            DateOfBirth = new DateOnly(2000, 2, 1),
            EmployeeRole = EmployeeRoleEnum.Admin,
            AddressId = new Guid("0f45a845-485c-41c6-a2e8-ede512c2cba5")
        });

        // Add address
        builder.Entity<AddressEntity>().HasData(new AddressEntity
        {
            Id = new Guid("0f45a845-485c-41c6-a2e8-ede512c2cba5"),
            Address1 = "22k/5b",
            Address2 = "Kareli",
            City = "Prayagraj",
            State = "UP",
            Country = "India",
            ZipCode = "211016",
            CreatedBy = Constants.AdminUserId,
            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
        builder.Entity<EmployeePhoneEntity>().HasData(new EmployeePhoneEntity
        {
            Id = new Guid("22191c8f-6ba1-477a-bfd7-43daac6fa2bc"),
            EmployeeId = Constants.AdminEmployeeId,
            CountryCode = "+91",
            Number = "9721974817",
            CreatedBy = Constants.AdminUserId,
            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        //Add User
        builder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = Constants.AdminUserId,
            Email = "admin@empo.com",
            UserName = "admin1",
            IsActive = true,
            DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            CreatedBy = Constants.AdminUserId,
            EmailConfirmed = true,
            PhoneNumber = "9721974817",
            PhoneNumberConfirmed = true,
            PasswordHash = "$2a$12$GHDrilbduUGPyqdyNMuueOz2ervhawUXCCkF32Ve2.Ezpq2M2yuga", //password = admin@123
            RoleId = new Guid("e4b49093-ab55-47a2-91ef-1681372c7e6c"),
            EmployeeId = Constants.AdminEmployeeId,
            LastLoginAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });

        //User role
        //builder.Entity<User_RoleEntity>().HasData(
        //    new User_RoleEntity
        //    {
        //        UserId = Constants.AdminUserId,
        //        RoleId = Constants.AdminRoleId,
        //        CreatedBy = Constants.AdminUserId,
        //        DateCreated = DateTime.Now,
        //        Id = new Guid("74fa61db-fd04-4c57-9dbb-ee733d87237a")
        //    }
        //    );

        //Role permissions

        // add
        builder.Entity<Role_PermissionEntity>().HasData(
            new Role_PermissionEntity
            {
                Id = new Guid("812a175a-4342-44cb-95c4-11914d3038eb"),
                RoleId = Constants.AdminRoleId,
                PermissionId = Constants.AddEmployeePermissionId,
                CreatedBy = Constants.AdminUserId,
                DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            });

        //get
        builder.Entity<Role_PermissionEntity>().HasData(
    new Role_PermissionEntity
    {
        Id = new Guid("0a396c94-6ded-4a69-a472-8c8968a957b1"),
        RoleId = Constants.AdminRoleId,
        PermissionId = Constants.GetEmployeePermissionId,
        CreatedBy = Constants.AdminUserId,
        DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    });

        //update
        builder.Entity<Role_PermissionEntity>().HasData(
    new Role_PermissionEntity
    {
        Id = new Guid("a127cfbf-7643-4905-b5b1-0a2cb6766cd1"),
        RoleId = Constants.AdminRoleId,
        PermissionId = Constants.UpdateEmployeePermissionId,
        CreatedBy = Constants.AdminUserId,
        DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    });

        //list
        builder.Entity<Role_PermissionEntity>().HasData(
    new Role_PermissionEntity
    {
        Id = new Guid("b759aee7-aeb8-429c-ac1a-31e203c38c96"),
        RoleId = Constants.AdminRoleId,
        PermissionId = Constants.ListEmployeePermissionId,
        CreatedBy = Constants.AdminUserId,
        DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    });
    }

    //Create Permissions
    private static void SeedPermissionData(ModelBuilder builder)
    {
        builder.Entity<PermissionEntity>().HasData(
                new PermissionEntity { Id = Constants.AddEmployeePermissionId, Name = "Employee.Create", Description = "Employee Permission", IsActive = true, CreatedBy = Constants.AdminUserId, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PermissionEntity { Id = Constants.GetEmployeePermissionId, Name = "Employee.Get", Description = "Employee Permission", IsActive = true, CreatedBy = Constants.AdminUserId, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PermissionEntity { Id = Constants.UpdateEmployeePermissionId, Name = "Employee.Update", Description = "Employee Permission", IsActive = true, CreatedBy = Constants.AdminUserId, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) },
                new PermissionEntity { Id = Constants.ListEmployeePermissionId, Name = "Employee.List", Description = "Employee Permission", IsActive = true, CreatedBy = Constants.AdminUserId, DateCreated = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc) }
            );
    }
}
