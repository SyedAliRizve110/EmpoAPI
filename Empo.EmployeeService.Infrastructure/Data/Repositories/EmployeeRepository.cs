using AutoMapper;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.EmployeService;
using Empo.EmployeeService.Application.Models;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class EmployeeRepository : IEmployeeService
{
    private DbSet<EmployeeEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }

    private IMapper _mapper { get; }

    public EmployeeRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<EmployeeEntity>();
        _mapper = mapper;
    }

    public async Task<EmployeeDto> AddEmployee(EmployeeModel request)
    {
        var employeeEntity = _mapper.Map<EmployeeEntity>(request);
        await _dbSet.AddAsync(employeeEntity);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(employeeEntity);
    }

    public async Task<bool> IsEmployeeEmailExistsAsync(string email)
    {
        var employeeEntity = await _dbSet.FirstOrDefaultAsync(e => e.Email == email);
        if (employeeEntity == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
