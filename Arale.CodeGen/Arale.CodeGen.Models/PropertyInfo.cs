using Arale.CodeGen.Models.Entity;

namespace Arale.CodeGen.Models;

/// <summary>
///     Property info
///     <remarks>
///         representation of a property / field / column...
///     </remarks>
/// </summary>
public class PropertyInfo
{
    /// <summary>
    ///     Name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    ///     Field (Java) / property (C#) name
    /// </summary>
    public string? FieldName { get; set; }

    /// <summary>
    ///     Field (Java) / property (C#) name
    /// </summary>
    public FieldType? FieldType { get; set; }

    /// <summary>
    ///     Type
    /// </summary>
    public string? ColType { get; set; }

    /// <summary>
    ///     Column length
    /// </summary>
    public string? Length { get; set; }

    /// <summary>
    ///     Primary key flag
    /// </summary>
    public bool IsPrimaryKey { get; set; }

    /// <summary>
    ///     Mandatory flag for c# nullable type
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    ///     Column comment
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Column type is bigint?
    /// </summary>
    public bool IsBigIntType => "long".Equals(FieldType?.TypeName, StringComparison.CurrentCultureIgnoreCase);

    /// <inheritdoc />
    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(ColType)}: {ColType}, {nameof(FieldType)}: {FieldType?.TypeName}, {nameof(Length)}: {Length}, {nameof(IsPrimaryKey)}: {IsPrimaryKey}, {nameof(Mandatory)}: {Mandatory}, {nameof(Comment)}: {Comment}";
    }
}
