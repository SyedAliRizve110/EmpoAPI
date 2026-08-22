using Empo.BuildingBlocks.Infrastructure.Data;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.User;

public class RefreshTokenEntity : EntityBase
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpiresAt { get; set; }
    public UserEntity User { get; set; }
}
