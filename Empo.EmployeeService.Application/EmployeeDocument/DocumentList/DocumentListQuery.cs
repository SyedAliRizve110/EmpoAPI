using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.EmployeeService.Application.EmployeeDocument.GetDocument;

public class DocumentListQuery : CommandBase<List<DocumentModel>>
{
    public Guid _employeeId { get; set; }
    public static DocumentListQuery Create(Guid employeeId)
    {
        return new DocumentListQuery
        {
            _employeeId = employeeId,
        };
    }
}
