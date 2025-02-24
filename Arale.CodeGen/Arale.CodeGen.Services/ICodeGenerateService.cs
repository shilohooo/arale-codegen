using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Services;

/// <summary>
///     Code generate service
/// </summary>
public interface ICodeGenerateService
{
    /// <summary>
    ///     Generate code by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>target code</returns>
    Task<string> GenerateBySql(CodeGenerateReq codeGenerateReq);

    /// <summary>
    ///     Generate code by JSON object or array
    /// </summary>
    /// <param name="generateReq">request params</param>
    /// <returns>target code</returns>
    Task<string> GenerateByJson(CodeGenerateReq generateReq);
}
