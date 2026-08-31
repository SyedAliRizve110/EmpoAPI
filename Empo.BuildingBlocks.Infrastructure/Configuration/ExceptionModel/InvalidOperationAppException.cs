using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class InvalidOperationAppException : AppException
{
    public InvalidOperationAppException(string message)
        : base(message, HttpStatusCode.UnprocessableEntity, "INVALID_OPERATION")
    { }
}
