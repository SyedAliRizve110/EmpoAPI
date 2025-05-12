using Empo.BuildingBlocks.Application.Contracts;

namespace Empo.BuildingBlocks.Application.Configuration.Queries;

public abstract class QueryBase
{
    public Guid Id { get; }
    protected QueryBase()
    {
        this.Id = Guid.NewGuid();
    }
    protected QueryBase(Guid id)
        { this.Id = id; }
}

public abstract class QueryBase<TResult> :IQuery<TResult>
{
    public Guid Id { get; }
    protected QueryBase()
    {
        this.Id = Guid.NewGuid();
    }
    protected QueryBase(Guid id)
    {
        this.Id = id;
    }
}
