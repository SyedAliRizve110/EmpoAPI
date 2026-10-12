using Microsoft.AspNetCore.Http;

namespace Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;

public class UploadEmployeeDocumentRequest
{
    public IFormFile File { get; set; } = null!;
    public string DocumentType { get; set; } = string.Empty; 
}
