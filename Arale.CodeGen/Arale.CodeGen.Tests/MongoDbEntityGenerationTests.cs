using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Infrastructure.Generators;
using Arale.CodeGen.Models.Dto;
using Arale.CodeGen.Tests.Constants;

namespace Arale.CodeGen.Tests;

/// <summary>
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
    }
}
