using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of generate target types
/// </summary>
public enum TargetType
{
    [Description("C# Class")] CSharpClass = 1,

    // [Description("C# Record")] CSharpRecord = 2,
    // [Description("C# Struct")] CSharpStruct = 3,
    [Description("Java Class")] JavaClass = 4
}
