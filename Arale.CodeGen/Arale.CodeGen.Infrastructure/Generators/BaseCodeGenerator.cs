using System.Text;
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
        var jsonParser = new JsonParser(codeGenerateReq);

        var jsonNode = JsonNode.Parse(codeGenerateReq.Code);
        jsonParser.Parse(jsonNode!.GetValueKind() is JsonValueKind.Array
            ? jsonNode.AsArray()[0]!.AsObject()
            : jsonNode.AsObject()
        );

        jsonParser.Models[0].Namespace = codeGenerateReq.TargetType switch
        {
            TargetType.CSharpClass or TargetType.SqlSugarEntity or TargetType.EFCoreEntity =>
                ModelInfo.DefaultNamespace,
            TargetType.JavaClass or TargetType.MyBatisPlusEntity or TargetType.HibernateEntity => ModelInfo
                .DefaultPackageName,
            _ => null
        };

        // FIXME - Java source file can't have more than one public class,
        // thus we need to generate inner static class for non-root json object / array
        var stringBuilder = new StringBuilder();
        foreach (var tableInfo in jsonParser.Models)
        {
            tableInfo.ImportStatements = tableInfo.Properties
                .Where(c => c.FieldType is not null && !string.IsNullOrWhiteSpace(c.FieldType?.ImportStatement))
                .Select(c => c.FieldType!.ImportStatement!)
                .ToHashSet();

            stringBuilder.Append(await TemplateHelper.RenderAsync(GetTemplateName(), tableInfo)).AppendLine();
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    ///     Get template name for generate
    /// </summary>
    /// <returns>template name</returns>
    protected abstract TemplateName GetTemplateName();
}
