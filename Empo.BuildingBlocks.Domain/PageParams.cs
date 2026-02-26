namespace Empo.BuildingBlocks.Domain;

public class PageParams
{
    const int maxPageSize = 100;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 10;

    public int PageSize
    {
        get
        {
            return _pageSize;
        }

        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }

    public PageParams(int pageNumber, int pageSize) 
    {
        PageNumber = pageNumber;
        pageSize = pageSize;
    }
}
