using System.Collections.Immutable;
using System.Text.Json;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Constants;

/// <summary>
///     CSharp property type mapping
/// </summary>
public static class CSharPropertyTypeMapping
{
    /// <summary>
    ///     Table column type & csharp property type mapping
    ///     <remarks>
    ///         see:
    ///         https://learn.microsoft.com/zh-cn/sql/language-extensions/how-to/c-sharp-to-sql-data-types?view=sql-server-ver16
    ///     </remarks>
    /// </summary>
    public static readonly ImmutableDictionary<Type, string> ColumnPropertyTypeMapping = ImmutableDictionary
        .Create<Type, string>()
        .Add(typeof(DataType.TinyInt), "byte")
        .Add(typeof(DataType.SmallInt), "short")
        .Add(typeof(DataType.Int), "int")
        .Add(typeof(DataType.Real), "float")
        .Add(typeof(DataType.BigInt), "long")
        .Add(typeof(DataType.Float), "double")
        .Add(typeof(DataType.Nvarchar), nameof(String).ToLower())
        .Add(typeof(DataType.Binary), "byte[]")
        .Add(typeof(DataType.Varbinary), "byte[]")
        .Add(typeof(DataType.Char), nameof(String).ToLower())
        .Add(typeof(DataType.Varchar), nameof(String).ToLower())
        .Add(typeof(DataType.Date), nameof(DateOnly))
        .Add(typeof(DataType.Time), nameof(TimeOnly))
        .Add(typeof(DataType.Numeric), nameof(Decimal).ToLower())
        .Add(typeof(DataType.Datetime), nameof(DateTime));


    /// <summary>
    ///     Json property type & csharp property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<JsonValueKind, string> JsonPropertyTypeMapping = ImmutableDictionary
        .Create<JsonValueKind, string>()
        .Add(JsonValueKind.String, nameof(String).ToLower())
        .Add(JsonValueKind.True, "bool")
        .Add(JsonValueKind.False, "bool");
}

/// <summary>
///     Java field type mapping
/// </summary>
public static class JavaFieldTypeMapping
{
    /// <summary>
    ///     Table column type & java field type mapping
    /// </summary>
    public static readonly ImmutableDictionary<Type, string> ColumnFieldTypeMapping = ImmutableDictionary
        .Create<Type, string>()
        .Add(typeof(DataType.TinyInt), "Short")
        .Add(typeof(DataType.SmallInt), "Short")
        .Add(typeof(DataType.Int), "Integer")
        .Add(typeof(DataType.Real), "Float")
        .Add(typeof(DataType.BigInt), "Long")
        .Add(typeof(DataType.Float), "Double")
        .Add(typeof(DataType.Nvarchar), "String")
        .Add(typeof(DataType.Binary), "Byte[]")
        .Add(typeof(DataType.Varbinary), "Byte[]")
        .Add(typeof(DataType.Char), "String")
        .Add(typeof(DataType.Varchar), "String")
        .Add(typeof(DataType.Date), "java.util.Date")
        .Add(typeof(DataType.Time), "java.util.Date")
        .Add(typeof(DataType.Numeric), "java.math.BigDecimal")
        .Add(typeof(DataType.Datetime), "java.util.Date");
}
