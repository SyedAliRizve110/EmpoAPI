using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class BadRequestException :AppException
{
    public BadRequestException(string message)
        : base(message, HttpStatusCode.BadRequest, "BAD_REQUEST") { }

}
