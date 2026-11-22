using Empo.BuildingBlocks.Application.Configuration;
using Empo.BuildingBlocks.Application.Contracts;
using Empo.MediaService.Application.EmployeeDocument.GetDocument;

namespace Empo.MediaService.Application.EmployeeDocument.DownloadDocument;

public class DownloadEmployeeDocumentQueryHandler : ICommandHandler<DownloadEmployeeDocumentQuery, DownloadedFile>
{
    public IEmployeeDocumentService _service { get; }

    public DownloadEmployeeDocumentQueryHandler(IEmployeeDocumentService service)
    {
            _service = service;
    }

    public async Task<DownloadedFile> Handle(DownloadEmployeeDocumentQuery request, CancellationToken cancellationToken)
    {
        return await _service.DownloadAsync(request._employeeId, request._documentId);
    }
}
