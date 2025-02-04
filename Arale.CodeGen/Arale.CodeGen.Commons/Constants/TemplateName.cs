using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of Scriban template file name
/// </summary>
public enum TemplateName
{
    /// <summary>
    ///     C# class template
    /// </summary>
    [Description("CSharp/CSharpClass")] CSharpClass,

    /// <summary>
    ///     Java class template
    /// </summary>
    [Description("Java/JavaClass")] JavaClass
}
