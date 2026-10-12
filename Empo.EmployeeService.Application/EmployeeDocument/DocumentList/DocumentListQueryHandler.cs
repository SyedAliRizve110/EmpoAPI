using Empo.BuildingBlocks.Application.Configuration;

namespace Empo.EmployeeService.Application.EmployeeDocument.GetDocument;

public class DocumentListQueryHandler : ICommandHandler<DocumentListQuery, List<DocumentModel>>
{
    public IEmployeeDocumentService _service { get; set; }

    public DocumentListQueryHandler(IEmployeeDocumentService serivce)
    {
        _service = serivce;
    }

    public async Task<List<DocumentModel>> Handle(DocumentListQuery request, CancellationToken cancellationToken)
    {
        return await _service.ListAsync(request._employeeId);
    }
}
