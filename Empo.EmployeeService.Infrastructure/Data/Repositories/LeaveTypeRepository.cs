using AutoMapper;
using Empo.EmployeeService.Application.Leaves.LeaveType;
using Empo.EmployeeService.Application.Models;
using Empo.EmployeeService.Infrastructure.Data.Entities.Leave;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class LeaveTypeRepository : ILeaveTypeService
{
    private DbSet<LeaveTypeEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }
    private IMapper _mapper { get; }
    public LeaveTypeRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<LeaveTypeDto> AddAsync(CreateLeaveTypeRequest request)
    {
        var leaveTypeEntity = _mapper.Map<LeaveTypeEntity>(request);
        await _dbSet.AddAsync(leaveTypeEntity);
        await _dbContext.SaveChangesAsync();
        return new LeaveTypeDto { Id = leaveTypeEntity.Id };
    }

    public async Task<LeaveTypeRequestModel> GetAsyncById(Guid leaveTypeId)
    {
        var entity = _dbSet.AsNoTracking().Where(e => e.Id == leaveTypeId).FirstOrDefault();
        var leaveType = _mapper.Map<LeaveTypeRequestModel>(entity);
        return leaveType;
    }

    public async Task<GetLeaveTypeListResponse> ListAsync(GetLeaveTypeListRequest request)
    {
        string likeSearch = $"%{request.search}%";

        var query = (from d in _dbContext.LeaveType
                     where (
                     EF.Functions.Like(d.Name, likeSearch)
                     )
                     select new LeaveTypeRequestModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         DefaultDaysPerYear = d.DefaultDaysPerYear,
                         IsPaid = d.IsPaid,
                         CarryForwardAllowed = d.CarryForwardAllowed,
                         MaxCarryForwardDays = d.MaxCarryForwardDays,
                         RequiresApproval = d.RequiresApproval,
                         IsActive = d.IsActive,
                     }).AsNoTracking().ToListAsync();

        var list = new GetLeaveTypeListResponse()
        {
            Collection = query.Result,
            TotalRecords = query.Result.Count()
        };
        return list;
    }

    public async Task<LeaveTypeDto> UpdateAsync(LeaveTypeRequestModel request)
    {
        var entity = await _dbContext.LeaveType.FirstOrDefaultAsync(e => e.Id == request.Id);
        var _request = _mapper.Map<LeaveTypeEntity>(request);
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
        return new LeaveTypeDto { Id = entity.Id };
    }
}
