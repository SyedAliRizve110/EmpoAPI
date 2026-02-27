using Autofac;
using Empo.EmployeeService.Domain.OpsServiceInterfaces;

namespace Empo.EmployeeService.Infrastructure.Services.EmployeeService;

internal class EmployeeModule: Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<EmployeeOpsService>()
            .As<IEmployeeOpsService>()
            .InstancePerLifetimeScope();
    }
}
