using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeDocument.DeleteDocument;

public class DeleteEmployeeDocumentCommand : CommandBase<bool>
{
    public Guid _employeeId { get; set; }
    public Guid _documentId { get; set; }

    public static DeleteEmployeeDocumentCommand Create(Guid employeeId, Guid documentId)
    {
        return new DeleteEmployeeDocumentCommand
        {
            _employeeId = employeeId,
            _documentId = documentId
        };
    }
}
