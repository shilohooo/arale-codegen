using System.ComponentModel;

namespace Arale.CodeGen.Extensions;

/// <summary>
///     Enum extension methods
/// </summary>
public static class EnumExtensions
{
    /// <summary>
    ///     Get Enum description, if not exist, return Enum name
    /// </summary>
    /// <param name="value">Enum instance</param>
    /// <returns>Return the description of Enum, if it has, otherwise Enum name</returns>
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        if (memberInfo.Length <= 0) return value.ToString();
        var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : value.ToString();
    }
}
