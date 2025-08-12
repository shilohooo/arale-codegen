using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Models.Dto;

/// <summary>
/// </summary>
public class CodeGenerateResp
{
    /// <summary>
    ///     Target type
    /// </summary>
    public required TargetType TargetType { get; set; }

    /// <summary>
    ///     File name
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    ///     Code
    /// </summary>
    public required string Code { get; set; }

    /// <summary>
    ///     Language
    /// </summary>
    public required LanguageType Language { get; set; }
}
