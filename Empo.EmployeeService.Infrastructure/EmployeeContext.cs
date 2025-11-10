using Empo.BuildingBlocks.Application.Outbox;
using Empo.BuildingBlocks.Domain.Interfaces;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using Empo.EmployeeService.Infrastructure.Database;
using Empo.EmployeeService.Infrastructure.Processing.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyModel;
using System.Diagnostics;
using System.Reflection;

namespace Empo.EmployeeService.Infrastructure;

public class EmployeeContext : DbContext
{
    private ITenantProvider _tenantProvider;
    private IUserInfoProvider _userInfoProvider;

    public DbSet<EmployeeEntity> Employee { get; set; }
    public DbSet<EmployeePhoneEntity> Phone { get; set; }
    public DbSet<EmployeeTimeSheetEntity> TimeSheet { get; set; }
    public DbSet<AddressEntity> Address { get; set; }

    public DbSet<InternalCommand> InternalCommand { get; set; }
    public DbSet<OutboxMessage> OutboxMessage { get; set; }

    public EmployeeContext(DbContextOptions<EmployeeContext> options, IUserInfoProvider userInfoProvider, ITenantProvider tenantProvider)
    : base(options)
    {
        this._tenantProvider = tenantProvider;
        this._userInfoProvider = userInfoProvider;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Application);

        ApplyTenantFilter(modelBuilder);

        // modelBuilder.SeedDataBase();
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        SetChangesInternal();
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }

    public override int SaveChanges()
    {
        SetChangesInternal();
        var result = base.SaveChanges();
        return result;
    }

    private void SetChangesInternal()
    {
        var listEntriesTenantEntityBase = ChangeTracker.Entries<TenantEntityBase>().ToList();
        if (listEntriesTenantEntityBase.Count > 0)
        {
            SetTenantEntityBase(listEntriesTenantEntityBase);

            var listTEntriesEntityBase = listEntriesTenantEntityBase.Select(
                entry => Entry<EntityBase>(entry.Entity)).ToList();

            SetEntityBase(listTEntriesEntityBase);
        }
        var listEntriesEntityBase = ChangeTracker.Entries<EntityBase>().ToList();
        if (listEntriesEntityBase.Count() > 0)
        {
            SetEntityBase(listEntriesEntityBase);
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.LogTo(message => Debug.WriteLine(message));

    public void SetTenantEntityBase(List<EntityEntry<TenantEntityBase>> list)
    {
        var tenantId = _tenantProvider.GetTenantId();
        foreach (var entry in list)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetTenantId(tenantId);
                    break;
                case EntityState.Modified:
                    entry.Entity.SetTenantId(tenantId);
                    break;
            }
        }
    }
    public void SetEntityBase(List<EntityEntry<EntityBase>> list)
    {
        var userId = _userInfoProvider.GetUserId();
        foreach (var entry in list)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetDataRecorderMetadata(userId, true);
                    break;
                case EntityState.Modified:
                    entry.Property("DateCreated").IsModified = false;
                    entry.Property("CreatedBy").IsModified = false;
                    entry.Entity.SetDataRecorderMetadata(userId, false);
                    break;
            }
        }
    }
    private IList<Type>  GetEntityTypes()
    {
        IList<Type> entityType;
        entityType = (from a in GetReferencingAssemblies()
                      from t in a.DefinedTypes
                      where t.BaseType == typeof(TenantEntityBase)
                      select t.AsType()).ToList();

        return entityType;
    }

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
    public void ApplyTenantFilter(ModelBuilder modelBuilder)
    {
        MethodInfo SetGlobalQueryMethod = typeof(EmployeeContext)
            .GetMethods()
            .Single(t => t.Name == "SetGlobalQuery");

        foreach (var type in GetEntityTypes())
        {
            var method = SetGlobalQueryMethod.MakeGenericMethod(type);
            method.Invoke(modelBuilder, new object[] { modelBuilder });
        }
    }
}