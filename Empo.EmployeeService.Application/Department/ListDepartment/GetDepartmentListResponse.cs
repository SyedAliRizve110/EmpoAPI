namespace Empo.EmployeeService.Application.Department.ListDepartment;

public class GetDepartmentListResponse
{
    public IEnumerable<DepartmentModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}
