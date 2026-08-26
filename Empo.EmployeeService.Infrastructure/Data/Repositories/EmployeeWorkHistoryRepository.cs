using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.EmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.CreateEmployeeWorkHistory;
using Empo.EmployeeService.Application.EmployeeWorkHistory.UpdateEmployeeWorkHistory;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using Empo.EmployeeService.Infrastructure.Data.Entities.WorkHistory;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class EmployeeWorkHistoryRepository : IEmployeeWorkHistoryService
{
    private DbSet<EmployeeWorkHistoryEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }

    public EmployeeWorkHistoryRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<EmployeeWorkHistoryEntity>();
        _mapper = mapper;
    }

    public async Task<Guid> AddEmployeeWorkHistory(CreateEmployeWorkHistoryRequest model)
    {
        foreach (var history in model.WorkHistory)
        {
            var entity = _mapper.Map<EmployeeWorkHistoryEntity>(history);
            bool isNew = true;
            entity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
            await _dbSet.AddAsync(entity);
        }
        await _dbContext.SaveChangesAsync();
        return model.WorkHistory.First().EmployeeId;
    }

    public async Task<Guid> UpdateEmployeeWorkHistory(UpdateEmployeeWorkHistoryRequest request)
    {
        var entity = await _dbContext.WorkHistory.AsNoTracking().FirstOrDefaultAsync(w => w.Id == request.Id);
        var _request = _mapper.Map<EmployeeWorkHistoryEntity>(request);
        var updatedEntity = await UpdateMetaData(entity, _request);
        _dbSet.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<EmployeeWorkHistoryModel> GetEmployeeWorkHistoryById(Guid id)
    {
        var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
        return entity == null ? null : _mapper.Map<EmployeeWorkHistoryModel>(entity);
    }

    public async Task<List<EmployeeWorkHistoryModel>> GetEmployeeWorkHistoryList(Guid employeeId)
    {
        var list = await _dbSet.AsNoTracking()
            .Where(w => w.EmployeeId == employeeId).Include(x=> x.Branch)
            .Include(x=> x.Department).Include(x=>x.Designation)
            .OrderBy(w => w.StartDate)
            .ToListAsync();
        return _mapper.Map<List<EmployeeWorkHistoryModel>>(list);
    }

    public async Task<EmployeeWorkHistoryEntity> UpdateMetaData(EmployeeWorkHistoryEntity entity, EmployeeWorkHistoryEntity request)
    {
        request.CreatedBy = entity.CreatedBy;
        request.DateCreated = entity.DateCreated;
        bool isNew = false;
        entity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        return request;
    }
}
