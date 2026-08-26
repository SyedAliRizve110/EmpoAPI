using Empo.BuildingBlocks.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Infrastructure.Data.Entities.Employee;

public class EmployeeBankEntity : EntityBase
{
    [MaxLength(150)]
    public string BankName { get; set; }

    [MaxLength(150)]
    public string AccountHolderName { get; set; }

    [MaxLength(30)]
    public string AccountNumber { get; set; }

    [MaxLength(15)]
    public string IFSCCode { get; set; }

    [MaxLength(150)]
    public string BranchName { get; set; }

    [ForeignKey("Employee")]
    public Guid EmployeeId { get; set; }
    public EmployeeEntity Employee { get; set; }
}
