using AutoMapper;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.AuthService.Intrfaces;
using Empo.EmployeeService.Application.AuthService.Role.AssignRolePermission;
using Empo.EmployeeService.Application.AuthService.Role.AssignUserRole;
using Empo.EmployeeService.Application.AuthService.Role.CreateRole;
using Empo.EmployeeService.Application.AuthService.Role.GetRoleDetails;
using Empo.EmployeeService.Application.AuthService.Role.RevokePermission;
using Empo.EmployeeService.Application.AuthService.Role.RoleList;
using Empo.EmployeeService.Application.AuthService.Role.RoleStatus;
using Empo.EmployeeService.Application.AuthService.Role.UpdateRole;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace Empo.EmployeeService.Infrastructure.Data.Repositories.AuthRepository;

public class RoleRepository : IRoleService
{
    private readonly IMapper _mapper;
    private readonly EmployeeContext _dbContext;
    public RoleRepository(IMapper mapper, EmployeeContext dbContext)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<Guid> AddAsync(CreateRoleRequest request)
    {
        var roleEntity = _mapper.Map<RoleEntity>(request);
        bool isNew = true; roleEntity.IsActive = true;
        roleEntity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        await _dbContext.AddAsync(roleEntity);
        await _dbContext.SaveChangesAsync();
        return roleEntity.Id;
    }

    public async Task<Guid> AssignPermission(AssignRolePermissionRequest request)
    {
        var permissionIdList = request.PermissionId;
        foreach (var permissionId in permissionIdList)
        {
            var exist = await _dbContext.Role_Permission.AnyAsync(rp =>
            rp.RoleId == request.RoleId && rp.PermissionId == permissionId);
            if (exist) continue;
            _dbContext.Role_Permission.Add(new Role_PermissionEntity
            {
                RoleId = request.RoleId,
                PermissionId = permissionId,
                CreatedBy = Constants.AdminUserId,
                DateCreated = DateTime.UtcNow
            });
        }
        await _dbContext.SaveChangesAsync();
        return request.RoleId;
    }

    public async Task<Guid> AssignUserRole(AssignUserRoleRequest request)
    {
        var user = await _dbContext.User.FindAsync(request.UserId);
        user.RoleId = request.RoleId;
        await _dbContext.SaveChangesAsync();
        return request.RoleId;
    }

    public async Task<GetRoleDetailResponse> GetAsync(Guid id)
    {
        var roleEntity = _dbContext.Role.AsNoTracking().Where(e => e.Id == id).Include(x => x.Role_Permission).AsNoTracking().FirstOrDefault();
        var roleModel = _mapper.Map<GetRoleDetailResponse>(roleEntity);
        return roleModel;
    }

    public async Task<bool> HasRoleAssigned(Guid id)
    {
        var hasUsers = await _dbContext.User.AnyAsync(u => u.RoleId == id);
        return hasUsers;
    }

    public Task<RoleListResponse> ListAsync(RoleListRequest request)
    {
        string likeSearch = $"%{request.search}%";
        bool activeStatus = request.isActive;
        var query = (from d in _dbContext.Role
                     where (
                     EF.Functions.Like(d.Name, likeSearch) ||
                     EF.Functions.Like(d.Description, likeSearch)
                     )
                     select new RoleResponseModel
                     {
                         Id = d.Id,
                         Name = d.Name,
                         Description = d.Description,
                         IsActive = d.IsActive
                     }).Where(r => r.IsActive == activeStatus).AsNoTracking().ToListAsync();

        var roleList = new RoleListResponse()
        {
            Collection = query.Result,
            TotalRecords = query.Result.Count()
        };
        return Task.FromResult(roleList);
    }

    public async Task<Guid> RevokePermission(RevokePermissionRequest request)
    {
        var link = await _dbContext.Role_Permission
            .FirstOrDefaultAsync(rp => rp.RoleId == request.RoleId &&
            rp.PermissionId == request.PermissionId);
        if (link == null) return Guid.Empty;
        _dbContext.Role_Permission.Remove(link);
        await _dbContext.SaveChangesAsync();
        return link.RoleId;
    }

    public async Task<Guid> UpdateAsync(UpdateRoleRequest request)
    {
        var roleEntity = await _dbContext.Role.FirstOrDefaultAsync(e => e.Id == request.Id);
        var _request = _mapper.Map<RoleEntity>(request);
        var updatedEntity = await UpdateMetaData(roleEntity, _request);
        _dbContext.Update(updatedEntity);
        await _dbContext.SaveChangesAsync();
        return roleEntity.Id;
    }

    public async Task<Guid> UpdateStatusAsync(RoleStatusRequest request)
    {
        var role = await _dbContext.Role.FirstOrDefaultAsync(r => r.Id == request.Id);
        role.IsActive = request.IsActive;
        _dbContext.Role.Update(role);
        await _dbContext.SaveChangesAsync();
        return role.Id;
    }
    private async Task<RoleEntity> UpdateMetaData(RoleEntity roleEntity, RoleEntity request)
    {
        request.CreatedBy = roleEntity.CreatedBy;
        request.DateCreated = roleEntity.DateCreated;
        bool isNew = false;
        roleEntity.SetDataRecorderMetadata(Constants.AdminUserId, isNew);
        return request;
    }
}
