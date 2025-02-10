using System.ComponentModel;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Extensions;
using Arale.CodeGen.Infrastructure.Helpers;
using Arale.CodeGen.Models.Dto;
using Xunit.Abstractions;
using DbType = Arale.CodeGen.Commons.Constants.DbType;

namespace Arale.CodeGen.Tests;

/// <summary>
///     TemplateHelper unit tests
/// </summary>
public class TemplateHelperTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TemplateHelperTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    /// <summary>
    ///     Test getting Enum description by Enum extension method
    /// </summary>
    [Fact]
    public async void TestGetEnumDescription()
    {
        Assert.Equal("星期一", Week.Monday.GetDescription());
        const string sql = """
                           CREATE TABLE T_SqlServerGenerateTest (
                               Id bigint primary key not null,
                             BitCol bit not null,
                             TinyIntCol tinyint not null,
                             SmallIntCol smallint not null,
                             IntCol int not null,
                             RealCol real not null,
                             BigIntCol bigint not null,
                             FloatCol float not null,
                             NCharCol nchar(10) not null,
                             NVarCharCol nvarchar (10) not null,
                             BinaryCol binary(10) not null,
                             VarBinaryCol varbinary(10) not null,
                             UniqueIdentifierCol uniqueidentifier not null,
                             CharCol char(10) not null,
                             VarCharCol varchar(10) not null,
                             VarCharMaxCol varchar(max) not null,
                             DateCol date not null,
                             TimeCol time not null,
                             NumericCol numeric(10, 2) not null,
                             DecimalCol decimal(10, 2) not null,
                             MoneyCol money,
                             SmallMoneyCol smallmoney,
                             SmallDatetimeCol smalldatetime,
                             DatetimeCol datetime,
                             Datetime2Col datetime2
                           );
                           """;
        var tableInfo = ClassHelper.ToClassBySql(new CodeGenerateBySqlReq
        {
            Code = sql, DbType = DbType.SqlServer, TargetType = TargetType.MyBatisPlusEntity, TableNamePrefix = "T_"
        });
        var entityCode = await TemplateHelper.RenderAsync(TemplateName.MyBatisPlusEntity, tableInfo);
        _testOutputHelper.WriteLine(entityCode.Replace("\\\n", "\n"));
    }
}

/// <summary>
///     Enum of weeks
/// </summary>
public enum Week
{
    [Description("星期一")] Monday
}
