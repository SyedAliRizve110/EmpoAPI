using Empo.BuildingBlocks.Infrastructure.Data;
using Empo.EmployeeService.Infrastructure.Data.Entities.CommonEntity;
using Empo.EmployeeService.Infrastructure.Data.Entities.Employee;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Branch;

public class BranchEntity : EntityBase
{
    public string Name { get; set; }
    public string BranchCode { get; set; }
    public Guid AddressId { get; set; }
    [ForeignKey(nameof(AddressId))]
    public AddressEntity Address { get; set; }
    public bool IsActive { get; set; }
    public Guid ManagerId { get; set; }
    public ICollection<EmployeeEntity> BranchEmployee { get; set; }
}
