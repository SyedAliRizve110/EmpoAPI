namespace Empo.EmployeeService.Application.Designations.DesignationList;

public class DesignationListResponse
{
    public IEnumerable<DesignationModel> Collecion { get; set; }
    public long TotalRecords { get; set; }
}
