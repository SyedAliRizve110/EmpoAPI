using Empo.BuildingBlocks.Domain.Interfaces;
using Empo.BuildingBlocks.Domain.Rules;

namespace Empo.BuildingBlocks.Domain.Base;

public abstract class DomainBase :IDomainBase
{
    public virtual Guid Id { get; set; }

    protected DomainBase()
    {
            
    }
    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    protected void SetId(Guid id)
    {
        Id = id;
    }
    public Guid GetId()
    {
        return Id;
    }
}
