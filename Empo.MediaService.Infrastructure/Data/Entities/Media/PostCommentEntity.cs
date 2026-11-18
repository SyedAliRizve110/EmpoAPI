using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.MediaService.Infrastructure.Data.Entities.Media;

public class PostCommentEntity : EntityBase
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; } = null!;
    public Guid UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
