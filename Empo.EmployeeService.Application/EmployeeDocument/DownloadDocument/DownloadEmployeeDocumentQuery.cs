using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeDocument.DownloadDocument;

public class DownloadEmployeeDocumentQuery : CommandBase<DownloadedFile>
{
    public Guid _employeeId { get; set; }
    public Guid _documentId { get; set; }
    public static DownloadEmployeeDocumentQuery Create(Guid employeeId, Guid documentId)
    {
        return new DownloadEmployeeDocumentQuery
        {
            _employeeId = employeeId,
            _documentId = documentId
        };
    }
}
