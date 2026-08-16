using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Designations.DesignationEmployeesList;

public class DesignationEmployeeListQuery : IQuery<DesignationEmployeeListResponse>
{
    public DesignationEmployeeListRequest request { get; private set; }

    public DesignationEmployeeListQuery()
    { }

    public static DesignationEmployeeListQuery Get(DesignationEmployeeListRequest request)
    {
        return new DesignationEmployeeListQuery()
        {
            request = request
        };
    }
}
