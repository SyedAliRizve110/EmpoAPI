using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Media;

public class PostEntity : EntityBase
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public string? Text { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<PostMediaEntity> Media {  get; set; } = new List<PostMediaEntity>();
    public ICollection<PostCommentEntity> Comments { get; set; } = new List<PostCommentEntity>();
    public ICollection<PostLikeEntity> Likes { get; set; } = new List<PostLikeEntity>();
}
