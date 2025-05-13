namespace Empo.BuildingBlocks.Application;

public class InvalidCommandException : Exception
{
    public string Details { get; }
    public InvalidCommandException(string mmessage, string details) : base(mmessage)
    {
            this.Details = details;
    }
}
