using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.EmployeeService.Application.EmployeeDocument.DeleteDocument;

public class DeleteEmployeeDocumentCommandHandler : ICommandHandler<DeleteEmployeeDocumentCommand, bool>
{
    public IEmployeeDocumentService _service { get; set; }

    public DeleteEmployeeDocumentCommandHandler(IEmployeeDocumentService service)
    {
        _service = service;
    }

    public async Task<bool> Handle(DeleteEmployeeDocumentCommand request, CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(request._employeeId, request._documentId);
        return true;
    }
}
