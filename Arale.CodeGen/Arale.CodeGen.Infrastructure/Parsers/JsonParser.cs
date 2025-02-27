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
    /// <summary>
    ///     Root model
    /// </summary>
    public ModelInfo RootModel { get; set; }

    /// <summary>
    ///     Parse JSON object or array to model info
    /// </summary>
    /// <param name="jsonObject">json object</param>
    /// <param name="modelInfo">model info</param>
    public void Parse(JsonObject jsonObject, ModelInfo modelInfo)
    {
        foreach (var keyValuePair in jsonObject.AsEnumerable())
        {
            var fieldName = FieldHelper.GetFieldName(keyValuePair.Key, codeGenerateReq.TargetType);
            var fieldType = FieldHelper.GetFieldType(keyValuePair, codeGenerateReq.TargetType);
            modelInfo.Properties.Add(new PropertyInfo
            {
                Name = keyValuePair.Key,
                FieldName = fieldName,
                Comment = fieldName,
                Mandatory = keyValuePair.Value is not null,
                FieldType = fieldType
            });
            switch (keyValuePair.Value)
            {
                // nested object
                case JsonObject:
                {
                    var nestedModel = new ModelInfo
                    {
                        Name = keyValuePair.Key,
                        ClassName = keyValuePair.Key.Pascalize(),
                        Comment = PluralizerHelper.Singularize(keyValuePair.Key.Pascalize())
                    };
                    RootModel.NestedModels.Add(nestedModel);
                    Parse(keyValuePair.Value.AsObject(), nestedModel);
                    continue;
                }
                // nested array
                case JsonArray:
                {
                    // use first element to determine the type
                    var firstElement = keyValuePair.Value[0];
                    if (firstElement?.GetValueKind() is JsonValueKind.Object)
                    {
                        var nestedModel = new ModelInfo
                        {
                            Name = keyValuePair.Key,
                            ClassName = PluralizerHelper.Singularize(keyValuePair.Key.Pascalize()),
                            Comment = PluralizerHelper.Singularize(keyValuePair.Key.Pascalize())
                        };
                        RootModel.NestedModels.Add(nestedModel);
                        Parse(firstElement.AsObject(), nestedModel);
                    }

                    continue;
                }
            }
        }
    }
}
