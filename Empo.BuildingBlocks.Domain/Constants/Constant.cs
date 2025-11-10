namespace Empo.BuildingBlocks.Domain.Constants;

public class Constant
{
    public static Guid DefaultTenantId { get { return new Guid("BCFA858A-0488-40EA-8EAA-A3ADCC5BF882"); } }  
    public static string NotificationQueueName { get { return "notification-queue"; } }
}
