namespace Empo.BuildingBlocks.Application.Contracts;

public interface IModule
{
    Task<TResult> ExecuteCommandAsync<TResult>(ICommand<TResult> command);
    Task ExecuteCommandAsync(ICommand command);
    Task<TResult> ExecutQueryAsync<TResult>(IQuery<TResult> query);
}
