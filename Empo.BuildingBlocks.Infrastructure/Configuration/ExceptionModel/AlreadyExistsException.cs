using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class AlreadyExistsException : AppException
{
    public AlreadyExistsException(string message)
        : base(message, System.Net.HttpStatusCode.Conflict, "ALREADY_EXISTS")
    {
    }

    public AlreadyExistsException(string entityName, string fieldName, object fieldValue)
        : base($"{entityName} with '{fieldName}' already exists.", HttpStatusCode.Conflict, "ALREADY_EXISTS")
    {
    }


}
