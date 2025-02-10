namespace Arale.CodeGen.Models.Db;

/// <summary>
///     Table info
/// </summary>
public class TableInfo
{
    private const string DefaultClassName = "Root";

    /// <summary>
    ///     table name
    /// </summary>
    public string? TableName { get; init; }

    /// <summary>
    ///     table comment
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     class name
    /// </summary>
    public string? ClassName { get; init; } = DefaultClassName;

    /// <summary>
    ///     columns
    /// </summary>
    public List<ColumnInfo> Columns { get; set; } = [];

    /// <summary>
    ///     Whether the table has a primary key defined
    /// </summary>
    public bool HasPrimaryKey => Columns.Any(x => x.IsPrimaryKey);

    /// <summary>
    ///     Whether the table has a bigint column defined
    /// </summary>
    public bool HasBigIntColumn => Columns.Any(x => x.IsBigIntType);

    /// <summary>
    ///     import / using statements
    /// </summary>
    public HashSet<string> ImportStatements { get; set; } = [];

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{nameof(TableName)}: {TableName}, {nameof(Comment)}: {Comment}, {nameof(ClassName)}: {ClassName}";
    }
}
