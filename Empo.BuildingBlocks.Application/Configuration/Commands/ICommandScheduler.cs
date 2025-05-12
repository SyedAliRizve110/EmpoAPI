
using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.BuildingBlocks.Application.Configuration.Commands
{
    public interface ICommandScheduler
    {
        Task EnqueueAsync<T>(ICommand<T> command);
    }
}
