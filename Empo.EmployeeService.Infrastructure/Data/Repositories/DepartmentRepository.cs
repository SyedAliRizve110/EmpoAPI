using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Department;
using Empo.EmployeeService.Application.Department.AddDepartmentEmployee;
using Empo.EmployeeService.Application.Department.AssignManager;
using Empo.EmployeeService.Application.Department.CreateDepartment;
using Empo.EmployeeService.Application.Department.DepartmentEmployeesList;
using Empo.EmployeeService.Application.Department.GetDepartmentDetails;
using Empo.EmployeeService.Application.Department.ListDepartment;
using Empo.EmployeeService.Application.Department.ServiceInterface;
using Empo.EmployeeService.Application.Department.UpdateDepartment;
using Empo.EmployeeService.Infrastructure.Data.Entities.Department;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class DepartmentRepository : IDepartmentService
{
    private DbSet<DepartmentEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }

    public DepartmentRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<DepartmentEntity>();
        _mapper = mapper;
    }

    public async Task<Guid> AddDepartment(CreateDepartmentRequest request)
    {
        var departmentEntity = _mapper.Map<DepartmentEntity>(request);
        bool isNew = true; departmentEntity.IsActive = true;
        departmentEntity.SetDataRecorderMetadata(Constants.UserId, isNew);
        await _dbSet.AddAsync(departmentEntity);
        await _dbContext.SaveChangesAsync();
        return departmentEntity.Id;
    }

    public async Task<Guid> UpdateDepartment(UpdateDepartmentRequestModel request)
    {
        var departmentEntity = await _dbContext.Department.FindAsync(request.Id);
        var updated = await UpdateMetaData(departmentEntity, request);
        _dbSet.Update(updated);
        await _dbContext.SaveChangesAsync();
        return departmentEntity.Id;
    }

    public async Task<GetDepartmentDetailsResponse> GetDepartmentDetails(Guid id)
    {
        var departmentEntity = _dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        if (departmentEntity == null)
        {
            throw new Exception("Department not found.");
        }
        var departmentModel = _mapper.Map<GetDepartmentDetailsResponse>(departmentEntity);
        return departmentModel;
    }

    public Task<GetDepartmentListResponse> DepartmentListAsync(GetDepartmentListRequest request)
    {
        string likeSearch = $"%{request.search}%";

        var query = (from d in _dbContext.Department
                     where (
                     EF.Functions.Like(d.Name, likeSearch)
                     )
                     select new DepartmentModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         Description = d.Description,
                         IsActive = d.IsActive
                     }).AsNoTracking().ToListAsync();

        var departmentList = new GetDepartmentListResponse()
        {
            Collecion = query.Result,
            TotalRecords = query.Result.Count()
        };
        return Task.FromResult(departmentList);

    }

    public Task<DepartmentEmployeeListResponse> DepartmentEmployeeListAsync(DepartmentEmployeeListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        var query = (from d in _dbContext.Employee.Where(e => e.DepartmentId == request.DepartmentId)
                     where (
                     EF.Functions.Like(d.FirstName, likeSearch)
                     || EF.Functions.Like(d.LastName, likeSearch)
                     || EF.Functions.Like(d.Email, likeSearch)
                     )
                     select new DepartmentEmployeeModel
                     {
                         Id = d.Id,
                         Name = d.FirstName + " " + d.LastName,
                         Email = d.Email,
                         IsActive = d.IsActive
                     }).AsNoTracking().ToListAsync();
        var departmentList = new DepartmentEmployeeListResponse()
        {
            Collecion = query.Result,
            TotalRecords = query.Result.Count()
        }; return Task.FromResult(departmentList);
    }
    public async Task<DepartmentEntity> UpdateMetaData(DepartmentEntity departmentEntity, UpdateDepartmentRequestModel request)
    {
        bool isNew = false;
        departmentEntity.SetDataRecorderMetadata(Constants.UserId, isNew);
        departmentEntity.Name = request.Name;
        departmentEntity.Description = request.Description;
        departmentEntity.IsActive = request.IsActive;
        departmentEntity.ManagerId = request.ManagerId;
        return departmentEntity;
    }

    public Task<Guid> AddDepartmentEmployeeAsync(AddDepartmentEmployeeRequest request)
    {
        var departmentEntity = _dbContext.Department.Find(request.DepartmentId);
        if (departmentEntity == null)
        {
            throw new Exception("Department not found.");
        }
        var employees = _dbContext.Employee.Where(e => request.EmployeesId.Contains(e.Id)).ToList();
        foreach (var employee in employees)
        {
            employee.DepartmentId = request.DepartmentId;
            employee.SetDataRecorderMetadata(Constants.UserId, false);
            _dbContext.Employee.Update(employee);
        }
        _dbContext.SaveChanges();
        return Task.FromResult(departmentEntity.Id);
    }

    public async Task<Guid> AssignManagerAsync(AssignManagerRequest request)
    {
        var department = _dbContext.Department.Where(x => x.Id == request.DepartmentId).FirstOrDefault();
        department.ManagerId = request.ManagerId;
        _dbContext.Department.Update(department);
        await _dbContext.SaveChangesAsync();
        return department.Id;
    }
}
