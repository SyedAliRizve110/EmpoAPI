using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeEducation.GetEmployeeEducation;

public class GetEmployeeEducationQuery : IQuery<List<EmployeeEducationModel>>
{
    public Guid _employeeId { get; private set; }

    public GetEmployeeEducationQuery()
    {
    }

    public static GetEmployeeEducationQuery Create(Guid employeeId)
    {
        return new GetEmployeeEducationQuery()
        {
            _employeeId = employeeId
        };
    }
}
