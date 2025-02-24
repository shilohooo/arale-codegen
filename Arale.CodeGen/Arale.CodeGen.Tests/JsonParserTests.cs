using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Parsers;
using Arale.CodeGen.Models.Dto;
using Xunit.Abstractions;

namespace Arale.CodeGen.Tests;

/// <summary>
///     Unit test of JsonParser
/// </summary>
public class JsonParserTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public JsonParserTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

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
        var jsonParser = new JsonParser(new CodeGenerateReq { Code = jsonStr, TargetType = TargetType.CSharpClass });
        var jsonObj = JsonNode.Parse(jsonStr) switch
        {
            JsonObject jsonObject => jsonObject,
            JsonArray jsonArray => jsonArray[0]!.AsObject(),
            _ => throw new ArgumentException("Invalid json string")
        };
        jsonParser.Parse(jsonObj);
        Assert.NotEmpty(jsonParser.Models);
        foreach (var tableInfo in jsonParser.Models)
        {
            Assert.NotEmpty(tableInfo.Properties);

            _testOutputHelper.WriteLine(tableInfo.ToString());

            _testOutputHelper.WriteLine("====================== Columns start ======================");
            foreach (var columnInfo in tableInfo.Properties) _testOutputHelper.WriteLine(columnInfo.ToString());
            _testOutputHelper.WriteLine("====================== Columns end ======================");

            if (tableInfo.ImportStatements.Count <= 0) continue;

            _testOutputHelper.WriteLine("====================== ImportStatements start ======================");
            foreach (var importStatement in tableInfo.ImportStatements)
                _testOutputHelper.WriteLine(importStatement);
            _testOutputHelper.WriteLine("====================== ImportStatements end ======================");
        }
    }
}
