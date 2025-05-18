namespace Empo.Shared.Utility.Extensions;

public static class GuidExtensions
{
    public static bool IsNullOrEmptyGuid(this Guid? guid)
    {
        if (guid == null || guid == Guid.Empty)
            return true;

        else return false;
    }
}
