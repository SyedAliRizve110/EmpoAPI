using AutoMapper;
using Empo.EmployeeService.Application.Designations;
using Empo.EmployeeService.Application.Designations.AssignDesignation;
using Empo.EmployeeService.Application.Designations.CreateDesignation;
using Empo.EmployeeService.Application.Designations.DesignationEmployeesList;
using Empo.EmployeeService.Application.Designations.DesignationList;
using Empo.EmployeeService.Application.Designations.ServiceInterface;
using Empo.EmployeeService.Infrastructure.Data.Entities.Designation;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class DesignationRepository : IDesignationService
{
    private DbSet<DesignationEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }
    public DesignationRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<DesignationEntity>();
        _mapper = mapper;
    }
    public async Task<Guid> AddAsync(CreateDesignationRequest request)
    {
        var designationEntity = _mapper.Map<DesignationEntity>(request);
        await _dbSet.AddAsync(designationEntity);
        await _dbContext.SaveChangesAsync();
        return designationEntity.Id;
    }
    public async Task<DesignationModel> GetAsync(Guid id)
    {
        var designationEntity = _dbSet.AsNoTracking().Where(e => e.Id == id).FirstOrDefault();
        var designationModel = _mapper.Map<DesignationModel>(designationEntity);
        return designationModel;
    }

    public async Task<DesignationListResponse> ListAsync(DesignationListRequest request)
    {
        string likesearch = $"%{request.search}%";

        var query = (from d in _dbContext.Designation
                     where (
                     EF.Functions.Like(d.Name, likesearch)
                     )
                     select new DesignationModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         Description = d.Description,
                         IsActive = d.IsActive
                     }).AsNoTracking().ToListAsync();

        var designationlist = new DesignationListResponse()
        {
            Collection = query.Result,
            TotalRecords = query.Result.Count()
        };
        return designationlist;
    }

    public async Task<Guid> UpdateAsync(DesignationModel model)
    {
        var designationEntity = await _dbContext.Designation.FindAsync(model.Id);
        var updated = await UpdateData(designationEntity, model);
        _dbSet.Update(updated);
        await _dbContext.SaveChangesAsync();
        return designationEntity.Id;
    }
    public async Task<DesignationEntity> UpdateData(DesignationEntity designationEntity, DesignationModel model)
    {
        designationEntity.Name = model.Name;
        designationEntity.Description = model.Description;
        designationEntity.IsActive = model.IsActive;
        return designationEntity;
    }

    public Task<DesignationEmployeeListResponse> DesignationEmployeeListAsync(DesignationEmployeeListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        var query = (from d in _dbContext.Employee.Where(e => e.DesignationId == request.DesignationId)
                     where (
                     EF.Functions.Like(d.FirstName, likeSearch)
                     || EF.Functions.Like(d.LastName, likeSearch)
                     || EF.Functions.Like(d.Email, likeSearch)
                     )
                     select new DesignationEmployeeModel
                     {
                         Id = d.Id,
                         Name = d.FirstName + " " + d.LastName,
                         Email = d.Email,
                         IsActive = d.IsActive
                     }).AsNoTracking().ToListAsync();
        var designationList = new DesignationEmployeeListResponse()
        {
            Collection = query.Result,
            TotalRecords = query.Result.Count()
        }; return Task.FromResult(designationList);
    }

    public async Task<Guid> AssignDesignationAsync(AssignDesignationRequest request)
    {
        var employee = _dbContext.Employee.Where(x => x.Id == request.EmployeeId).FirstOrDefault();
        employee.DesignationId = request.DesignationId;
        _dbContext.Employee.Update(employee);
        await _dbContext.SaveChangesAsync();
        return request.DesignationId;
    }
}
