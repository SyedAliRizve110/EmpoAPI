namespace Empo.EmployeeService.Application.Designations.DesignationList;

public class DesignationListResponse
{
    public IEnumerable<DesignationModel> Collection { get; set; }
    public long TotalRecords { get; set; }
}
