namespace Arale.CodeGen.Models.Entity;

/// <summary>
///     Field / property type
/// </summary>
public class FieldType
{
    /// <summary>
    ///     field /property type
    /// </summary>
    public string? TypeName { get; set; }

    /// <summary>
    ///     import / using statement, can be null
    /// </summary>
    public string? ImportStatement { get; set; }

    /// <summary>
    ///     Create a field type
    /// </summary>
    /// <param name="typeName">type name</param>
    /// <param name="importStatement">import statement</param>
    /// <returns>field type instance</returns>
    public static FieldType Of(string typeName, string importStatement = "")
    {
        return new FieldType { TypeName = typeName, ImportStatement = importStatement };
    }
}
