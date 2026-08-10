using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;

namespace Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;

public class ActiveEmployeeListQueryHandler : IQueryHandler<ActiveEmployeeListQuery, ActiveEmployeeListResponse>
{
    public IAttendanceService _repo { get; }

    public ActiveEmployeeListQueryHandler(IAttendanceService repo)
    {
        _repo = repo;
    }

    public async Task<ActiveEmployeeListResponse> Handle(ActiveEmployeeListQuery query, CancellationToken cancellationToken)
    {
        var response = await _repo.ActiveEmployeeListAsync(query.request);
        return response;
    }
}
