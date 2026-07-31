using AutoMapper;
using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

namespace Empo.EmployeeService.Infrastructure.Data.Mappers
{
    public class EmployeeMapper : Profile
    {
        public EmployeeMapper()
        {
            CreateMap<EmployeeModel, EmployeeEntity>().ReverseMap();
            CreateMap<EmployeePhoneModel, EmployeePhoneEntity>().ReverseMap();
            //CreateMap<EmployeeTimeSheetModel, EmployeeTimeSheetEntity>().ReverseMap();
            CreateMap<AddressModel, AddressEntity>().ReverseMap();

        }
    }
}
