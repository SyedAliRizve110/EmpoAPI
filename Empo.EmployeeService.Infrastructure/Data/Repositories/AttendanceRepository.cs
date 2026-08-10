using AutoMapper;
using Empo.BuildingBlocks.Application.SharedModels;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Attendence;
using Empo.EmployeeService.Application.Attendence.ActiveEmployeesList;
using Empo.EmployeeService.Application.Attendence.AttendenceList;
using Empo.EmployeeService.Application.Attendence.ServiceInterface;
using Empo.EmployeeService.Application.CommonRequestModel;
using Empo.EmployeeService.Application.Employees.EmployeesModel;
using Empo.EmployeeService.Application.Employees.GetEmployeeList;
using Empo.EmployeeService.Application.Enums;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class AttendanceRepository : IAttendanceService
{
    private DbSet<EmployeeAttendanceEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }

    private IMapper _mapper { get; }

    public AttendanceRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<EmployeeAttendanceEntity>();
        _mapper = mapper;
    }

    public async Task<bool> CheckAnyOpenSession(Guid employeeId)
    {
        var IfSession = _dbContext.Attendence.Where(x => x.EmployeeId == employeeId && x.Status == AttendanceSessionStatus.Active && x.Date == DateOnly.FromDateTime(DateTime.Today));
        if (IfSession.Any()) return true;
        else return false;
    }

    public async Task<Guid> ClockIn(EmployeeAttendanceModel model)
    {
        var attendanceEntity = _mapper.Map<EmployeeAttendanceEntity>(model);
        var attencence = await _dbContext.Attendence.AddAsync(attendanceEntity);
        await _dbContext.SaveChangesAsync();

        return attendanceEntity.Id;
    }

    public async Task<EmployeeAttendanceModel> GetOpenSession(Guid employeeId)
    {
        var sessionEntity = await _dbContext.Attendence.Where(
            x => x.EmployeeId == employeeId &&
        x.Status == AttendanceSessionStatus.Active &&
        x.Date == DateOnly.FromDateTime(DateTime.Today)
        ).AsNoTracking().FirstOrDefaultAsync();
        var session = _mapper.Map<EmployeeAttendanceModel>(sessionEntity);
        return session;
    }

    public async Task<Guid> ClockOut(EmployeeAttendanceModel model)
    {
        var attendanceEntity = _mapper.Map<EmployeeAttendanceEntity>(model);
        bool isNew = false;
        attendanceEntity.SetDataRecorderMetadata(Constants.UserId, isNew);
        _dbSet.Update(attendanceEntity);
        await _dbContext.SaveChangesAsync();
        return attendanceEntity.Id;
    }

    public async Task<AttendanceListResponse> ListAsync(AttendanceListRequest request)
    {
        var empId = request.employeeId;
        var date = request.Date;

        var query = _dbContext.Attendence.AsNoTracking().AsQueryable();

        // Filter by EmployeeId if provided
        if (GuidExtensions.IsNullOrEmptyGuid(empId) != true)
        {
            query = query.Where(x => x.EmployeeId == empId);
        }

        // Filter by Date if provided
        if (date.HasValue)
        {
            query = query.Where(x => x.Date == date.Value);
        }
        var _query = await query.OrderByDescending(s => s.Date).ToListAsync();
        var response = new AttendanceListResponse
        {
            Collection = _mapper.Map<IEnumerable<EmployeeAttendanceModel>>(_query),
            TotalRecords = _query.Count()
        };
        return response;
    }

    public Task<ActiveEmployeeListResponse> ActiveEmployeeListAsync(ActiveEmployeeListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        var query = (from e in _dbContext.Attendence
                    join ea in _dbContext.Employee on e.EmployeeId equals ea.Id into eea
                    where (
                 EF.Functions.Like(e.Employee.FirstName, likeSearch)
                 || EF.Functions.Like(e.Employee.LastName, likeSearch)
                 || EF.Functions.Like(e.Employee.Email, likeSearch)
                 || EF.Functions.Like(e.Employee.Phone.Number, likeSearch)
                 )
                    where (
                    e.Status == AttendanceSessionStatus.Active && e.Employee.IsActive == true)
                    select new ActiveListUserModel
                    {
                        Name = e.Employee.FirstName + " " + e.Employee.LastName,
                        EmployeeId = e.EmployeeId,
                        Status = e.Status
                    }).AsNoTracking().ToListAsync();
        var employeeList = new ActiveEmployeeListResponse
        {
            Collecion = query.Result,
            TotalRecords = query.Result.Count()
        };
        return Task.FromResult(employeeList);

    }
}
