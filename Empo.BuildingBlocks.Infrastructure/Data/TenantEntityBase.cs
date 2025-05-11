namespace Empo.BuildingBlocks.Infrastructure.Data;

public abstract class TenantEntityBase : EntityBase
{
    public void SetTenantId(Guid _TenantId)
    {
        TenantId = _TenantId;
    }
    public Guid TenantId { get; set; }
}
