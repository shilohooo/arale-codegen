namespace Arale.CodeGen.Models;

/// <summary>
///     Model info
///     <remarks>
///         representation of a class / entity / table...
///     </remarks>
/// </summary>
public class ModelInfo
{
    public const string DefaultPackageName = "io.arale.codegen";
    public const string DefaultNamespace = "Arale.CodeGen";

    private const string DefaultClassName = "Root";

    /// <summary>
    ///     Package (Java) name or namespace (C#)
    /// </summary>
    public string? Namespace { get; set; }

    /// <summary>
    ///     Name
    /// </summary>
    public string? Name { get; init; }

    /// <summary>
    ///     Comment
    ///     <remarks>Maybe same as name</remarks>
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    ///     Class name
    /// </summary>
    public string? ClassName { get; init; } = DefaultClassName;

    /// <summary>
    ///     Properties
    /// </summary>
    public List<PropertyInfo> Properties { get; set; } = [];

    /// <summary>
    ///     Whether the table has a primary key defined
    /// </summary>
    public bool HasPrimaryKey => Properties.Any(x => x.IsPrimaryKey);

    /// <summary>
    ///     Whether the table has a bigint column defined
    /// </summary>
    public bool HasBigIntColumn => Properties.Any(x => x.IsBigIntType);

    /// <summary>
    ///     Import (Java) / using (C#) statements
    /// </summary>
    public HashSet<string> ImportStatements { get; set; } = [];

    /// <inheritdoc />
    public override string ToString()
    {
        return
            $"{nameof(Name)}: {Name}, {nameof(Comment)}: {Comment}, {nameof(ClassName)}: {ClassName}, {nameof(HasPrimaryKey)}: {HasPrimaryKey}, {nameof(HasBigIntColumn)}: {HasBigIntColumn}";
    }
}
