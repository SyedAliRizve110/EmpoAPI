namespace Empo.EmployeeService.Application.Designations.AssignDesignation;

public class AssignDesignationRequest
{
    public Guid EmployeeId { get; set; }
    public Guid DesignationId { get; set; }
}
