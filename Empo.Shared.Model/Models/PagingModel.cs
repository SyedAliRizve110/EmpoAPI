namespace Empo.Shared.Model.Models;

public class PagingModel
{
    public int PageSize { get; set; }
    public long TotalRecords { get; set; }
    public int CurrentPage { get; set; }

    public PagingModel(int currentPage, int pageSize, long totalRecords)
    {
        PageSize = pageSize;
        TotalRecords = totalRecords;
        CurrentPage = currentPage;
    }
}
