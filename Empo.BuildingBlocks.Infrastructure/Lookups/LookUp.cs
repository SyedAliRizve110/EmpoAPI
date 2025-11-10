using Empo.BuildingBlocks.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Empo.BuildingBlocks.Infrastructure.Lookups;

public class LookUp : DomainBase
{
    [Required]
    public long LookupTypeId { get; set; }

    [Required]
    [StringLength(256)]
    public string Name { get; set; }

    [Required]
    [StringLength(128)]
    public string Alias { get; set; }

    public int RelativeOrder { get; set; }


    [ForeignKey("LookupTypeId")]
    public virtual LookupType LookupType { get; set; }


}
