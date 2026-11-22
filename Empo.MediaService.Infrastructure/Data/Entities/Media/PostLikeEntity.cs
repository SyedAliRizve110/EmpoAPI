using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.MediaService.Infrastructure.Data.Entities.Media;

public class PostLikeEntity : EntityBase
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; } = null!;
    public Guid UserId { get; set; }
}
