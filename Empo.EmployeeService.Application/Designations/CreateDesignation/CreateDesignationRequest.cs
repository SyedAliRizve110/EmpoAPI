namespace Empo.EmployeeService.Application.Designations.CreateDesignation;

public class CreateDesignationRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
