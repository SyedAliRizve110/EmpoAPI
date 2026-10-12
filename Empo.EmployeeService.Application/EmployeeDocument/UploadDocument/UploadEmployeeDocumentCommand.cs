using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;

public class UploadEmployeeDocumentCommand : CommandBase<DocumentModel>
{
    public Guid _employeeId { get; set; }
    public UploadEmployeeDocumentRequest _request { get; set; }
    public Guid _uploadedByUserId { get; set; }
    public static UploadEmployeeDocumentCommand Create(UploadEmployeeDocumentRequest request, Guid employeeId, Guid uploadedByUserId)
    {
        return new UploadEmployeeDocumentCommand
        {
            _uploadedByUserId = uploadedByUserId,
            _employeeId = employeeId,
            _request = request
        };
    }
}
