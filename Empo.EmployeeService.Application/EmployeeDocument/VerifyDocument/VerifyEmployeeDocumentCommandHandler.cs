using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.EmployeeService.Application.EmployeeDocument.VerifyDocument;

public class VerifyEmployeeDocumentCommandHandler : ICommandHandler<VerifyEmployeeDocumentCommand, DocumentModel>
{
    public IEmployeeDocumentService _service { get; set; }
    public VerifyEmployeeDocumentCommandHandler(IEmployeeDocumentService service)
    {
        _service = service;
    }

    public async Task<DocumentModel> Handle(VerifyEmployeeDocumentCommand request, CancellationToken cancellationToken)
    {
        return await _service.VerifyAsync(request._employeeId, request._documentId, request._currentUserId);
    }
}
