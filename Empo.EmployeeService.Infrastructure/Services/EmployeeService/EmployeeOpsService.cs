using AutoMapper;
using Empo.EmployeeService.Domain.EmployeeDomain;
using Empo.EmployeeService.Domain.OpsServiceInterfaces;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Empo.EmployeeService.Infrastructure.Data.Repositories.Interfaces;
using Utility.Extension;

namespace Empo.EmployeeService.Infrastructure.Services.EmployeeService;

public class EmployeeOpsService : IEmployeeOpsService
{
    private IMapper _mapper;
    private readonly IRepository<EmployeeEntity> _repoEmployee;

    public EmployeeOpsService(
        IMapper mapper,
        IRepository<EmployeeEntity> repoEmployee
        )
    {
         _mapper = mapper;
        _repoEmployee = repoEmployee;
    }

    public async Task<Guid> Save(Employee employee)
    {
        EmployeeEntity entity;
        entity = _mapper.Map<EmployeeEntity>(employee);

        if (GuidExtensions.IsNulllOrEmptyGuid(entity.Id) == true)
            await this._repoEmployee.AddAsync(entity);
        else
            await this._repoEmployee.UpdateAsync(entity);

        return entity.Id;
    }
}
