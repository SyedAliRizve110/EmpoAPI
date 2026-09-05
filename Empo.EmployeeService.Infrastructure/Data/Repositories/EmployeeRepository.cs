using AutoMapper;
using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Employees.CreateEmployee;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.GetEmployeeList;
using Empo.EmployeeService.Application.Employees.ServiceInterface;
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

    public async Task<Guid> AddEmployee(CreateEmployeeRequestModel request)
    {
        var employeeEntity = _mapper.Map<EmployeeEntity>(request);
        employeeEntity.IsActive = true;
        await _dbSet.AddAsync(employeeEntity);
        await _dbContext.SaveChangesAsync();
        return employeeEntity.Id;
    }

    public async Task<Guid> UpdateEmployee(EmployeeModel request)
    {
        var employeeEntity = await _dbContext.Employee.FirstOrDefaultAsync(e => e.Id == request.Id);
        var _request = _mapper.Map<EmployeeEntity>(request);
        _request.Attendance = null;
        var updatedEntity = await UpdateData(employeeEntity, _request);
        _dbSet.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return employeeEntity.Id;
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

    public async Task<EmployeeModel> GetEmployeeDetails(Guid employeeId)
    {
        var employeeEntity = _dbSet.AsNoTracking().Where(e => e.Id == employeeId).Include(x => x.Phone).Include(x => x.Address).FirstOrDefault();
        var employeeModel = _mapper.Map<EmployeeModel>(employeeEntity);
        return employeeModel;
    }

    public async Task<EmployeeEntity> ActivateEmployee(EmployeeEntity entity)
    {
        entity.IsActive = true;
        return entity;
    }

    public async Task<GetEmployeeListResponse> EmployeeListAsync(GetEmployeeListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        var query = (from v in _dbContext.Employee
                     join vp in _dbContext.Phone on v.Id equals vp.EmployeeId into vvp
                     from vp in vvp.DefaultIfEmpty()
                     where (
                     EF.Functions.Like(v.FirstName, likeSearch)
                     || EF.Functions.Like(v.LastName, likeSearch)
                     || EF.Functions.Like(v.Email, likeSearch)
                     || EF.Functions.Like(vp.Number, likeSearch)
                     )
                     select new EmployeeModel
                     {
                         Id = v.Id,
                         FirstName = v.FirstName,
                         LastName = v.LastName,
                         Email = v.Email,
                         DateOfBirth = v.DateOfBirth,
                         IsActive = v.IsActive,
                         EmployeeRole = v.EmployeeRole,
                         AddressId = v.AddressId,
                         Address = new AddressModel
                         {
                             Id = v.Address.Id,
                             Address1 = v.Address.Address1,
                             Address2 = v.Address.Address2,
                             City = v.Address.City,
                             State = v.Address.State,
                             ZipCode = v.Address.ZipCode,
                             Country = v.Address.Country
                         },
                         Phone = new EmployeePhoneModel
                         {
                             Id = vp.Id,
                             CountryCode = vp.CountryCode,
                             Number = vp.Number
                         }
                     }).ToListAsync();
        var employeeList = new GetEmployeeListResponse
        {
            Collection = await query,
            TotalRecords = query.Result.Count()
        };
        return employeeList;
    }

    public async Task<EmployeeEntity> UpdateData(EmployeeEntity employeeEntity, EmployeeEntity request)
    {
        request.CreatedBy = employeeEntity.CreatedBy;
        request.DateCreated = employeeEntity.DateCreated;
        return request;
    }
}