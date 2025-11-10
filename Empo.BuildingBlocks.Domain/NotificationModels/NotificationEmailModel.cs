namespace Empo.BuildingBlocks.Domain.NotificationModels;

public class NotificationEmailModel
{
    public string RecepientEmailTo;
    public string RecepientEmailCc;
    public List<EmailAttachmentModel> EmailAttachments;
    public static NotificationEmailModel Create(string _recepientEmailTo, string _recepientEmailCc, List<EmailAttachmentModel> _emailAttachments)
    {
        return new NotificationEmailModel()
        {
            RecepientEmailTo= _recepientEmailTo,
            RecepientEmailCc= _recepientEmailCc,
            EmailAttachments= _emailAttachments
        };
    }
}
