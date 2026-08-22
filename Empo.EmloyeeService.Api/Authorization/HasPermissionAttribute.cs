using Microsoft.AspNetCore.Authorization;

namespace Empo.EmloyeeService.Api.Authorization;

// Usage: [HasPermission(Permissions.EmployeeCreate)]
public class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission) : base(policy: PermissionPolicyProvider.PolicyPrefix + permission)
    {

    }
}

//cemtral list of every permission string in the system.
public static class Permissions
{
    #region Employee
    public const string EmployeeCreate = "Employee.Create";
    public const string EmployeeGet = "Employee.Get";
    public const string EmployeeUpdate = "Employee.Update";
    public const string EmployeeList = "Employee.List";
    #endregion

    #region Attendence
    public const string AttendenceClockIn = "Attendence.ClockIn";
    public const string AttendenceClockOut = "Attendence.ClockOut";
    public const string AttendenceList = "Attendence.List";
    public const string AttendenceActive = "Attendence.Active-Employee";

    #endregion

    #region Branch
    public const string BranchCreate = "Branch.Create";
    public const string BranchGet = "Branch.Get";
    public const string BranchUpdate = "Branch.Update";
    public const string BranchList = "Branch.List";
    public const string BranchEmployeeList = "Branch.Employee-List";
    public const string BranchAssignEmoloyee = "Branch.Assign-Employee";
    public const string BranchAssignManager = "Branch.Assign-Manager";

    #endregion

    #region Department
    public const string DepartmentCreate = "Department.Create";
    public const string DepartmentGet = "Department.Get";
    public const string DepartmentUpdate = "Department.Update";
    public const string DepartmentList = "Department.List";
    public const string DepartmentEmployeeList = "Department.Employee-List";
    public const string DepartmentAssignEmoloyee = "Department.Assign-Employee";
    public const string DepartmentAssignManager = "Department.Assign-Manager";

    #endregion

    #region Designation
    public const string DesignationCreate = "Designation.Create";
    public const string DesignationGet = "Designation.Get";
    public const string DesignationUpdate = "Designation.Update";
    public const string DesignationList = "Designation.List";
    public const string DesignationEmployeeList = "Designation.Employee-List";
    public const string DesignationAssignDesignation = "Designation.Assign-Designation";

    #endregion

    #region Permission
    public const string PermissionCreate = "Permission.Create";
    public const string PermissionList = "Permission.List";

    #endregion

    #region Role
    public const string RoleCreate = "Role.Create";
    public const string RoleGet = "Role.Get";
    public const string RoleUpdate = "Role.Update";
    public const string RoleList = "Role.List";
    public const string RoleStatus = "Role.Status";
    public const string AssignPermission = "Role.Assign-Permission";
    public const string RevokePermission = "Role.Revoke-Permission";
    public const string AssignUser = "Role.Assign-User";

    #endregion
}