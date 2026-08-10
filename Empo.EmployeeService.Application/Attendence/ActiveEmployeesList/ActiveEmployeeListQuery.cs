using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;

public class ActiveEmployeeListQuery : IQuery<ActiveEmployeeListResponse>
{
    public ActiveEmployeeListRequest request { get; private set; }

    public ActiveEmployeeListQuery()
    { }

    public static ActiveEmployeeListQuery Create(ActiveEmployeeListRequest request)
    {
        return new ActiveEmployeeListQuery()
        {
            request = request
        };
    }
}
