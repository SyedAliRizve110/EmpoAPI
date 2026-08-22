using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Department;
using Empo.EmployeeService.Infrastructure.Data.Entities.Designation;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;
using Empo.EmployeeService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyModel;
using System.Diagnostics;
using System.Reflection;

namespace Empo.EmployeeService.Infrastructure;

public class EmployeeContext : DbContext
{
    #region Employee

    public DbSet<EmployeeEntity> Employee { get; set; }
    public DbSet<EmployeePhoneEntity> Phone { get; set; }
    public DbSet<EmployeeAttendanceEntity> Attendence { get; set; }
    public DbSet<AddressEntity> Address { get; set; }
    public DbSet<DepartmentEntity> Department { get; set; }
    public DbSet<DesignationEntity> Designation { get; set; }
    public DbSet<BranchEntity> Branch { get; set; }

    #endregion

    #region User

    public DbSet<UserEntity> User { get; set; }
    public DbSet<RefreshTokenEntity> RefreshToken { get; set; }

    //Role and Permission
    public DbSet<RoleEntity> Role { get; set; }
    public DbSet<PermissionEntity> Permission { get; set; }
    public DbSet<Role_PermissionEntity> Role_Permission { get; set; }
   // public DbSet<User_RoleEntity> User_Role { get; set; }

    #endregion


    public EmployeeContext(DbContextOptions<EmployeeContext> options)
    : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Application);

        modelBuilder.SeedDataBase();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //SetChangesInternal();
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    public override int SaveChanges()
    {
        // SetChangesInternal();
        var result = base.SaveChanges();
        return result;
    }

    //private void SetChangesInternal()
    //{
    //    var listEntriesTenantEntityBase = ChangeTracker.Entries<TenantEntityBase>().ToList();
    //    if (listEntriesTenantEntityBase.Count > 0)
    //    {
    //        SetTenantEntityBase(listEntriesTenantEntityBase);

    //        var listTEntriesEntityBase = listEntriesTenantEntityBase.Select(
    //            entry => Entry<EntityBase>(entry.Entity)).ToList();

    //        SetEntityBase(listTEntriesEntityBase);
    //    }
    //}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(message => Debug.WriteLine(message));

    //public void SetTenantEntityBase(List<EntityEntry<TenantEntityBase>> list)
    //{
    //   // var tenantId = _tenantProvider.GetTenantId();
    //    foreach (var entry in list)
    //    {
    //        switch (entry.State)
    //        {
    //            case EntityState.Added:
    //             //   entry.Entity.SetTenantId(tenantId);
    //                break;
    //            case EntityState.Modified:
    //             //   entry.Entity.SetTenantId(tenantId);
    //                break;
    //        }
    //    }
    //}
    public void SetEntityBase(List<EntityEntry<EntityBase>> list)
    {
        // var userId = _userInfoProvider.
        // var userId = _userInfoProvider.GetUserId();
        foreach (var entry in list)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    //  entry.Entity.SetDataRecorderMetadata(userId, true);
                    break;
                case EntityState.Modified:
                    entry.Property("DateCreated").IsModified = false;
                    entry.Property("CreatedBy").IsModified = false;
                    //entry.Entity.SetDataRecorderMetadata(userId, false);
                    break;
            }
        }
    }
    //private IList<Type> GetEntityTypes()
    //{
    //    IList<Type> entityType;
    //    entityType = (from a in GetReferencingAssemblies()
    //                  from t in a.DefinedTypes
    //                  where t.BaseType == typeof(TenantEntityBase)
    //                  select t.AsType()).ToList();

    //    return entityType;
    //}

    private IEnumerable<Assembly> GetReferencingAssemblies()
    {
        var assemblies = new List<Assembly>();
        var dependencies = DependencyContext.Default.RuntimeLibraries;

        foreach (var library in dependencies)
        {
            try
            {
                var assembly = Assembly.Load(new AssemblyName(library.Name));
                assemblies.Add(assembly);
            }
            catch (FileNotFoundException)
            { }
        }
        return assemblies;
    }
}

