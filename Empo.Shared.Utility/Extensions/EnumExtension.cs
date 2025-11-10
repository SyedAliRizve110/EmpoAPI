using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Empo.Shared.Utility.Extensions;

public static class EnumExtension
{
    public static IEnumerable<KeyValuePair<long, string>> GetNameValuePairs(this System.Type enumType)
    {
        return (from object value in System.Enum.GetValues(enumType) select GetPair(value)).ToArray();
    }

    private static KeyValuePair<long, string> GetPair(object value)
    {
        return new KeyValuePair<long, string>(Convert.ToInt64(value), ((System.Enum)value).GetDisplayName());
    }

    public static string GetDisplayName(this Enum value)
    {
        FieldInfo fieldInfo = value.GetType().GetField(value.ToString());

        DisplayAttribute[] attributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);

        if (attributes != null && attributes.Length > 0)
            return attributes[0].Name;
        else
            return value.ToString();

    }

    public static string GetDescription(this System.Enum enumValue)
    {
        var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
        if (memberInfo.Length < 1) return enumValue.ToString();

        var descriptionAttribute = (DescriptionAttribute)memberInfo.FirstOrDefault().GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

        if (descriptionAttribute == null) return enumValue.ToString();
        return descriptionAttribute.Description;
    }

    public static bool IsNullableEnum(this Type t)
    {
        Type u = Nullable.GetUnderlyingType(t);
        return (u != null) && u.IsEnum;
    }
}
