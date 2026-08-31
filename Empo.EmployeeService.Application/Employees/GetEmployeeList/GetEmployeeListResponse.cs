using Empo.EmployeeService.Application.Employees.EmployeesModel;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeList;

public class GetEmployeeListResponse
{
    public IEnumerable<EmployeeModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}
