using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.Department.GetDepartmentDetails;

public class GetDepartmentDetailsQuery : IQuery<GetDepartmentDetailsResponse>
{
    public Guid _departmentId { get; private set; }

    public static GetDepartmentDetailsQuery Get(Guid DepartmentId)
    {
        return new GetDepartmentDetailsQuery()
        {
            _departmentId = DepartmentId
        };
    }
}
