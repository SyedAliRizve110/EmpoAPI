using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.User;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Media;

public class PostCommentEntity : EntityBase
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; } = null!;
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
