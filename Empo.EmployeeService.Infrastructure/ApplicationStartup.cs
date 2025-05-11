using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.CommonServiceLocator;
using CommonServiceLocator;
using Empo.BuildingBlocks.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Extensions.DependencyInjection;
using ILogger = Serilog.ILogger;


namespace Empo.EmployeeService.Infrastructure;

public class ApplicationStartup
{
    public static IServiceProvider Initialize(
        IServiceCollection services,
                string connectionString,
        IServiceProvider builderServiceProvider,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor,
        bool runQuarts = true

        )
    {
        if (runQuarts)
        {
            StartQuarts(connectionString, logger, executionContextAccessor);
        }

        var serviceProvider = CreateAutofacServiceProvider(
            services,
            connectionString,
            builderServiceProvider,
            logger,
            executionContextAccessor);

        InitializeDatabase(serviceProvider);

        return serviceProvider;
    }

    public static IServiceProvider CreateAutofacServiceProvider(
        IServiceCollection services,
        string connectionString,
        IServiceProvider builderServiceProvider,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor
        )
    {
        var container = new ContainerBuilder();

        container.Populate(services);

        container.RegisterInstance(executionContextAccessor);

        var buildContainer = container.Build();

        ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(buildContainer));

        var serviceProvider = new AutofacServiceProvider(buildContainer);

        CompoitionRoot.SetContainer(buildContainer);

        return serviceProvider;

    }

    public static void StartQuarts(
        string connectionString,
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor
        )
    {
        var container = new ContainerBuilder();

        //   container.RegisterAssemblyModules( new LoggingModul)
        container.RegisterInstance(
            executionContextAccessor);
        container.Register(c =>
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<EmployeeContext>();
            dbContextOptionBuilder.UseNpgsql(connectionString);

         //   dbContextOptionBuilder.ReplaceService<IValueGeneratorSelector, Strong>

            return new EmployeeContext(dbContextOptionBuilder.Options);
        }).AsSelf().InstancePerLifetimeScope();
        container.Build();
    }

    public static void InitializeDatabase(IServiceProvider services)
    {

    }

}
