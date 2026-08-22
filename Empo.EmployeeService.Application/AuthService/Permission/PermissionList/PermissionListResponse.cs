namespace Empo.EmployeeService.Application.AuthService.Permission.PermissionList;

public class PermissionListResponse
{
    public IEnumerable<PermissionResponseModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}

public class PermissionResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
