namespace Empo.EmployeeService.Application.Designations;

public class DesignationModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
