using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Employees.GetEmployeeList;

public class GetEmployeeListQuery : IQuery<GetEmployeeListResponse>
{
    public GetEmployeeListRequest request { get; private set; }

    public GetEmployeeListQuery()
    { }

    public static GetEmployeeListQuery Create(GetEmployeeListRequest request)
    {
        return new GetEmployeeListQuery()
        {
            request = request
        };
    }
}
