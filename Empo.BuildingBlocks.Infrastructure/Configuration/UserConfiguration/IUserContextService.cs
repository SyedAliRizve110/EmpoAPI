namespace Empo.BuildingBlocks.Infrastructure.Configuration.UserConfiguration;

public interface IUserContextService
{
    Guid? GetUserId();
    Guid? GetEmployeeId();
    string GetEmail();
    string? GetRole();
}
