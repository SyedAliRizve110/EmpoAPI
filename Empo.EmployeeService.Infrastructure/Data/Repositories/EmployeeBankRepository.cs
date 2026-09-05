using AutoMapper;
using Empo.EmployeeService.Application.EmployeeBank;
using Empo.EmployeeService.Application.EmployeeBank.Create;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class EmployeeBankRepository : IEmployeeBankService
{
    private DbSet<EmployeeBankEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }

    public EmployeeBankRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<EmployeeBankEntity>();
        _mapper = mapper;
    }

    public async Task<Guid> AddEmployeeBank(CreateEmployeeBankRequest request)
    {
        var bankEntity = _mapper.Map<EmployeeBankEntity>(request);
        await _dbSet.AddAsync(bankEntity);
        await _dbContext.SaveChangesAsync();
        return bankEntity.Id;
    }

    public async Task<Guid> UpdateEmployeeBank(EmployeeBankModel request)
    {
        var bankEntity = await _dbContext.EmployeeBank.AsNoTracking().FirstOrDefaultAsync(b => b.Id == request.Id);
        var _request = _mapper.Map<EmployeeBankEntity>(request);
        var updatedEntity = await UpdateData(bankEntity, _request);
        _dbSet.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return bankEntity.Id;
    }

    public async Task<bool> IsAccountNumberExistsAsync(string accountNumber)
    {
        var bankEntity = await _dbSet.FirstOrDefaultAsync(b => b.AccountNumber == accountNumber);
        if (bankEntity == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public async Task<EmployeeBankModel> GetEmployeeBank(Guid employeeId)
    {
        var bankEntity = _dbSet.AsNoTracking().FirstOrDefault(b => b.EmployeeId == employeeId);
        var bankModel = _mapper.Map<EmployeeBankModel>(bankEntity);
        return bankModel;
    }

    public async Task<EmployeeBankEntity> UpdateData(EmployeeBankEntity bankEntity, EmployeeBankEntity request)
    {
        request.CreatedBy = bankEntity.CreatedBy;
        request.DateCreated = bankEntity.DateCreated;
        return request;
    }
}
