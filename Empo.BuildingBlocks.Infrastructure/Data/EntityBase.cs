namespace Empo.BuildingBlocks.Infrastructure.Data;

public abstract class EntityBase
{
    public Guid Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModifieed { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid ModifiedBy { get; set; }
    public EntityBase()
    {
    }

    public void SetDataRecorderMetadata(Guid? userId, bool IsNew)
    {
        if (IsNew)
        {

            DateCreated = DateTime.UtcNow;
            CreatedBy = userId ?? Guid.Empty;
            DateModifieed = DateTime.UtcNow;
            ModifiedBy = userId ?? Guid.Empty;
        }
        else
        {
            DateModifieed = DateTime.UtcNow;
            ModifiedBy = userId ?? Guid.Empty;
        }
    }
}
