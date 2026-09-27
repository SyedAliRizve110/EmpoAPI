namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class MediaTypeException : AppException
{
    public MediaTypeException(string message)
        : base(message, System.Net.HttpStatusCode.UnsupportedMediaType, "UNSUPPORTED_MEDIA_TYPE")
    { }
}

public class MediaNotFoundException : AppException
{
    public MediaNotFoundException(string message)
        : base(message, System.Net.HttpStatusCode.NotFound, "NO_TEXT_OR_MEDIA_FOUND")
    { }
}