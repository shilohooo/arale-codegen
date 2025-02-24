using Arale.CodeGen.Infrastructure.Generators;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Services;

/// <summary>
///     Code generate service impl
/// </summary>
public class CodeGenerateService : ICodeGenerateService
{
    /// <inheritdoc />
    public async Task<string> GenerateBySql(CodeGenerateReq codeGenerateReq)
    {
        return await CodeGeneratorFactory.Create(codeGenerateReq.TargetType)
            .GenerateBySql(codeGenerateReq);
    }

    /// <inheritdoc />
    public async Task<string> GenerateByJson(CodeGenerateReq generateReq)
    {
        return await CodeGeneratorFactory.Create(generateReq.TargetType)
            .GenerateByJson(generateReq);
    }
}
