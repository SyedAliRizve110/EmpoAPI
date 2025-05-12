
namespace Empo.BuildingBlocks.Infrastructure.InternalCommand;

public class InternalCommandMaper : IInternalCommandMapper
{
    private readonly BiDictionary<string, Type> _internalCommandMap;

    public InternalCommandMaper(BiDictionary<string, Type> internalCommandMap)
    {
            _internalCommandMap = internalCommandMap;
    }
    public string GetName(Type type)
    {
        return _internalCommandMap.TryGetSecond(type, out var name) ? name : null;
    }

    public Type GetType(string name)
    {
        return _internalCommandMap.TryGetFirst(name, out var type) ? type : null;
    }
}
