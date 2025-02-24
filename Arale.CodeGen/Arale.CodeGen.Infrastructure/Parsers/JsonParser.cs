using System.Text.Json;
using System.Text.Json.Nodes;
using Arale.CodeGen.Infrastructure.Helpers;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;
using Humanizer;

namespace Arale.CodeGen.Infrastructure.Parsers;

/// <summary>
///     JSON parser
///     <param name="codeGenerateReq">code generate params</param>
/// </summary>
public class JsonParser(CodeGenerateReq codeGenerateReq)
{
    private const string DefaultRootObjectName = "RootObject";

    /// <summary>
    ///     table info list
    /// </summary>
    public List<ModelInfo> Models { get; set; } = [];

    /// <summary>
    ///     Parse JSON object or array to table info
    /// </summary>
    /// <param name="jsonObject">json object</param>
    /// <param name="className">class name</param>
    public void Parse(JsonObject jsonObject, string className = DefaultRootObjectName)
    {
        var model = new ModelInfo { ClassName = PluralizerHelper.Singularize(className), Comment = className };
        Models.Add(model);
        foreach (var keyValuePair in jsonObject.AsEnumerable())
        {
            var fieldName = FieldHelper.GetFieldName(keyValuePair.Key, codeGenerateReq.TargetType);
            var fieldType = FieldHelper.GetFieldType(keyValuePair, codeGenerateReq.TargetType);
            model.Properties.Add(new PropertyInfo
            {
                FieldName = fieldName,
                Comment = fieldName,
                Mandatory = keyValuePair.Value is not null,
                FieldType = fieldType
            });
            switch (keyValuePair.Value)
            {
                // nested object
                case JsonObject:
                    Parse(keyValuePair.Value.AsObject(), keyValuePair.Key.Pascalize());
                    continue;
                // nested array
                case JsonArray:
                {
                    // use first element to determine the type
                    var firstElement = keyValuePair.Value[0];
                    if (firstElement?.GetValueKind() is JsonValueKind.Object)
                        Parse(firstElement.AsObject(), keyValuePair.Key.Pascalize());

                    continue;
                }
            }
        }
    }
}
