using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.UpdateBranch;

public class UpdateBranchCommandHandler : ICommandHandler<UpdateBranchCommand, BranchDto>
{
    public IBranchService _service { get; }
    public UpdateBranchCommandHandler(IBranchService service)
    {
        _service = service;
    }
    public async Task<BranchDto> Handle(UpdateBranchCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var branch = await this._service.IsBranchExist(request.Id);
        if (branch)
        {
            var _branch = await _service.UpdateBranchAsync(request);
            return new BranchDto { Id = _branch };
        }
        else
        {
            throw new Exception("Branch with this email does not exists.");
        }
    }
}
