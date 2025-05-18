using Empo.Shared.Utility.Constants;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Empo.EmloyeeService.Api.Configuration;

public class OperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }
        operation.Parameters.Add(new OpenApiParameter()
        {
            Name = "tenant-id",
            In = ParameterLocation.Header,
            Description = "tenant-id",
            Required = true,
            Example = new OpenApiString(Constant.DefaultTenantId.ToString())
        });
    }
}
