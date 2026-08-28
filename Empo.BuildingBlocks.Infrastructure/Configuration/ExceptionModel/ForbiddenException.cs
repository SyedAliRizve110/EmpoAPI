using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message)
        : base(message, HttpStatusCode.Forbidden, "FORBIDDEN")
    { }

}
