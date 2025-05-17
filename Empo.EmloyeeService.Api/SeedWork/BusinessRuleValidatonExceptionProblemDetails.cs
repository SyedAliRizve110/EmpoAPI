using Empo.BuildingBlocks.Domain.Rules;
using Microsoft.AspNetCore.Mvc;

namespace Empo.EmloyeeService.Api.SeedWork;

public class BusinessRuleValidatonExceptionProblemDetails : ProblemDetails
{
    public BusinessRuleValidatonExceptionProblemDetails(BusinessRuleValidationException exception)
    {
        this.Title = "Business rule validation error";
        this.Status = StatusCodes.Status409Conflict;
        this.Detail = exception.Details;
        this.Type = "https://somedomain/business-rule-validation-error";

    }
}
