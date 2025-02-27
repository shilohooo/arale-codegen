using System.Text.Json;
using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Helpers;
using Arale.CodeGen.Infrastructure.Parsers;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Base Code Generator
/// </summary>
public abstract class BaseCodeGenerator : ICodeGenerator
{
    /// <inheritdoc />
    public async Task<string> GenerateBySql(CodeGenerateReq codeGenerateReq)
    {
        var tableInfo = SqlParseHelper.Parse(codeGenerateReq);
        return await TemplateHelper.RenderAsync(GetTemplateName(), tableInfo);
    }

    /// <inheritdoc />
    public async Task<string> GenerateByJson(CodeGenerateReq codeGenerateReq)
    {
        var jsonNode = JsonNode.Parse(codeGenerateReq.Code);
        var jsonParser = new JsonParser(codeGenerateReq)
        {
            RootModel = new ModelInfo
            {
                Name = ModelInfo.DefaultClassName,
                ClassName = ModelInfo.DefaultClassName,
                Comment = ModelInfo.DefaultClassName
            }
        };
        jsonParser.Parse(
            jsonNode!.GetValueKind() is JsonValueKind.Array ? jsonNode.AsArray()[0]!.AsObject() : jsonNode.AsObject(),
            jsonParser.RootModel
        );
        jsonParser.RootModel.CreateImportStatements();
        return await TemplateHelper.RenderAsync(GetTemplateName(), jsonParser.RootModel);
    }

    /// <summary>
    ///     Get template name for generate
    /// </summary>
    /// <returns>template name</returns>
    protected abstract TemplateName GetTemplateName();
}
