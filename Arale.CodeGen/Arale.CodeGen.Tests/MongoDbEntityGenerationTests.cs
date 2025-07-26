using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Generators;
using Arale.CodeGen.Models.Dto;
using Arale.CodeGen.Tests.Constants;

namespace Arale.CodeGen.Tests;

/// <summary>
///
/// </summary>
public class MongoDbEntityGenerationTests
{
    [Fact]
    public async Task TestGenerateSpringDataMongoDbEntity()
    {
        var generator = new SpringDataMongoDbEntityCodeGenerator();
        var code = await generator.GenerateByJson(new CodeGenerateReq
        {
            Code = TestDataConstants.TestJsonStr,
            TargetType = TargetType.SpringDataMongoDbEntity
        });
        Assert.NotEmpty(code);
        Assert.Contains("import org.springframework.data.mongodb.core.mapping.Document;", code);
        Assert.Contains("import org.springframework.data.mongodb.core.mapping.Field;", code);
        Assert.Contains("@Document(collection = \"RootClass\")", code);
        Assert.Contains("@Field(\"id\")", code);
    }

    [Fact]
    public async Task TestGenerateMongoDbDriverEntity()
    {
        var generator = new CSharpMongoDbDriverEntityCodeGenerator();
        var code = await generator.GenerateByJson(new CodeGenerateReq
        {
            Code = TestDataConstants.TestJsonStr,
            TargetType = TargetType.CSharpMongoDbDriverEntity
        });
        Assert.NotEmpty(code);
        Assert.Contains("using MongoDB.Bson.Serialization.Attributes;", code);
        Assert.Contains("[BsonElement(\"Id\")]", code);
    }
}
