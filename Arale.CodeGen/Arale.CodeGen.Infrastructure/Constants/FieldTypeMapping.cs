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
    ///     Custom column property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<string, FieldType> CustomColumnPropertyTypeMapping = ImmutableDictionary
        .Create<string, FieldType>()
        .Add("bit", FieldType.Of("bool"))
        .Add("nchar", FieldType.Of(nameof(String).ToLower()))
        .Add("uniqueidentifier", FieldType.Of(nameof(Guid)))
        .Add("money", FieldType.Of(nameof(Decimal).ToLower()))
        .Add("smallmoney", FieldType.Of(nameof(Decimal).ToLower()))
        .Add("smalldatetime", FieldType.Of(nameof(DateTime)))
        .Add("datetime2", FieldType.Of(nameof(DateTime)));


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
    private const string JavaDateClassName = "Date";
    private const string JavaDateClassImportStatement = "import java.util.Date;";
    private const string JavaBigDecimalClassName = "BigDecimal";
    private const string JavaBigDecimalClassImportStatement = "import java.math.BigDecimal;";

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
        .Add(typeof(DataType.Date), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Time), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Numeric), FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add(typeof(DataType.Decimal), FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add(typeof(DataType.Datetime), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement));

    /// <summary>
    ///     Custom column property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<string, FieldType> CustomColumnPropertyTypeMapping = ImmutableDictionary
        .Create<string, FieldType>()
        .Add("bit", FieldType.Of("Boolean"))
        .Add("nchar", FieldType.Of(nameof(String)))
        .Add("uniqueidentifier", FieldType.Of("UUID", "import java.util.UUID;"))
        .Add("money", FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add("smallmoney", FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add("smalldatetime", FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add("datetime2", FieldType.Of(JavaDateClassName, JavaDateClassImportStatement));
}
