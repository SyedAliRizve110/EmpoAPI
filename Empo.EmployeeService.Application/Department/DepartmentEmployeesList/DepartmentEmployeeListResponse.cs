namespace Empo.EmployeeService.Application.Department.DepartmentEmployeesList;

public class DepartmentEmployeeListResponse
{
    public IEnumerable<DepartmentEmployeeModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}

public class DepartmentEmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}