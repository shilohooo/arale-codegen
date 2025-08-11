using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of supported languages
/// </summary>
public enum LanguageType
{
    [Description("cs")] CSharp = 1,
    [Description("java")] Java = 2,
    [Description("js")] JavaScript = 3,
    [Description("ts")] TypeScript = 4
}
