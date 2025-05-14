using Empo.BuildingBlocks.Domain.Base;
using Empo.BuildingBlocks.Domain.Interfaces;

namespace Empo.BuildingBlocks.Domain.Rules;

public class RecordNotFoundRule<T> : IBusinessRule
{
    private readonly DomainBase _domain;
    private readonly Guid _identifier;

    public RecordNotFoundRule(DomainBase domain, Guid identifier)
    {
        this._domain = domain;
        this._identifier = identifier;
    }
    public string Message => $"Record not found for {typeof(T).Name} with Id: {_identifier}";

    public bool IsBroken()
    {
        if (this._domain == null)
        {
            return true;
        }
        return false;
    }
}
