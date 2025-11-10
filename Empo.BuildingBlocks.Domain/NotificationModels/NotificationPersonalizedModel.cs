namespace Empo.BuildingBlocks.Domain.NotificationModels;

public class NotificationPersonalizedModel
{
    public dynamic Attributes;
    public EmailSourceEntityType SourceEntityItemType;
    public Guid SourceEntityItemId;
    public NotificationEmailModel EmailConfig;
    public NotificationSMSModel SMSConfig;
    public NotificationPersonalizedModel()
    {
    }
    public static NotificationPersonalizedModel Create(dynamic _attributes, EmailSourceEntityType _sourceEntityItemType, Guid _sourceEntityItemId,
        NotificationEmailModel _emailConfig, NotificationSMSModel _sMSConfig)
    {
        return new NotificationPersonalizedModel()
        {
            Attributes = _attributes,
            SourceEntityItemType = _sourceEntityItemType,
            SourceEntityItemId = _sourceEntityItemId,
            EmailConfig = _emailConfig,
            SMSConfig = _sMSConfig,
        };
    }
}