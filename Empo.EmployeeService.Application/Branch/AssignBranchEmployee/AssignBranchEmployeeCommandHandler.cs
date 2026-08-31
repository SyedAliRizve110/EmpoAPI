using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.AssignBranchEmployee;

public class AssignBranchEmployeeCommandHandler : ICommandHandler<AssignBranchEmployeeCommand, BranchDto>
{
    public IBranchService _service { get; }
    public IEmployeeService _empservice { get; }
    public AssignBranchEmployeeCommandHandler(IBranchService service, IEmployeeService empservice)
    {
        _service = service;
        _empservice = empservice;
    }

    public async Task<BranchDto> Handle(AssignBranchEmployeeCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var branch = await _service.IsBranchExist(request.BranchId);
        if (branch)
        {
            var branchId = await _service.AssigBranchEmployee(request);
            return new BranchDto { Id = branchId };
        }
        else throw new NotFoundException("Branch", request.BranchId);
    }
}
