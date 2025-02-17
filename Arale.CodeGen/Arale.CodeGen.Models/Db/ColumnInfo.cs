using Arale.CodeGen.Models.Entity;

namespace Arale.CodeGen.Models.Db;

/// <summary>
///     Table column info
/// </summary>
public class ColumnInfo
{
    /// <summary>
    ///     Column name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Column type
    /// </summary>
    public string ColType { get; set; }

    /// <summary>
    ///     field / property name
    /// </summary>
    public string? FieldName { get; set; }

    /// <summary>
    ///     field /property type
    /// </summary>
    public FieldType? FieldType { get; set; }

    /// <summary>
    ///     Column length
    /// </summary>
    public string? Length { get; set; }

    /// <summary>
    ///     Primary key flag
    /// </summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>
    ///     Column mandatory flag for c# nullable type
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    ///     Column comment
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     column type is bigint?
    /// </summary>
    public bool IsBigIntType => "long".Equals(FieldType?.TypeName, StringComparison.CurrentCultureIgnoreCase);

    /// <inheritdoc />
    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(ColType)}: {ColType}, {nameof(FieldType)}: {FieldType}, {nameof(Length)}: {Length}, {nameof(IsPrimaryKey)}: {IsPrimaryKey}, {nameof(Mandatory)}: {Mandatory}, {nameof(Comment)}: {Comment}";
    }
}
