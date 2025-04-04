﻿using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;
using Arale.CodeGen.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arale.CodeGen.Api.Controllers;

/// <summary>
///     Code generate Controller
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
public class CodeGenerateController(ICodeGenerateService codeGenerateService) : ControllerBase
{
    /// <summary>
    ///     Generate code by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>target code</returns>
    [HttpPost]
    public async Task<ApiResult<string>> GenerateBySql([FromBody] CodeGenerateReq codeGenerateReq)
    {
        var targetCode = await codeGenerateService.GenerateBySql(codeGenerateReq);
        return ApiResult<string>.Success(targetCode);
    }

    /// <summary>
    ///     Generate code by JSON object or array
    /// </summary>
    /// <param name="codeGenerateReq">request params</param>
    /// <returns>target code</returns>
    [HttpPost]
    public async Task<ApiResult<string>> GenerateByJson([FromBody] CodeGenerateReq codeGenerateReq)
    {
        var targetCode = await codeGenerateService.GenerateByJson(codeGenerateReq);
        return ApiResult<string>.Success(targetCode);
    }
}
