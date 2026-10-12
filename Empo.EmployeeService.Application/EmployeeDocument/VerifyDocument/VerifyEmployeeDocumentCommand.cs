using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeDocument.VerifyDocument;

public class VerifyEmployeeDocumentCommand : CommandBase<DocumentModel>
{
    public Guid _employeeId { get; set; }
    public Guid _documentId { get; set; }
    public Guid _currentUserId { get; set; }

    public static VerifyEmployeeDocumentCommand Create(Guid employeeId, Guid documentId, Guid currentUserId)
    {
        return new VerifyEmployeeDocumentCommand
        {
            _employeeId = employeeId,
            _documentId = documentId,
            _currentUserId = currentUserId
        };
    }
}
