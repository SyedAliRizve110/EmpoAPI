using System.Data;

namespace Empo.EmployeeService.Application.Configuration;

public interface ISqlConnectionFactory
{
    IDbConnection GetOpenConnection();
}
