using Arale.CodeGen.Infrastructure.Generators;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Services;

/// <summary>
///     Class code generate service impl
/// </summary>
public class ClassCodeGenerateService : IClassCodeGenerateService
{
    /// <inheritdoc />
    public async Task<string> GenerateBySql(CodeGenerateBySqlReq codeGenerateBySqlReq)
    {
        return await CodeGeneratorFactory.Create(codeGenerateBySqlReq.TargetType)
            .GenerateBySql(codeGenerateBySqlReq);
    }
}
