namespace Empo.BuildingBlocks.Domain.Base;

public class Document : DomainBase
{
    public Guid FileId { get; set; }
    public string Note { get; set; }
    public string FileName { get; set; }
    public string FileExtension { get; set; }
    public bool DeletedByUser { get; set; }
    public Document()
    {
    }
}
   


