using Empo.EmloyeeService.Api.Authorization;
using Empo.EmployeeService.Api.Jwt;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Application.EmployeeBank;
using Empo.EmployeeService.Application.EmployeeEducation;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.EmployeeWorkHistory;
using Empo.EmployeeService.Application.Interface;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Empo.EmployeeService.Infrastructure.Data.Repositories;
using Empo.EmployeeService.Infrastructure.Data.Repositories.AuthRepository;
using Empo.EmployeeService.Infrastructure.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Empo.EmloyeeService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration
        )
    {
        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
        services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
        services.AddScoped<PasswordHasher<UserEntity>>();
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IEmployeeService, EmployeeRepository>();
        services.AddScoped<IAttendanceService, AttendanceRepository>();
        services.AddScoped<IDepartmentService, DepartmentRepository>();
        services.AddScoped<IDesignationService, DesignationRepository>();
        services.AddScoped<IBranchService, BranchRepository>();
        services.AddScoped<IUserService, UserRepository>();
        services.AddScoped<IPermissionService, PermissionRepository>();
        services.AddScoped<IRoleService, RoleRepository>();
        services.AddScoped<IEmployeeBankService, EmployeeBankRepository>();
        services.AddScoped<IEmployeeWorkHistoryService, EmployeeWorkHistoryRepository>();
        services.AddScoped<IEmployeeEducationService, EmployeeEducationRepository>();

        return services;
    }
}
