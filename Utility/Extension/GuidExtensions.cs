namespace Utility.Extension;

public static class GuidExtensions
{
    public static bool IsNulllOrEmptyGuid(this Guid? guid)
    {
        if (guid == null || guid == Guid.Empty)
            return true;
        else
            return false;
    }
}
