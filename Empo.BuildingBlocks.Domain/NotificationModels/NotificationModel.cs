namespace Empo.BuildingBlocks.Domain.NotificationModels;

public class NotificationModel
{
    public dynamic Attributes;
    public List<NotificationPersonalizedModel> PersonalizedModelList;
    public Guid NotificationTypeId;
    public EmailSourceEntityType SourceEntityType;
    public Guid SourceEntityId;
    public Guid TenantId;
    public NotificationModel()
    {
    }
    public static NotificationModel Create(dynamic _attributes, List<NotificationPersonalizedModel> _personalizedModelList,
        Guid _notificationTypeId, EmailSourceEntityType _sourceEntityType, Guid _sourceEntityId, Guid TenantId)
    {
        return new NotificationModel()
        {
            Attributes = _attributes,
            PersonalizedModelList = _personalizedModelList,
            NotificationTypeId = _notificationTypeId,
            SourceEntityType = _sourceEntityType,
            SourceEntityId = _sourceEntityId,
        };
    }
}
