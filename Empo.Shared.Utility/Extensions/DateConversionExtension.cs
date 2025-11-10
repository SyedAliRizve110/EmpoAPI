namespace Empo.Shared.Utility.Extensions;

public static class DateConversionExtension
{
   

    public static DateTime GetStartDateTime(DateOnly dateOnly, TimeOnly? timeOnly)
    {

        if (default(TimeOnly) == timeOnly || timeOnly==null)
        {
            return dateOnly.ToDateTime(TimeOnly.MinValue);
        }
        return dateOnly.ToDateTime(timeOnly.Value);
    }
    public static DateTime GetEndDateTime(DateOnly dateOnly, TimeOnly? timeOnly)
    {
        if(default(TimeOnly)== timeOnly || timeOnly == null)
        {
            return dateOnly.ToDateTime(TimeOnly.MaxValue);
        }
        return dateOnly.ToDateTime(timeOnly.Value);
    }
}
