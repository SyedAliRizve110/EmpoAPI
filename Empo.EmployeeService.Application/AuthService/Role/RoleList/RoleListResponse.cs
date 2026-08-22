namespace Empo.EmployeeService.Application.AuthService.Role.RoleList;

public class RoleListResponse
{
    public IEnumerable<RoleResponseModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}

public class RoleResponseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}