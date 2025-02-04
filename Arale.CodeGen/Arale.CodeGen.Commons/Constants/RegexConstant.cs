using System.Text.RegularExpressions;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     regex patterns
/// </summary>
public static partial class RegexConstant
{
    /// <summary>
    ///     table column length pattern
    /// </summary>
    [GeneratedRegex(@"\((\d+)\)")]
    public static partial Regex LengthPattern();
}
