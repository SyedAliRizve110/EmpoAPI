namespace Empo.EmployeeService.Application.Designations.DesignationEmployeesList;

public class DesignationEmployeeListResponse
{
    public IEnumerable<DesignationEmployeeModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}

public class DesignationEmployeeModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
}