using System.ComponentModel;
using System.Runtime.Serialization;

namespace Empo.Shared.Model.Models;

public class FilterBaseModel
{
    private int _pageSize = 15;
    private int _pageNumber = 1;
    private string SortBy { get; set; }

    public SortDirection SortDIrection { get; set; }
    public int PageSize { get { return _pageSize; } set { if (value <= 0) { value = _pageSize; } else { _pageSize = value; } } }
    public int PageNumber { get { return _pageNumber; } set { if (value <= 0) { value = _pageNumber; } else { _pageNumber = value; } } }
}

public enum SortDirection
{
    [EnumMember(Value = "asc")]
    Asc = 1,
    [EnumMember(Value = "desc")]
    Desc = 2
}