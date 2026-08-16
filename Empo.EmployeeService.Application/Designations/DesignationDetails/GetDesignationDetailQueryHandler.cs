using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Designations.ServiceInterface;

namespace Empo.EmployeeService.Application.Designations.DesignationDetails;

public class GetDesignationDetailQueryHandler : IQueryHandler<GetDesignationDetailQuery, DesignationModel>
{
    public IDesignationService _service { get; }

    public GetDesignationDetailQueryHandler(IDesignationService service)
    {
        _service = service;
    }

    public async Task<DesignationModel> Handle(GetDesignationDetailQuery request, CancellationToken cancellationToken)
    {
        var dsignation = await _service.GetAsync(request._designationId);
        return dsignation;
    }
}
