using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public abstract class AppException : Exception
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorCode { get; }
    protected AppException(string message, HttpStatusCode statusCode, string errorCode)
    : base(message)
    {
        statusCode = statusCode;
        errorCode = errorCode;
    }
}
