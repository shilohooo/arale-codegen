using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Generator
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    ///     Generate code by SQL DDL
    /// </summary>
    /// <param name="codeGenerateBySqlReq">request params</param>
    /// <returns>code string</returns>
    Task<string> GenerateBySql(CodeGenerateBySqlReq codeGenerateBySqlReq);
}
