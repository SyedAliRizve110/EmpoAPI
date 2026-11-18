using Empo.BuildingBlocks.Infrastructure.Configuration.UserConfiguration;
using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.MediaService.Infrastructure.Data.Entities.Document;
using Empo.MediaService.Infrastructure.Data.Entities.Media;
using Empo.MediaService.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyModel;
using System.Diagnostics;
using System.Reflection;

namespace Empo.MediaService.Infrastructure;

public class MediaContext : DbContext
{
    private readonly IUserContextService _userInfoProvider;

    #region Media
    public DbSet<DocumentEntity> Document { get; set; }
    public DbSet<PostMediaEntity> PostMedia { get; set; }
    public DbSet<PostEntity> Post { get; set; }
    public DbSet<PostLikeEntity> PostLike { get; set; }
    public DbSet<PostCommentEntity> PostComment { get; set; }
    #endregion

    public MediaContext(DbContextOptions<MediaContext> options, IUserContextService userInfoProvider)
    : base(options)
    {
        _userInfoProvider = userInfoProvider;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeContext).Assembly);
        modelBuilder.HasDefaultSchema(SchemaNames.Application);

        //modelBuilder.SeedDataBase();
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
        var listEntriesEntityBase = ChangeTracker.Entries<EntityBase>().ToList();
        if (listEntriesEntityBase.Count > 0)
        {
            //  SetTenantEntityBase(listEntriesTenantEntityBase);

            var listTEntriesEntityBase = listEntriesEntityBase.Select(
                entry => Entry<EntityBase>(entry.Entity)).ToList();

            SetEntityBase(listTEntriesEntityBase);
        }
    }
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

