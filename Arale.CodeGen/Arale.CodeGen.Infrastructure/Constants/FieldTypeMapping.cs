using System.Collections.Immutable;
using System.Net;
using System.Net.NetworkInformation;
using Arale.CodeGen.Models.Entity;
using NpgsqlTypes;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Constants;

/// <summary>
///     CSharp property type mapping
/// </summary>
public static class CSharPropertyTypeMapping
{
    public const string CSharpBaseNamespace = "Arale.CodeGen";
    public const string CSharpObjectClassName = "object";

    private const string CSharpBooleanPrimitiveName = "bool";
    private const string CSharpShortPrimitiveName = "short";
    private const string CSharpIntPrimitiveName = "int";
    private const string CSharpDoublePrimitiveName = "double";
    private const string CSharpByteArrayPrimitiveName = "byte[]";
    private const string NpgsqlTypesNamespace = "using NpgsqlTypes;";

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
        .Add(typeof(DataType.SmallInt), FieldType.Of(CSharpShortPrimitiveName))
        .Add(typeof(DataType.Int), FieldType.Of(CSharpIntPrimitiveName))
        .Add(typeof(DataType.MediumInt), FieldType.Of(CSharpIntPrimitiveName))
        .Add(typeof(DataType.Real), FieldType.Of(CSharpDoublePrimitiveName))
        .Add(typeof(DataType.BigInt), FieldType.Of("long"))
        .Add(typeof(DataType.Float), FieldType.Of(CSharpDoublePrimitiveName))
        .Add(typeof(DataType.Double), FieldType.Of(CSharpDoublePrimitiveName))
        .Add(typeof(DataType.Nvarchar), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Binary), FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add(typeof(DataType.Varbinary), FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add(typeof(DataType.Blob), FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add(typeof(DataType.Timestamp), FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add(typeof(DataType.Char), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Varchar), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Text), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Json), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.JsonB), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Enum), FieldType.Of(nameof(String).ToLower()))
        .Add(typeof(DataType.Date), FieldType.Of(nameof(DateOnly)))
        .Add(typeof(DataType.Time), FieldType.Of(nameof(TimeOnly)))
        .Add(typeof(DataType.Numeric), FieldType.Of(nameof(Decimal).ToLower()))
        .Add(typeof(DataType.Decimal), FieldType.Of(nameof(Decimal).ToLower()))
        .Add(typeof(DataType.Uuid), FieldType.Of(nameof(Guid)))
        .Add(typeof(DataType.Set), FieldType.Of($"HashSet<{nameof(String).ToLower()}>"))
        .Add(typeof(DataType.Datetime), FieldType.Of(nameof(DateTime)))
        .Add(typeof(DataType.Interval), FieldType.Of(nameof(TimeSpan)));


    /// <summary>
    ///     Custom column property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<string, FieldType> CustomColumnPropertyTypeMapping = ImmutableDictionary
        .Create<string, FieldType>()
        .Add("bit", FieldType.Of(CSharpBooleanPrimitiveName))
        .Add("bit(1)", FieldType.Of(CSharpBooleanPrimitiveName))
        .Add("bit(8)", FieldType.Of("byte"))

        #region Custom column type of MySQL

        .Add("year", FieldType.Of(CSharpShortPrimitiveName))
        .Add("mediumblob", FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add("longblob", FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add("tinytext", FieldType.Of(nameof(String).ToLower()))
        .Add("mediumtext", FieldType.Of(nameof(String).ToLower()))
        .Add("longtext", FieldType.Of(nameof(String).ToLower()))

        #endregion

        #region Custom column type of SQL Server

        .Add("nchar", FieldType.Of(nameof(String).ToLower()))
        .Add("uniqueidentifier", FieldType.Of(nameof(Guid)))
        .Add("money", FieldType.Of(nameof(Decimal).ToLower()))
        .Add("smallmoney", FieldType.Of(nameof(Decimal).ToLower()))
        .Add("smalldatetime", FieldType.Of(nameof(DateTime)))
        .Add("datetime2", FieldType.Of(nameof(DateTime)))

        #endregion

        #region Custom column type of PostgreSql

        .Add("boolean", FieldType.Of(CSharpBooleanPrimitiveName))
        .Add("smallserial", FieldType.Of(CSharpShortPrimitiveName))
        .Add("serial", FieldType.Of(CSharpIntPrimitiveName))
        .Add("integer", FieldType.Of(CSharpIntPrimitiveName))
        .Add("double precision", FieldType.Of(CSharpDoublePrimitiveName))
        .Add("bytea", FieldType.Of(CSharpByteArrayPrimitiveName))
        .Add("inet", FieldType.Of(nameof(IPAddress), "using System.Net;"))
        .Add("macaddr", FieldType.Of(nameof(PhysicalAddress), "using System.Net.NetworkInformation;"))
        .Add("cidr", FieldType.Of(nameof(NpgsqlInet), NpgsqlTypesNamespace))
        .Add("point", FieldType.Of(nameof(NpgsqlPoint), NpgsqlTypesNamespace))
        .Add("line", FieldType.Of(nameof(NpgsqlLine), NpgsqlTypesNamespace))
        .Add("lseg", FieldType.Of(nameof(NpgsqlLSeg), NpgsqlTypesNamespace))
        .Add("box", FieldType.Of(nameof(NpgsqlBox), NpgsqlTypesNamespace))
        .Add("path", FieldType.Of(nameof(NpgsqlPath), NpgsqlTypesNamespace))
        .Add("polygon", FieldType.Of(nameof(NpgsqlPolygon), NpgsqlTypesNamespace))
        .Add("circle", FieldType.Of(nameof(NpgsqlCircle), NpgsqlTypesNamespace))
        .Add("tsvector", FieldType.Of(nameof(NpgsqlTsVector), NpgsqlTypesNamespace))
        .Add("tsquery", FieldType.Of(nameof(NpgsqlTsQuery), NpgsqlTypesNamespace))
        .Add("integer[]", FieldType.Of($"{CSharpIntPrimitiveName}[]"))
        .Add("text[]", FieldType.Of($"{nameof(String).ToLower()}[]"));

    #endregion
}

/// <summary>
///     Java field type mapping
/// </summary>
public static class JavaFieldTypeMapping
{
    public const string JavaBasePackage = "io.arale.codegen";

    public const string JavaObjectClassName = "Object";
    public const string JavaBooleanClassName = "Boolean";
    public const string JavaIntegerClassName = "Integer";
    public const string JavaDoubleClassName = "Double";
    public const string JavaDateClassName = "Date";
    public const string JavaDateClassImportStatement = "import java.util.Date;";
    public const string JavaListClassImportStatement = "import java.util.List;";

    private const string JavaByteArrayClassName = "Byte[]";
    private const string JavaShortClassName = "Short";
    private const string JavaBigDecimalClassName = "BigDecimal";
    private const string JavaBigDecimalClassImportStatement = "import java.math.BigDecimal;";

    /// <summary>
    ///     Table column type & java field type mapping
    /// </summary>
    public static readonly ImmutableDictionary<Type, FieldType> ColumnFieldTypeMapping = ImmutableDictionary
        .Create<Type, FieldType>()
        .Add(typeof(DataType.TinyInt), FieldType.Of(JavaShortClassName))
        .Add(typeof(DataType.SmallInt), FieldType.Of(JavaShortClassName))
        .Add(typeof(DataType.Int), FieldType.Of(JavaIntegerClassName))
        .Add(typeof(DataType.MediumInt), FieldType.Of(JavaIntegerClassName))
        .Add(typeof(DataType.Real), FieldType.Of(JavaDoubleClassName))
        .Add(typeof(DataType.BigInt), FieldType.Of("Long"))
        .Add(typeof(DataType.Float), FieldType.Of(JavaDoubleClassName))
        .Add(typeof(DataType.Double), FieldType.Of(JavaDoubleClassName))
        .Add(typeof(DataType.Nvarchar), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.Binary), FieldType.Of(JavaByteArrayClassName))
        .Add(typeof(DataType.Varbinary), FieldType.Of(JavaByteArrayClassName))
        .Add(typeof(DataType.Blob), FieldType.Of(JavaByteArrayClassName))
        .Add(typeof(DataType.Char), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.Varchar), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.Json), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.JsonB), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.Text), FieldType.Of(nameof(String)))
        .Add(typeof(DataType.Date), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Time), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Timestamp), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Numeric), FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add(typeof(DataType.Decimal), FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add(typeof(DataType.Uuid), FieldType.Of("UUID", "import java.util.UUID;"))
        .Add(typeof(DataType.Set), FieldType.Of("Set<String>", "import java.util.Set;"))
        .Add(typeof(DataType.Datetime), FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add(typeof(DataType.Interval), FieldType.Of("PGInterval", "import org.postgresql.util.PGInterval;"));

    /// <summary>
    ///     Custom column property type mapping
    /// </summary>
    public static readonly ImmutableDictionary<string, FieldType> CustomColumnPropertyTypeMapping = ImmutableDictionary
        .Create<string, FieldType>()
        .Add("bit", FieldType.Of(JavaBooleanClassName))
        .Add("bit(1)", FieldType.Of(JavaBooleanClassName))
        .Add("bit(8)", FieldType.Of("Byte"))

        #region Custom column type of MySQL

        .Add("year", FieldType.Of(JavaShortClassName))
        .Add("mediumblob", FieldType.Of(JavaByteArrayClassName))
        .Add("longblob", FieldType.Of(JavaByteArrayClassName))
        .Add("tinytext", FieldType.Of(nameof(String)))
        .Add("mediumtext", FieldType.Of(nameof(String)))
        .Add("longtext", FieldType.Of(nameof(String)))

        #endregion

        #region Custom column type of SQL Server

        .Add("nchar", FieldType.Of(nameof(String)))
        .Add("uniqueidentifier", FieldType.Of("UUID", "import java.util.UUID;"))
        .Add("money", FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add("smallmoney", FieldType.Of(JavaBigDecimalClassName, JavaBigDecimalClassImportStatement))
        .Add("smalldatetime", FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))
        .Add("datetime2", FieldType.Of(JavaDateClassName, JavaDateClassImportStatement))

        #endregion

        #region Custom column type of PostgreSql

        .Add("boolean", FieldType.Of("Boolean"))
        .Add("smallserial", FieldType.Of(JavaShortClassName))
        .Add("serial", FieldType.Of(JavaIntegerClassName))
        .Add("integer", FieldType.Of(JavaIntegerClassName))
        .Add("double precision", FieldType.Of(JavaDoubleClassName))
        .Add("bytea", FieldType.Of(JavaByteArrayClassName))
        .Add("inet", FieldType.Of("InetAddress", "import java.net.InetAddress;"))
        .Add("macaddr", FieldType.Of(nameof(String)))
        .Add("cidr", FieldType.Of("InetAddress", "import java.net.InetAddress;"))
        .Add("point", FieldType.Of("PGpoint", "import org.postgresql.geometric.PGpoint;"))
        .Add("line", FieldType.Of("PGline", "import org.postgresql.geometric.PGline;"))
        .Add("lseg", FieldType.Of("PGlseg", "import org.postgresql.geometric.PGlseg;"))
        .Add("box", FieldType.Of("PGbox", "import org.postgresql.geometric.PGbox;"))
        .Add("path", FieldType.Of("PGpath", "import org.postgresql.geometric.PGpath;"))
        .Add("polygon", FieldType.Of("PGpolygon", "import org.postgresql.geometric.PGpolygon;"))
        .Add("circle", FieldType.Of("PGcircle", "import org.postgresql.geometric.PGcircle;"))
        .Add("tsvector", FieldType.Of(nameof(String)))
        .Add("tsquery", FieldType.Of(nameof(String)))
        .Add("integer[]", FieldType.Of($"{JavaIntegerClassName}[]"))
        .Add("text[]", FieldType.Of($"{nameof(String)}[]"));

    #endregion
}
