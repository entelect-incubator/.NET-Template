namespace Common.Extensions;

using System.Reflection;
using System.Runtime.Serialization;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        var fi = value.GetType().GetField(value.ToString());
        return fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Length != 0 ? attributes.First().Description : value.ToString();
    }

    public static T GetEnumFromDescription<T>(this string description)
        where T : struct
    {
        var fields = typeof(T).GetFields();
        for (var i = 0; i < fields.Length; i++)
        {
            var field = fields[i];
            if (field.GetCustomAttributes(typeof(DescriptionAttribute), false).Length > 0
                && string.Equals(((DescriptionAttribute)field.GetCustomAttributes(typeof(DescriptionAttribute), false)[0]).Description, description))
            {
                return (T)field.GetValue(null);
            }
        }

        throw new ArgumentException("No enum member found with the specified description.", nameof(description));
    }

    public static int GetValue(this Enum element) => Convert.ToInt32(element);

    public static string GetValueAsString(this Enum element) => Convert.ToInt32(element).ToString();

    public static string? GetEnumMemberValue<T>(this T value)
    where T : struct, IConvertible => typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .SingleOrDefault(x => x.Name == value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;

    public static bool IsNull<T>(this T? val)
    where T : struct
        => val == null;

    public static bool IsNull<T>(this T val)
    where T : Enum
        => val == null;
}
