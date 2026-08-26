using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeBank;

public class EmployeeBankModel
{
    public Guid Id { get; set; }
    public Guid EmployeeId { get; set; }

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
}
