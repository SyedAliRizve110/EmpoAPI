namespace Empo.BuildingBlocks.Domain.Interfaces;

public interface ITenantProvider
{
    public Guid GetTenantId();
}
