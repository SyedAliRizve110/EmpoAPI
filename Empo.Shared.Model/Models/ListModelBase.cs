namespace Empo.Shared.Model.Models;

public abstract class ListModelBase<T, N> : ViewModelBase
    where T : ViewModelBase
    where N : FilterBaseModel
{
    public abstract IEnumerable<T> Collection { get; set; }
    public abstract N Filter { get; set; }
    public PagingModel Paging { get; set; }

}