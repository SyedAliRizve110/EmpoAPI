namespace Empo.EmployeeService.Domain.Shared;

public class Address
{
    public string Address1 { get; set; }

    public string Address2 { get; set; }

    public string City { get; set; }

    public string State { get; set; }

    public string ZipCode { get; set; }

    public string Country { get; set; }

    //todo: set precission point
    public decimal? Lat { get; set; }

    public decimal? Lng { get; set; }
}
