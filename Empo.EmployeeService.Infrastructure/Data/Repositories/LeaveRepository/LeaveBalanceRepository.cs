using Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;
using Empo.EmployeeService.Application.Leaves.LeaveBalance;
using Empo.EmployeeService.Application.Leaves.LeaveBalance.AdjustLeaveBalance;
using Empo.EmployeeService.Application.Models;
using Empo.EmployeeService.Infrastructure.Data.Entities.Leave;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories.LeaveRepository;

public class LeaveBalanceRepository : ILeaveBalanceService
{
    private readonly EmployeeContext _dbContext;

    public LeaveBalanceRepository(EmployeeContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<LeaveBalanceModel>> GetByEmployeeIdAsync(Guid employeeId, int? year = null)
    {
        var query = _dbContext.EmployeeLeaveBalance
            .Include(b => b.LeaveType)
            .Include(b => b.Employee)
            .Where(b => b.EmployeeId == employeeId);

        if (year.HasValue)
        {
            query = query.Where(b => b.Year == year.Value);
        }

        var balances = await query
            .OrderBy(b => b.Year)
            .ThenBy(b => b.LeaveType.Name)
            .Select(b => new LeaveBalanceModel
            {
                Id = b.Id,
                EmployeeId = b.EmployeeId,
                EmployeeName = (b.Employee.FirstName + " " + b.Employee.LastName).Trim(),
                LeaveTypeId = b.LeaveTypeId,
                LeaveTypeName = b.LeaveType != null ? b.LeaveType.Name : string.Empty,
                Year = b.Year,
                TotalAllocated = b.TotalAllocated,
                Used = b.Used,
                CarriedForward = b.CarriedForward,
                Available = b.TotalAllocated + b.CarriedForward - b.Used
            })
            .AsNoTracking()
            .ToListAsync();

        return balances;
    }

    public async Task<LeaveBalanceDto> AdjustAsync(AdjustLeaveBalanceRequest request)
    {
        if (request.EmployeeId == Guid.Empty)
        {
            throw new BadRequestException("EmployeeId is required.");
        }

        if (request.LeaveTypeId == Guid.Empty)
        {
            throw new BadRequestException("LeaveTypeId is required.");
        }

        var employeeExists = await _dbContext.Employee.AnyAsync(e => e.Id == request.EmployeeId);
        if (!employeeExists)
        {
            throw new NotFoundException("Employee", request.EmployeeId);
        }

        var leaveType = await _dbContext.LeaveType.FirstOrDefaultAsync(lt => lt.Id == request.LeaveTypeId);
        if (leaveType == null)
        {
            throw new NotFoundException("LeaveType", request.LeaveTypeId);
        }

        int targetYear = request.Year > 0 ? request.Year : DateTime.UtcNow.Year;

        EmployeeLeaveBalanceEntity? balance = null;
        if (request.Id.HasValue && request.Id.Value != Guid.Empty)
        {
            balance = await _dbContext.EmployeeLeaveBalance
                .FirstOrDefaultAsync(b => b.Id == request.Id.Value);

            if (balance == null)
            {
                throw new NotFoundException("EmployeeLeaveBalance", request.Id.Value);
            }
        }
        else
        {
            balance = await _dbContext.EmployeeLeaveBalance
                .FirstOrDefaultAsync(b => b.EmployeeId == request.EmployeeId &&
                                          b.LeaveTypeId == request.LeaveTypeId &&
                                          b.Year == targetYear);
        }

        if (balance == null)
        {
            balance = new EmployeeLeaveBalanceEntity
            {
                EmployeeId = request.EmployeeId,
                LeaveTypeId = request.LeaveTypeId,
                Year = targetYear,
                TotalAllocated = request.TotalAllocated ?? (request.AdjustmentDays ?? leaveType.DefaultDaysPerYear),
                Used = request.Used ?? 0,
                CarriedForward = request.CarriedForward ?? 0
            };
            await _dbContext.EmployeeLeaveBalance.AddAsync(balance);
        }
        else
        {
            if (request.TotalAllocated.HasValue)
            {
                balance.TotalAllocated = request.TotalAllocated.Value;
            }
            else if (request.AdjustmentDays.HasValue)
            {
                balance.TotalAllocated += request.AdjustmentDays.Value;
            }

            if (request.Used.HasValue)
            {
                balance.Used = request.Used.Value;
            }

            if (request.CarriedForward.HasValue)
            {
                balance.CarriedForward = request.CarriedForward.Value;
            }
        }

        if (balance.TotalAllocated < 0)
        {
            throw new BadRequestException("TotalAllocated cannot be negative.");
        }
        if (balance.Used < 0)
        {
            throw new BadRequestException("Used days cannot be negative.");
        }
        if (balance.CarriedForward < 0)
        {
            throw new BadRequestException("CarriedForward days cannot be negative.");
        }

        await _dbContext.SaveChangesAsync();
        return new LeaveBalanceDto { Id = balance.Id };
    }
}
