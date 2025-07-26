using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Parsers;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;
using Arale.CodeGen.Tests.Constants;

namespace Arale.CodeGen.Tests;

/// <summary>
///     Unit test of JsonParser
/// </summary>
public class JsonParserTests
{
    [Fact]
    public void TestParse()
    {
        var jsonParser = new JsonParser(new CodeGenerateReq
            { Code = TestDataConstants.TestJsonStr, TargetType = TargetType.CSharpClass })
        {
            RootModel = new ModelInfo
            {
                Name = ModelInfo.DefaultClassName,
                ClassName = ModelInfo.DefaultClassName,
                Comment = ModelInfo.DefaultClassName
            }
        };
        var jsonObj = JsonNode.Parse(TestDataConstants.TestJsonStr) switch
        {
            JsonObject jsonObject => jsonObject,
            JsonArray jsonArray => jsonArray[0]!.AsObject(),
            _ => throw new ArgumentException("Invalid json string")
        };
        jsonParser.Parse(jsonObj, jsonParser.RootModel);
        Assert.NotNull(jsonParser.RootModel);
        Assert.NotEmpty(jsonParser.RootModel.Properties);
        jsonParser.RootModel.CreateImportStatements();
        Assert.NotEmpty(jsonParser.RootModel.NestedModels);
        foreach (var nestedModel in jsonParser.RootModel.NestedModels) Assert.NotEmpty(nestedModel.Properties);
    }
}
