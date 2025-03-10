﻿using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Generator
/// </summary>
public interface ICodeGenerator
{
    /// <summary>
    ///     Generate code by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>code string</returns>
    Task<string> GenerateBySql(CodeGenerateReq codeGenerateReq);

    /// <summary>
    ///     Generate code by JSON object or array
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>code string</returns>
    Task<string> GenerateByJson(CodeGenerateReq codeGenerateReq);
}
