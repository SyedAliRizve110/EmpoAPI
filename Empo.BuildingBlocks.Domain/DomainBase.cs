namespace Empo.BuildingBlocks.Domain;

public class DomainBase : IDomainBase
{
    public virtual Guid Id { get; set; }

    public DomainBase()
    {
            
    }

    protected void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            throw new BusinessRuleValidationException(rule);
        }
    }

    protected void SetId(Guid Id)
    {
               this.Id = Id;
    }

    public Guid GetId()
    {
        return Id;
    }
}
