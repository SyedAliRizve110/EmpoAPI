using Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;

namespace Empo.EmployeeService.Application.EmployeeEducation;

public interface IEmployeeEducationService
{
    Task<Guid> AddEmployeeEducation(CreateEducationRequestModel request);
    Task<Guid> UpdateEmployeeEducation(EmployeeEducationModel request);
    Task<EmployeeEducationModel> GetEmployeeEducationById(Guid id);
    Task<List<EmployeeEducationModel>> GetEmployeeEducationList(Guid employeeId);
}
