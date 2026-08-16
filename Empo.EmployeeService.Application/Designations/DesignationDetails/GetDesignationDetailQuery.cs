using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Designations.DesignationDetails;

public class GetDesignationDetailQuery : IQuery<DesignationModel>
{
    public Guid _designationId { get; private set; }
    public GetDesignationDetailQuery()
    {

    }

    public static GetDesignationDetailQuery Get(Guid designationId)
    {
        return new GetDesignationDetailQuery()
        {
            _designationId = designationId
        };
    }
}
