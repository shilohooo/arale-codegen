using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of supported languages
/// </summary>
public enum LanguageType
{
    [Description("C#")] CSharp = 1,
    [Description("Java")] Java = 2,
    [Description("JS")] JavaScript = 3,
    [Description("TS")] TypeScript = 4
}
