namespace Empo.MediaService.Api.Authorization;

public static class MediaPermissions
{
    #region Media Post
    public const string PostCreate = "Post.Create";
    public const string PostView = "Post.View";
    public const string PostModerate = "Post.Moderate";
    public const string PostLike = "Post.Like";
    public const string PostComment = "Post.Comment";
    public const string PostDelete = "Post.Delete";
    #endregion

    #region Employee Document
    public const string EmployeeDocumentManageAny = "Document.Manage";
    public const string EmployeeDocumentUpload = "Document.Upload";
    public const string EmployeeDocumentGet = "Document.Get";
    public const string EmployeeDocumentDelete = "Document.Delete";
    #endregion
}