namespace Empo.EmployeeService.Application.EmployeeDocument;

public class DocumentModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }
    public string DocumentType { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public DateTime UploadedAt { get; set; }
    public bool IsVerified { get; set; }

}
