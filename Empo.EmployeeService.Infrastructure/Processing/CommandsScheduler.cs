using Dapper;
using Empo.BuildingBlocks.Application.Configuration.Commands;
using Empo.BuildingBlocks.Application.Contracts;
using Empo.EmployeeService.Application.Configuration;
using Newtonsoft.Json;

namespace Empo.EmployeeService.Infrastructure.Processing;

public class CommandsScheduler : ICommandScheduler
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public CommandsScheduler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }
    public async Task EnqueueAsync<T>(ICommand<T> command)
    {
        var connection = this._sqlConnectionFactory.GetOpenConnection();

        const string sqlInsert = "INSERT INTO [app].[InternalCommands] ([Id], [EnqueueDate] , [Type], [Data]) VALUES " +
                                 "(@Id, @EnqueueDate, @Type, @Data)";

        await connection.ExecuteAsync(sqlInsert, new
        {
            command.Id,
            EnqueueDate = DateTime.UtcNow,
            Type = command.GetType().FullName,
            Data = JsonConvert.SerializeObject(command)
        });
    }
}