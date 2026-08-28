using Empo.BuildingBlocks.Application.Contracts;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

namespace Empo.EmployeeService.Application.EmployeeBank.Get;

public class GetEmployeeBankQueryHandler : IQueryHandler<GetEmployeeBankQuery, EmployeeBankModel>
{
    public IEmployeeBankService _service { get; }

    public GetEmployeeBankQueryHandler(IEmployeeBankService service)
    {
        _service = service;
    }

    public async Task<EmployeeBankModel> Handle(GetEmployeeBankQuery request, CancellationToken cancellationToken)
    {
        var bank = await _service.GetEmployeeBank(request._employeeId);
        if (bank == null)
        {
            throw new NotFoundException("bank details", request._employeeId);
        }
        return bank;
    }
}
