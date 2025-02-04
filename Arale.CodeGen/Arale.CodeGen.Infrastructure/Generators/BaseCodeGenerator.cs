using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Helpers;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Base Code Generator
/// </summary>
public abstract class BaseCodeGenerator : ICodeGenerator
{
    /// <inheritdoc />
    public async Task<string> GenerateBySql(CodeGenerateBySqlReq codeGenerateBySqlReq)
    {
        var tableInfo = ClassHelper.ToClassBySql(codeGenerateBySqlReq);
        return await TemplateHelper.RenderAsync(GetTemplateName(), tableInfo);
    }

    /// <summary>
    ///     Get template name for generate
    /// </summary>
    /// <returns>template name</returns>
    protected abstract TemplateName GetTemplateName();
}
