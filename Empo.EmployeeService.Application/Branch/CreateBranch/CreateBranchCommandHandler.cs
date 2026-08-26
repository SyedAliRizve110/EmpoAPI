using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchCommandHandler : ICommandHandler<CreateBranchCommand, BranchDto>
{
    public IBranchService _service { get; }
    public CreateBranchCommandHandler(IBranchService service)
    {
        _service = service;
    }

    public async Task<BranchDto> Handle(CreateBranchCommand command, CancellationToken cancellationToken)
    {
        var request = command._request;
        var branch = await _service.CreateBranchAsync(request);
        return new BranchDto { Id = branch };
    }
}
