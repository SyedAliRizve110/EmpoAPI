using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.AuthService.Permission.CreatePermission;
using Empo.EmployeeService.Application.AuthService.Permission.PermissionList;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories.AuthRepository;

public class PermissionRepository : IPermissionService
{
    private readonly EmployeeContext _dbcontext;
    private IMapper _mapper { get; }

    public PermissionRepository(EmployeeContext dbContext, IMapper mapper)
    {
        _dbcontext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(CreatePermissionRequest request)
    {
        var permissionEntity = _mapper.Map<PermissionEntity>(request);
        bool isNew = true;
        permissionEntity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        await _dbcontext.Permission.AddAsync(permissionEntity);
        await _dbcontext.SaveChangesAsync();
        return permissionEntity.Id;
    }

    public async Task<List<string>> GetPermissionsForRoleAsync(Guid? roleId)
    {
        var permissionList = await _dbcontext.Role_Permission
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permission.Name)
            .AsNoTracking().ToListAsync();
        return permissionList;
    }

    public Task<PermissionListResponse> ListAsync(PermissionListRequest request)
    {
        string likeSearch = $"%{request.search}%";

        var query = (from d in _dbcontext.Permission
                     where (
                     EF.Functions.Like(d.Name, likeSearch) ||
                     EF.Functions.Like(d.Description, likeSearch)
                     )
                     select new PermissionResponseModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         Description = d.Description,
                         IsActive = d.IsActive,
                     }).Where(s => s.IsActive == true).AsNoTracking().ToListAsync();

        var permissionList = new PermissionListResponse()
        {
            Collection = query.Result,
            TotalRecords = query.Result.Count()
        };
        return Task.FromResult(permissionList);
    }
}
