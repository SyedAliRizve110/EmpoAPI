using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.EmployeeBank.Update;

public class UpdateEmployeeBankCommandHandler : ICommandHandler<UpdateEmployeeBankCommand, EmployeeBankDto>
{
    public IEmployeeBankService _service { get; }

    public UpdateEmployeeBankCommandHandler(IEmployeeBankService service)
    {
        _service = service;
    }

    public async Task<EmployeeBankDto> Handle(UpdateEmployeeBankCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var bank = await this._service.GetEmployeeBank(request.EmployeeId);
        if (bank == null || bank.Id != request.Id)
        {
            throw new NotFoundException("Bank", request.EmployeeId);
        }
        else
        {
            var _bank = await _service.UpdateEmployeeBank(request);
            return new EmployeeBankDto { Id = _bank };
        }
    }
}
