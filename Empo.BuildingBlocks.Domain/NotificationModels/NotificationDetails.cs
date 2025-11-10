namespace Empo.BuildingBlocks.Domain.NotificationModels;

public class NotificationDetails
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsEmail { get; set; }
    public bool IsSMS { get; set; }
}
