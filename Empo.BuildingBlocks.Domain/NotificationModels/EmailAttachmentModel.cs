using System.Security.AccessControl;

namespace Empo.BuildingBlocks.Domain.NotificationModels;

public class EmailAttachmentModel
{
    public ResourceType ResourceType { get; set; }

    public string FilePath { get; set; }
    public string GiftCode { get; set; }

    public string ContentType { get; set; }

    public string ContentName { get; set; }
    public EmailAttachmentModel()
    {

    }
}
