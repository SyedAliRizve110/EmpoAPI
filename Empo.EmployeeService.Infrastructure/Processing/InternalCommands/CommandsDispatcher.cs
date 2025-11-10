using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Reflection;

namespace Empo.EmployeeService.Infrastructure.Processing.InternalCommands;

public class CommandsDispatcher : ICommandsDispatcher
{
    private readonly IMediator _mediator;
    private readonly EmployeeContext _Context;

    public CommandsDispatcher(
        IMediator mediator,
        EmployeeContext Context)
    {
        this._mediator = mediator;
        this._Context = Context;
    }

    public async Task DispatchCommandAsync(Guid id)
    {
        var internalCommand = await this._Context.InternalCommand.SingleOrDefaultAsync(x => x.Id == id);

        Type type = Assembly.GetAssembly(typeof(EmployeeContext)).GetType(internalCommand.Type);
        dynamic command = JsonConvert.DeserializeObject(internalCommand.Data, type);

        internalCommand.ProcessedDate = DateTime.UtcNow;

        await this._mediator.Send(command);
    }
}