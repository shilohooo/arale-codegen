using System.Text.Json;
using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Extensions;
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
    public abstract TargetType SupportedTargetType { get; }

    /// <inheritdoc />
    public async Task<List<CodeGenerateResp>> GenerateBySql(CodeGenerateReq codeGenerateReq)
    {
        var tableInfo = SqlParseHelper.Parse(codeGenerateReq);
        var generatedCode = await TemplateHelper.RenderAsync(GetTemplateName(), tableInfo);
        var languageType = LanguageTypeHelper.GetByTargetType(codeGenerateReq.TargetType);
        return
        [
            new CodeGenerateResp
            {
                TargetType = codeGenerateReq.TargetType,
                FileName = $"{tableInfo.ClassName}.{languageType.GetDescription()}",
                Code = generatedCode,
                Language = languageType
            }
        ];
    }

    /// <inheritdoc />
    public async Task<List<CodeGenerateResp>> GenerateByJson(CodeGenerateReq codeGenerateReq)
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

        var languageType = LanguageTypeHelper.GetByTargetType(codeGenerateReq.TargetType);
        List<CodeGenerateResp> results =
        [
            new()
            {
                TargetType = codeGenerateReq.TargetType,
                FileName = $"{jsonParser.RootModel.ClassName}.{languageType.GetDescription()}",
                Code = await TemplateHelper.RenderAsync(GetTemplateName(), jsonParser.RootModel),
                Language = languageType
            }
        ];
        foreach (var model in jsonParser.RootModel.NestedModels)
            results.Add(new CodeGenerateResp
            {
                TargetType = codeGenerateReq.TargetType,
                FileName = $"{model.ClassName}.{languageType.GetDescription()}",
                Code = await TemplateHelper.RenderAsync(GetTemplateName(), model),
                Language = languageType
            });
        return results;
    }

    /// <summary>
    ///     Get template name for generate
    /// </summary>
    /// <returns>template name</returns>
    protected abstract TemplateName GetTemplateName();
}
