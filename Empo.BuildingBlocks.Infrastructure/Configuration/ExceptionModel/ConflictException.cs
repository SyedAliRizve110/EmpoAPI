using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message, HttpStatusCode.Conflict, "CONFLICT")
    { }
}
