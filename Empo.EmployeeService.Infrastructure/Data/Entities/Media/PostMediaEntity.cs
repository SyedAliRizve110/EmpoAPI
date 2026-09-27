using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Application.Enums;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Media;

public class PostMediaEntity : EntityBase
{
    public Guid PostId { get; set; }
    public PostEntity Post { get; set; } = null!;
    public MediaType MediaType { get; set; }
    public string StorageKey { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public long SizeInBytes { get; set; }
    public int SortOrder { get; set; }
}
