using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.Branch.CreateBranch;

public class CreateBranchRequest
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Code is required")]
    public string BranchCode { get; set; }
    public CreateBranchAddressModel Address { get; set; }
}

public class CreateBranchAddressModel
{
    [Required(ErrorMessage = "Address1 is required")]
    [MaxLength(256)]
    public string Address1 { get; set; }

    [MaxLength(256)]
    public string Address2 { get; set; }

    [Required(ErrorMessage = "City is required")]
    [MaxLength(60)]
    public string City { get; set; }

    [Required(ErrorMessage = "State is required")]
    [MaxLength(60)]
    public string State { get; set; }

    [Required(ErrorMessage = "Zip Code is required")]
    [MaxLength(6)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [MaxLength(100)]
    public string Country { get; set; }

}