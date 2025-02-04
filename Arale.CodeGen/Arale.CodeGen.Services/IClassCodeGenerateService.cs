using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Services;

/// <summary>
///     Class code generate service
/// </summary>
public interface IClassCodeGenerateService
{
    /// <summary>
    ///     Generate class code by sql ddl
    /// </summary>
    /// <param name="codeGenerateBySqlReq">request params</param>
    /// <returns>class code string</returns>
    Task<string> GenerateBySql(CodeGenerateBySqlReq codeGenerateBySqlReq);
}
