using Empo.EmployeeService.Application.Configuration;
using System.Reflection;

namespace Empo.EmployeeService.Infrastructure.Processing;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(BaseFile).Assembly;
}
