using Empo.BuildingBlocks.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.EmployeeService.Domain.Shared;

public class Phone : DomainBase
{
    public string CountryCode { get; set; }

    public string Number { get; set; }

    public string Extension { get; set; }

}
