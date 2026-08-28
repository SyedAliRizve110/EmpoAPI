using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeBank.Create;

public class CreateEmployeeBankCommandHandler : ICommandHandler<CreateEmployeeBankCommand, EmployeeBankDto>
{
    public IEmployeeBankService _service { get; }

    public CreateEmployeeBankCommandHandler(IEmployeeBankService service)
    {
        _service = service;
    }

    public async Task<EmployeeBankDto> Handle(CreateEmployeeBankCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var isExist = await this._service.IsAccountNumberExistsAsync(request.AccountNumber);
        if (!isExist)
        {
            var bank = await _service.AddEmployeeBank(request);
            return new EmployeeBankDto { Id = bank };
        }
        else
        {
            throw new AlreadyExistsException("Bank Details", "account number", request.AccountNumber);
        }
    }
}
