using Empo.BuildingBlocks.Application;
using Empo.BuildingBlocks.Application.Commands;
using Empo.BuildingBlocks.Application.Contracts;
using MediatR;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Events;

namespace Empo.EmployeeService.Infrastructure.Logging;


internal class LoggingCommandHandlerDecorator<T> : ICommandHandler<T> where T : ICommand
{
    private readonly ILogger _logger;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly ICommandHandler<T> _decorated;

    public LoggingCommandHandlerDecorator(
        ILogger logger,
        IExecutionContextAccessor executionContextAccessor,
        ICommandHandler<T> decorated)
    {
        _logger = logger;
        _executionContextAccessor = executionContextAccessor;
        _decorated = decorated;
    }

    public async Task<Unit> Handle(T request, CancellationToken cancellationToken)
    {
        using (
             LogContext.Push(
                 new RequestLogEnricher(_executionContextAccessor),
                 new CommandLogEnricher(request)))
        {
            try
            {
                _logger.Information(
                    "Executing command {Command}",
                    request.GetType().Name);
                 await _decorated.Handle(request, cancellationToken);

                this._logger.Information("Command {Command} processed sucessful", request.GetType().Name);

                return Unit.Value;
            }
            catch (Exception exception)
            {

                this._logger.Error(exception, "Command {Command} processing failed", request.GetType().Name);
                throw;
            }
        }
    }

    Task IRequestHandler<T>.Handle(T request, CancellationToken cancellationToken)
    {
        return Handle(request, cancellationToken);
    }

    private class CommandLogEnricher : ILogEventEnricher
    {
        private readonly ICommand _command;
        public CommandLogEnricher(ICommand command)
        {
            _command = command;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            logEvent.AddOrUpdateProperty(new LogEventProperty("Context", new ScalarValue($"Command:{_command.Id.ToString()}")));
        }
    }

    private class RequestLogEnricher : ILogEventEnricher
    {
        private readonly IExecutionContextAccessor _executionContextAccessor;
        public RequestLogEnricher(IExecutionContextAccessor executionContextAccessor)
        {
            _executionContextAccessor = executionContextAccessor;
        }
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (_executionContextAccessor.IsAvailable)
            {
                logEvent.AddOrUpdateProperty(new LogEventProperty("CorrelationId", new ScalarValue(_executionContextAccessor.CorrelationId)));
            }
        }
        public void Cascade()
        {
            for (int i = 0; i < 6; i++)
            {
                var tech = i;
            }
        }
    }
}