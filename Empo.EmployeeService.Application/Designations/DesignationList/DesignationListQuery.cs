using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Designations.DesignationList;

public class DesignationListQuery : IQuery<DesignationListResponse>
{
    public DesignationListRequest request { get; private set; }

    public DesignationListQuery()
    { }

    public static DesignationListQuery Get(DesignationListRequest request)
    {
        return new DesignationListQuery()
        {
            request = request
        };
    }
}
