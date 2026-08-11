using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Department.DepartmentEmployeesList;

public class DepartmentEmployeeListQuery : IQuery<DepartmentEmployeeListResponse>
{
    public DepartmentEmployeeListRequest request { get; private set; }

    public DepartmentEmployeeListQuery()
    { }

    public static DepartmentEmployeeListQuery Get(DepartmentEmployeeListRequest request)
    {
        return new DepartmentEmployeeListQuery()
        {
            request = request
        };
    }
}