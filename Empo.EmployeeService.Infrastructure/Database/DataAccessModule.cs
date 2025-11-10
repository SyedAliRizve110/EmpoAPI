using Autofac;
using Empo.BuildingBlocks.Domain.Interfaces;
using Empo.EmployeeService.Application.Configuration;
using Empo.EmployeeService.Infrastructure.Data.Repositories;
using Empo.EmployeeService.Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;

namespace Empo.EmployeeService.Infrastructure.Database;

public class DataAccessModule : Module
{
    private readonly string _dataBaseConnectionString;
    private readonly IServiceProvider _serviceProvider;

    public DataAccessModule(string dataBaseConnectionString, IServiceProvider serviceProvider)
    {
        this._dataBaseConnectionString = dataBaseConnectionString;
        this._serviceProvider = serviceProvider;
    }
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<SqlConnectionFactory>()
            .As<ISqlConnectionFactory>()
            .WithParameter("connectionString", _dataBaseConnectionString)
            .InstancePerLifetimeScope();

        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IRepository<>));

        builder
            .Register(c =>
            {
                var tenantProvider = _serviceProvider.GetService<ITenantProvider>();
                var userInforProvider = _serviceProvider.GetService<IUserInfoProvider>();

                var dbContextoptionsBuilder = new DbContextOptionsBuilder<EmployeeContext>();
                dbContextoptionsBuilder.UseNpgsql(_dataBaseConnectionString, sqlOptions =>
                {
                    sqlOptions.MigrationsHistoryTable("_EFMigrationsHistory", SchemaNames.Application);
                });
               // dbContextoptionsBuilder
                //.ReplaceService<IValueConverterSelector, strong>

                return new EmployeeContext(dbContextoptionsBuilder.Options, userInforProvider, tenantProvider);
            })
            .AsSelf()
            .As<DbContext>()
            .InstancePerLifetimeScope();
    
    }
}
