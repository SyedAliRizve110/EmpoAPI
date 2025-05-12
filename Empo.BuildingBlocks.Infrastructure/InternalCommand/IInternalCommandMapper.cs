namespace Empo.BuildingBlocks.Infrastructure.InternalCommand;

public interface IInternalCommandMapper
{
    string GetName(Type type);
    Type GetType(string name);
}
