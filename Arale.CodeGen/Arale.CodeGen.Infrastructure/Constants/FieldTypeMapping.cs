using System.Collections.Immutable;
using System.Text.Json;
using Arale.CodeGen.Models.Entity;
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
    public static readonly ImmutableDictionary<Type, FieldType> ColumnPropertyTypeMapping = ImmutableDictionary
        .Create<Type, FieldType>()
        .Add(typeof(DataType.TinyInt), FieldType.Of("byte"))
        .Add(typeof(DataType.SmallInt), FieldType.Of("short"))
        .Add(typeof(DataType.Int), FieldType.Of("int"))
        .Add(typeof(DataType.Real), FieldType.Of("float"))
        .Add(typeof(DataType.BigInt), FieldType.Of("long"))
        .Add(typeof(DataType.Float), FieldType.Of("double"))
        .Add(typeof(DataType.Nvarchar), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Binary), FieldType.Of("byte[]"))
        .Add(typeof(DataType.Varbinary), FieldType.Of("byte[]"))
        .Add(typeof(DataType.Timestamp), FieldType.Of("byte[]"))
        .Add(typeof(DataType.Char), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Varchar), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Text), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Date), FieldType.Of(nameof(DateOnly)))
        .Add(typeof(DataType.Time), FieldType.Of(nameof(TimeOnly)))
        .Add(typeof(DataType.Numeric), FieldType.Of(nameof(Decimal).ToLower()))
        .Add(typeof(DataType.Decimal), FieldType.Of(nameof(Decimal).ToLower()))
        .Add(typeof(DataType.Datetime), FieldType.Of(nameof(DateTime)));


    /// <summary>
    ///     Json property type & csharp property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<JsonValueKind, FieldType> JsonPropertyTypeMapping = ImmutableDictionary
        .Create<JsonValueKind, FieldType>()
        .Add(JsonValueKind.String, FieldType.Of(nameof(String).ToLower()))
        .Add(JsonValueKind.True, FieldType.Of("bool"))
        .Add(JsonValueKind.False, FieldType.Of("bool"));
}

/// <summary>
///     Java field type mapping
/// </summary>
public static class JavaFieldTypeMapping
{
    /// <summary>
    ///     Table column type & java field type mapping
    /// </summary>
    public static readonly ImmutableDictionary<Type, FieldType> ColumnFieldTypeMapping = ImmutableDictionary
        .Create<Type, FieldType>()
        .Add(typeof(DataType.TinyInt), FieldType.Of("Short"))
        .Add(typeof(DataType.SmallInt), FieldType.Of("Short"))
        .Add(typeof(DataType.Int), FieldType.Of("Integer"))
        .Add(typeof(DataType.Real), FieldType.Of("Float"))
        .Add(typeof(DataType.BigInt), FieldType.Of("Long"))
        .Add(typeof(DataType.Float), FieldType.Of("Double"))
        .Add(typeof(DataType.Nvarchar), FieldType.Of("String"))
        .Add(typeof(DataType.Binary), FieldType.Of("Byte[]"))
        .Add(typeof(DataType.Varbinary), FieldType.Of("Byte[]"))
        .Add(typeof(DataType.Char), FieldType.Of("String"))
        .Add(typeof(DataType.Varchar), FieldType.Of("String"))
        .Add(typeof(DataType.Date), FieldType.Of("Date", "import java.util.Date;"))
        .Add(typeof(DataType.Time), FieldType.Of("Date", "import java.util.Date;"))
        .Add(typeof(DataType.Numeric), FieldType.Of("BigDecimal", "import java.math.BigDecimal;"))
        .Add(typeof(DataType.Decimal), FieldType.Of("BigDecimal", "import java.math.BigDecimal;"))
        .Add(typeof(DataType.Datetime), FieldType.Of("Date", "import java.util.Date;"));
}
