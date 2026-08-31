namespace Empo.EmployeeService.Application.Department.ListDepartment;

public class GetDepartmentListResponse
{
    public IEnumerable<DepartmentModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}
