using Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;

namespace Empo.EmployeeService.Application.EmployeeDocument;

public record DownloadedFile(Stream Stream, string Contentype, string FileName);
public interface IEmployeeDocumentService
{
    Task<DocumentModel> UploadAsync(Guid employeeId, UploadEmployeeDocumentRequest request, Guid uploadedByUserId);
    Task<List<DocumentModel>> ListAsync(Guid employeeId);
    Task<DownloadedFile> DownloadAsync(Guid employeeId, Guid documentId);
    Task DeleteAsync(Guid employeeId, Guid documentId);
    Task<DocumentModel> VerifyAsync(Guid employeeId, Guid documentId, Guid verifiedByUserId);
}
