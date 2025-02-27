using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Parsers;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;

namespace Arale.CodeGen.Tests;

/// <summary>
///     Unit test of JsonParser
/// </summary>
public class JsonParserTests
{
    [Fact]
    public void TestParse()
    {
        const string jsonStr = """
                               [
                                 {
                                   "id": 1,
                                   "username": "shiloh",
                                   "gender": 1,
                                   "birthday": "1998-03-02",
                                   "email": null,
                                   "pi": 3.1415926,
                                   "address": {
                                     "country": "China",
                                     "province": "Guangdong",
                                     "city": "FoShan",
                                     "street": "桂城街道"
                                   },
                                   "doubleNumArr": [
                                     1.52,
                                     2.53,
                                     3.53,
                                     4.54,
                                     5.55
                                   ],
                                   "intArr": [
                                     1,
                                     2,
                                     3,
                                     4,
                                     5
                                   ],
                                   "strArr": [
                                     "a",
                                     "b",
                                     "c",
                                     "d",
                                     "e"
                                   ],
                                   "roles": [
                                     {
                                       "id": 1,
                                       "roleName": "admin",
                                       "permissions": [
                                         {
                                           "id": 1,
                                           "code": "sys:user:view",
                                           "description": "查看用户列表",
                                           "url": "/api/users"
                                         }
                                       ]
                                     },
                                     {
                                       "id": 2,
                                       "roleName": "user"
                                     }
                                   ],
                                   "enabled": true
                                 },
                                 {
                                   "id": 2,
                                   "username": "bruce",
                                   "gender": 1,
                                   "birthday": "1998-03-02",
                                   "email": "bruce@gmail.com",
                                   "pi": 3.1415926,
                                   "address": {
                                     "country": "China",
                                     "province": "Guangdong",
                                     "city": "FoShan",
                                     "street": "桂城街道"
                                   },
                                   "enabled": false
                                 }
                               ]
                               """;
        var jsonParser = new JsonParser(new CodeGenerateReq { Code = jsonStr, TargetType = TargetType.CSharpClass })
        {
            RootModel = new ModelInfo
            {
                Name = ModelInfo.DefaultClassName,
                ClassName = ModelInfo.DefaultClassName,
                Comment = ModelInfo.DefaultClassName
            }
        };
        var jsonObj = JsonNode.Parse(jsonStr) switch
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
