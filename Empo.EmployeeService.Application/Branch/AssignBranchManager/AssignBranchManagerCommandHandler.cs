using Empo.BuildingBlocks.Application.Configuration;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
using Empo.EmployeeService.Application.Models;

namespace Empo.EmployeeService.Application.Branch.AssignBranchManager;

public class AssignBranchManagerCommandHandler : ICommandHandler<AssignBranchManagerCommand, BranchDto>
{
    public IBranchService _service { get; }
    public IEmployeeService _empservice { get; }
    public AssignBranchManagerCommandHandler(IBranchService service, IEmployeeService empservice)
    {
        _service = service;
        _empservice = empservice;
    }

    public async Task<BranchDto> Handle(AssignBranchManagerCommand command, CancellationToken cancellationToken)
    {
        var request = command.request;
        var branch = await _service.GetBranchDetails(request.BranchId);
        var employee = await _empservice.GetEmployeeDetails(request.ManagerId);
        if (branch != null && employee != null)
        {
            var branchId = await _service.AssigBranchManager(request);
            return new BranchDto { Id = branchId };
        }
        else throw new Exception("No branch or Employee found with this id");
    }
}
