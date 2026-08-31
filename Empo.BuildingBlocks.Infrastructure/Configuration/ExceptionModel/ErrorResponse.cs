namespace Empo.BuildingBlocks.Infrastructure.Configuration.ExceptionModel;

public class ErrorResponse
{
    public string ErrorCode { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string? TraceId { get; set; }

    // Only populated for validation-style errors with multiple
    // field-level messages (e.g. FluentValidation). Null otherwise.
    public IDictionary<string, string[]>? Errors { get; set; }
}
