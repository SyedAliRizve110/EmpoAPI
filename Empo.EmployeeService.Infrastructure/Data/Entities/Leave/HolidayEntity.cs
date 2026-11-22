using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.Branch;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Leave;

public class HolidayEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public DateOnly Date { get; set; }
    public bool IsOptional { get; set; }

    [ForeignKey(nameof(BranchId))]
    public Guid? BranchId { get; set; }
    public BranchEntity? Branch { get; set; }
}
