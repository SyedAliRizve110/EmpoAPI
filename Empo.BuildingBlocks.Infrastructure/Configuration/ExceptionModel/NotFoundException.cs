using System.Net;

namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class NotFoundException : AppException
{
    public NotFoundException(string message)
        : base(message, HttpStatusCode.NotFound, "NOT_FOUND")
    { }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with id '{key}' was not fount.", HttpStatusCode.NotFound, "NOT_FOUND") { }

    //Usage: throw new NotFoundExcption("role", "Name", "manager");

    public NotFoundException(string entityName, string fieldName, object fieldValue)
        : base($"{entityName} with {fieldName} '{fieldValue}' was not found.", HttpStatusCode.NotFound, "NOT_FOUND") { }

}
