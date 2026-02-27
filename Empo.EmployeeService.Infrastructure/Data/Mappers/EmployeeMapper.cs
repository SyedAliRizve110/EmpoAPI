using AutoMapper;
using Empo.EmployeeService.Domain.EmployeeDomain;
using Empo.EmployeeService.Domain.Shared;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

namespace Empo.EmployeeService.Infrastructure.Data.Mappers
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<Employee, EmployeeEntity>()
                .ReverseMap();
            CreateMap<EmployeePhoneEntity, EmployeePhone>()
                .ReverseMap();
             CreateMap<AddressEntity, Address>()
                .ReverseMap();

        }
    }
}
