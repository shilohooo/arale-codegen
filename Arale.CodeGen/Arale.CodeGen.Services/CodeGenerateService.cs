using Arale.CodeGen.Infrastructure.Generators;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Services;

/// <summary>
///     Code generate service impl
/// </summary>
public class CodeGenerateService(CodeGeneratorFactory codeGeneratorFactory) : ICodeGenerateService
{
    /// <inheritdoc />
    public async Task<List<CodeGenerateResp>> GenerateBySql(CodeGenerateReq codeGenerateReq)
    {
        return await codeGeneratorFactory.Create(codeGenerateReq.TargetType)
            .GenerateBySql(codeGenerateReq);
    }

    /// <inheritdoc />
    public async Task<List<CodeGenerateResp>> GenerateByJson(CodeGenerateReq generateReq)
    {
        return await codeGeneratorFactory.Create(generateReq.TargetType)
            .GenerateByJson(generateReq);
    }
}
