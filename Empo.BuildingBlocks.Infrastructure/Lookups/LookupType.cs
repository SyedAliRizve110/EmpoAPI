using Empo.BuildingBlocks.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Empo.BuildingBlocks.Infrastructure.Lookups;

public class LookupType : DomainBase
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    [StringLength(64)]
    public string Alias { get; set; }
}
