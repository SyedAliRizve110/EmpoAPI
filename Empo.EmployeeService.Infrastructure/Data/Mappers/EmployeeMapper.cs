using AutoMapper;
using Empo.EmployeeService.Application.Attendence;
using Empo.EmployeeService.Application.AuthService;
using Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;
using Empo.EmployeeService.Application.AuthService.Permission.PermissionList;
using Empo.EmployeeService.Application.AuthService.Role.CreateRole;
using Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;
using Empo.EmployeeService.Application.AuthService.Role.RoleList;
using Empo.EmployeeService.Application.AuthService.Role.UpdateRole;
using Empo.EmployeeService.Application.Branch;
using Empo.EmployeeService.Application.Branch.CreateBranch;
using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Department;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.GetDepartmentDetails;
using Empo.EmployeeService.Application.Department.UpdateDepartment;
using Empo.EmployeeService.Application.Designations;
using Empo.EmployeeService.Application.Designations.CreateDesignation;
using Empo.EmployeeService.Application.Designations.UpdateDesignation;
using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Department;
using Empo.EmployeeService.Infrastructure.Data.Entities.Designation;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;

namespace Empo.EmployeeService.Infrastructure.Data.Mappers;

public class EmployeeMapper : Profile
{
    public EmployeeMapper()
    {
        #region Employee
        CreateMap<EmployeeModel, EmployeeEntity>().ReverseMap();
        CreateMap<EmployeePhoneModel, EmployeePhoneEntity>().ReverseMap();
        CreateMap<EmployeeAttendanceModel, EmployeeAttendanceEntity>().ReverseMap();
        CreateMap<AddressModel, AddressEntity>().ReverseMap();

        CreateMap<CreateEmployeeRequestModel, EmployeeEntity>().ReverseMap();
        CreateMap<CreateEmployeeAddressModel, AddressEntity>().ReverseMap();
        CreateMap<CreateEmployeePhoneModel, EmployeePhoneEntity>().ReverseMap();

        #endregion

        #region Department
        CreateMap<DepartmentModel, DepartmentEntity>().ReverseMap();
        CreateMap<CreateDepartmentRequest, DepartmentEntity>().ReverseMap();
        CreateMap<GetDepartmentDetailsResponse, DepartmentEntity>().ReverseMap();
        CreateMap<UpdateDepartmentRequestModel, DepartmentEntity>().ReverseMap();

        #endregion

        #region Designation
        CreateMap<DesignationEntity, DesignationModel>().ReverseMap();
        CreateMap<CreateDesignationRequest, DesignationEntity>().ReverseMap();
        CreateMap<UpdateDesignationRequest, DesignationEntity>().ReverseMap();

        #endregion

        #region Branch
        CreateMap<BranchEntity, BranchModel>().ReverseMap();
        CreateMap<CreateBranchRequest, BranchEntity>().ReverseMap();
        CreateMap<CreateBranchAddressModel, AddressEntity>().ReverseMap();

        #endregion

        #region User
        CreateMap<RefreshTokenEntity, RefreshTokenModel>().ReverseMap();
        CreateMap<UserEntity, UserModel>().ReverseMap();
        #endregion

        #region Role
        CreateMap<RoleEntity, RoleModel>().ReverseMap();
        CreateMap<Role_PermissionEntity, Role_PermissionModel>().ReverseMap();
        CreateMap<Role_PermissionEntity, Role_PermissionModel>().ReverseMap();
        CreateMap<GetRoleDetailResponse, RoleEntity>().ReverseMap();
        CreateMap<CreateRoleRequest, RoleEntity>().ReverseMap();
        CreateMap<UpdateRoleRequest, RoleEntity>().ReverseMap();
        CreateMap<RoleResponseModel, RoleEntity>().ReverseMap();
        #endregion

        #region Permission
        CreateMap<CreatePermissionRequest, PermissionEntity>().ReverseMap();
        CreateMap<PermissionEntity, PermissionResponseModel>().ReverseMap();
        CreateMap<PermissionEntity, PermissionModel>().ReverseMap();
        #endregion
    }
}