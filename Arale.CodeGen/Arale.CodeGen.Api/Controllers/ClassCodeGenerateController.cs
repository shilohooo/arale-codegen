﻿using System.Net.Mime;
using System.Text;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;
using Arale.CodeGen.Services;
using Microsoft.AspNetCore.Mvc;

namespace Arale.CodeGen.Api.Controllers;

/// <summary>
///     Class code generate Controller
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
public class ClassCodeGenerateController(IClassCodeGenerateService classCodeGenerateService) : ControllerBase
{
    /// <summary>
    ///     Generate class code
    /// </summary>
    /// <param name="codeGenerateBySqlReq">request params</param>
    /// <returns>generated code</returns>
    [HttpPost]
    public async Task<ApiResult<string>> GenerateBySql([FromBody] CodeGenerateBySqlReq codeGenerateBySqlReq)
    {
        var classCode = await classCodeGenerateService.GenerateBySql(codeGenerateBySqlReq);
        return ApiResult<string>.Success(classCode);
    }
}
