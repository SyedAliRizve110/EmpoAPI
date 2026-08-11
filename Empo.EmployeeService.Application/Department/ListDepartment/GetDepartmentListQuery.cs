using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Department.ListDepartment;

public class GetDepartmentListQuery : IQuery<GetDepartmentListResponse>
{
    public GetDepartmentListRequest request { get; private set; }

    public GetDepartmentListQuery()
    { }

    public static GetDepartmentListQuery Get(GetDepartmentListRequest request)
    {
        return new GetDepartmentListQuery()
        {
            request = request
        };
    }

}
