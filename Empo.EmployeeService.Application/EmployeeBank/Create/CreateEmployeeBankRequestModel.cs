using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.EmployeeBank.Create
{
    public class CreateEmployeeBankRequest
    {
        [Required(ErrorMessage = "Employee Id is required")]
        public Guid EmployeeId { get; set; }

        [Required(ErrorMessage = "Bank Name is required")]
        [MaxLength(150)]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Account Holder Name is required")]
        [MaxLength(150)]
        public string AccountHolderName { get; set; }

        [Required(ErrorMessage = "Account Number is required")]
        [MaxLength(30)]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "IFSC Code is required")]
        [MaxLength(15)]
        public string IFSCCode { get; set; }

        [MaxLength(150)]
        public string BranchName { get; set; }
    }
}
