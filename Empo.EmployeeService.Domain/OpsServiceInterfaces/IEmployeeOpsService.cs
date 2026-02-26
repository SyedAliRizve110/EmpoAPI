using Empo.EmployeeService.Domain.EmployeeDomain;
    namespace Empo.EmployeeService.Domain.OpsServiceInterfaces;

public interface IEmployeeOpsService
{
    Task<Guid> Save(EmployeeDomain.Employee employee);
}
