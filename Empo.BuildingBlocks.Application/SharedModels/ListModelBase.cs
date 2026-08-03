namespace Empo.BuildingBlocks.Application.SharedModels;

public abstract class ListModelBase<T, N> : ViewModelBase
    where T : ViewModelBase
    where N : ModelFilterBase
{
    public abstract IEnumerable<T> Collection { get; set; }
    public abstract N Filter { get; set; }
    public PagingModel PagingModel { get; set; }
}
