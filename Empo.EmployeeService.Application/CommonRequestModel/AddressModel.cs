using System.ComponentModel.DataAnnotations;

namespace Empo.EmployeeService.Application.CommonRequestModel;

public class AddressModel
{
    public Guid Id { get; set; }
    [MaxLength(256)]
    public string Address1 { get; set; }

    [MaxLength(256)]
    public string Address2 { get; set; }

    [MaxLength(60)]
    public string City { get; set; }

    [MaxLength(60)]
    public string State { get; set; }

    [MaxLength(6)]
    public string ZipCode { get; set; }

    [MaxLength(100)]
    public string Country { get; set; }

}
