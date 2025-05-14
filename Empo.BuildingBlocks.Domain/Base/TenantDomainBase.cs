namespace Empo.BuildingBlocks.Domain.Base;

public class TenantDomainBase : DomainBase
{
    public virtual Guid TenantId { get; set; }
    public TenantDomainBase() : base()
    { }
}
