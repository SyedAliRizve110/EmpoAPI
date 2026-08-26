using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.EmployeeEducation;
using Empo.EmployeeService.Application.EmployeeEducation.CreateEmployeeEducation;
using Empo.EmployeeService.Infrastructure.Data.Entities.Education;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class EmployeeEducationRepository : IEmployeeEducationService
{
    private DbSet<EmployeeEducationEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }

    public EmployeeEducationRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<EmployeeEducationEntity>();
        _mapper = mapper;
    }

    public async Task<Guid> AddEmployeeEducation(CreateEducationRequestModel model)
    {
        foreach (var education in model.Education)
        {
            var entity = _mapper.Map<EmployeeEducationEntity>(education);
            bool isNew = true;
            entity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
            await _dbSet.AddAsync(entity);
        }
        await _dbContext.SaveChangesAsync();
        return model.Education.First().EmployeeId;
    }

    public async Task<Guid> UpdateEmployeeEducation(EmployeeEducationModel request)
    {
        var entity = await _dbContext.Education.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
        var _request = _mapper.Map<EmployeeEducationEntity>(request);
        var updatedEntity = await UpdateMetaData(entity, _request);
        _dbSet.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<EmployeeEducationModel> GetEmployeeEducationById(Guid id)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        return entity == null ? null : _mapper.Map<EmployeeEducationModel>(entity);
    }

    public async Task<List<EmployeeEducationModel>> GetEmployeeEducationList(Guid employeeId)
    {
        var list = await _dbSet.AsNoTracking()
            .Where(e => e.EmployeeId == employeeId)
            .OrderBy(e => e.StartYear)
            .ToListAsync();
        return _mapper.Map<List<EmployeeEducationModel>>(list);
    }

    public async Task<EmployeeEducationEntity> UpdateMetaData(EmployeeEducationEntity entity, EmployeeEducationEntity request)
    {
        request.CreatedBy = entity.CreatedBy;
        request.DateCreated = entity.DateCreated;
        bool isNew = false;
        entity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        return request;
    }
}
