using AutoMapper;
using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Domain.EmployeeDomain;

namespace Empo.EmloyeeService.Api.MappingProfiles
{
    public class RegisterProfiles : Profile
    {
        public RegisterProfiles()
        {
            CreateMap<CreateEmployeeRequest, Employee>();
        }
    }
}
