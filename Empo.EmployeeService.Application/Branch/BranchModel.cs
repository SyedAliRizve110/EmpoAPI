using Empo.EmployeeService.Application.CommonRequestModel;
using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Branch;

public class BranchModel
{
    [Required(ErrorMessage = "Id is required")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Code is required")]
    public string BranchCode { get; set; }
    public AddressModel Address { get; set; }
    public bool IsActive { get; set; }
}
