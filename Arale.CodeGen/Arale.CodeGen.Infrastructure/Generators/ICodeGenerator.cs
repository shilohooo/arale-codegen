using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Generator
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    ///     Get supported target type
    /// </summary>
    TargetType SupportedTargetType { get; }

    /// <summary>
    ///     Generate code by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>code generate results</returns>
    Task<List<CodeGenerateResp>> GenerateBySql(CodeGenerateReq codeGenerateReq);

    /// <summary>
    ///     Generate code by JSON object or array
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>code generate results</returns>
    Task<List<CodeGenerateResp>> GenerateByJson(CodeGenerateReq codeGenerateReq);
}
