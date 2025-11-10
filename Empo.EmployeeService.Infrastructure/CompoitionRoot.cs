using Autofac;

namespace Empo.EmployeeService.Infrastructure;

public static class CompositionRoot
{
    private static IContainer _container;
    public static void SetContainer(IContainer container)
    {
        _container = container;
    }
    internal static ILifetimeScope BeginLifeTimeScope()
    {
        return _container.BeginLifetimeScope();
    }
}
