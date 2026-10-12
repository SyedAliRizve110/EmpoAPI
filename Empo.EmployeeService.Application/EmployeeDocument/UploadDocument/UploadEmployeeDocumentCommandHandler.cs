using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.EmployeeService.Application.EmployeeDocument.UploadDocument;

public class UploadEmployeeDocumentCommandHandler : ICommandHandler<UploadEmployeeDocumentCommand, DocumentModel>
{
    public IEmployeeDocumentService _service { get; }
    public UploadEmployeeDocumentCommandHandler(IEmployeeDocumentService service)
    {
        _service = service;
    }

    public async Task<DocumentModel> Handle(UploadEmployeeDocumentCommand command, CancellationToken cancellationToken)
    {
        var document = _service.UploadAsync(command._employeeId, command._request, command._uploadedByUserId);
        return document.Result;
    }
}
