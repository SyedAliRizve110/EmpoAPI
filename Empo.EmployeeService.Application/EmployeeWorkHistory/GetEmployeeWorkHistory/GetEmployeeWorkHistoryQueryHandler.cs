using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeWorkHistory.GetEmployeeWorkHistory
{
    public class GetEmployeeWorkHistoryQueryHandler : IQueryHandler<GetEmployeeWorkHistoryQuery, List<EmployeeWorkHistoryModel>>
    {
        public IEmployeeWorkHistoryService _service { get; }

        public GetEmployeeWorkHistoryQueryHandler(IEmployeeWorkHistoryService service)
        {
            _service = service;
        }

        public async Task<List<EmployeeWorkHistoryModel>> Handle(GetEmployeeWorkHistoryQuery request, CancellationToken cancellationToken)
        {
            var workHistory = await _service.GetEmployeeWorkHistoryList(request._employeeId);
            return workHistory;
        }
    }
}
