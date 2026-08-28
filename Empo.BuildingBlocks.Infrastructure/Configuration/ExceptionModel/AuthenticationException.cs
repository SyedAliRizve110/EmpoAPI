using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class AuthenticationException : AppException
{
    public AuthenticationException(string message = "Invalid email or password")
        : base(message, HttpStatusCode.Unauthorized, "AUTHENTICATION_FAILED")
    {
    }
}
