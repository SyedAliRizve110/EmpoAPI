using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Branch;
using Empo.EmployeeService.Application.Branch.AssignBranchEmployee;
using Empo.EmployeeService.Application.Branch.AssignBranchManager;
using Empo.EmployeeService.Application.Branch.BranchEmployeeList;
using Empo.EmployeeService.Application.Branch.CreateBranch;
using Empo.EmployeeService.Application.Branch.ListBranch;
using Empo.EmployeeService.Application.Branch.ServiceInterface;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories;

public class BranchRepository : IBranchService
{
    private DbSet<BranchEntity> _dbSet;
    protected EmployeeContext _dbContext { get; set; }

    private IMapper _mapper { get; }

    public BranchRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<BranchEntity>();
        _mapper = mapper;
    }

    public async Task<Guid> CreateBranchAsync(CreateBranchRequest request)
    {
        var branchEntity = _mapper.Map<BranchEntity>(request);
        bool isNew = true; branchEntity.IsActive = true;
        branchEntity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        await _dbSet.AddAsync(branchEntity);
        await _dbContext.SaveChangesAsync();
        return branchEntity.Id;
    }

    public async Task<Guid> UpdateBranchAsync(BranchModel request)
    {
        var branchEntity = await _dbContext.Branch.AsNoTracking().FirstOrDefaultAsync(e => e.Id == request.Id);
        var _request = _mapper.Map<BranchEntity>(request);
        var updatedEntity = await UpdateMetaData(branchEntity, _request);
        _dbSet.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return branchEntity.Id;
    }

    public async Task<BranchModel> GetBranchDetails(Guid id)
    {
        var branchEntity = _dbSet.AsNoTracking().Where(e => e.Id == id).Include(x => x.Address).FirstOrDefault();
        if (branchEntity == null)
        {
            throw new Exception("Employee not found.");
        }
        var branchModel = _mapper.Map<BranchModel>(branchEntity);
        return branchModel;
    }

    public Task<BranchListResponse> BranchListAsync(BranchListRequest request)
    {
        string likeSearch = $"%{request.search}%";

        var query = (from d in _dbContext.Branch
                     where (
                     EF.Functions.Like(d.Name, likeSearch) ||
                     EF.Functions.Like(d.BranchCode, likeSearch)
                     )
                     select new BranchModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         BranchCode = d.BranchCode,
                         IsActive = d.IsActive,
                     }).AsNoTracking().ToListAsync();

        var branchList = new BranchListResponse()
        {
            Collecion = query.Result,
            TotalRecords = query.Result.Count()
        };
        return Task.FromResult(branchList);
    }

    public Task<BranchEmployeeListResponse> BranchEmployeeListAsync(BranchEmployeeListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        var query = (from d in _dbContext.Employee.Where(e => e.BranchId == request.BranchId)
                     where (
                     EF.Functions.Like(d.FirstName, likeSearch)
                     || EF.Functions.Like(d.LastName, likeSearch)
                     || EF.Functions.Like(d.Email, likeSearch)
                     )
                     select new BranchEmployeeModel
                     {
                         Id = d.Id,
                         Name = d.FirstName + " " + d.LastName,
                         Email = d.Email,
                         IsActive = d.IsActive
                     }).AsNoTracking().ToListAsync();
        var branchList = new BranchEmployeeListResponse()
        {
            Collecion = query.Result,
            TotalRecords = query.Result.Count()
        }; return Task.FromResult(branchList);
    }

    public Task<Guid> AssigBranchEmployee(AssignBranchEmployeeRequest request)
    {
        var employees = _dbContext.Employee.Where(e => request.EmployeeId.Contains(e.Id)).ToList();
        foreach (var employee in employees)
        {
            employee.BranchId = request.BranchId;
            employee.SetDataRecorderMetadata(Constants.AdminUserId, false);
            _dbContext.Employee.Update(employee);
        }
        _dbContext.SaveChanges();
        return Task.FromResult(request.BranchId);
    }

    public async Task<BranchEntity> UpdateMetaData(BranchEntity branchEntity, BranchEntity request)
    {
        request.CreatedBy = branchEntity.CreatedBy;
        request.DateCreated = branchEntity.DateCreated;
        bool isNew = false;
        branchEntity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        return request;
    }

    public async Task<Guid> AssigBranchManager(AssignBranchManagerRequest request)
    {
        var branch = _dbContext.Branch.Where(x => x.Id == request.BranchId).FirstOrDefault();
        branch.ManagerId = request.ManagerId;
        _dbContext.Branch.Update(branch);
        await _dbContext.SaveChangesAsync();
        return branch.Id;
    }

    public async Task<bool> IsBranchExist(Guid id)
    {
        var exist = _dbContext.Branch.Any(x => x.Id == id);
        return exist;
    }
}
