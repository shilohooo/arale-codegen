using System.Text.Json;
using Arale.CodeGen.Infrastructure.Constants;
using Arale.CodeGen.Models.Entity;
using SqlParser;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility methods for field / property type
/// </summary>
public static class FieldTypeHelper
{
    /// <summary>
    ///     Get C# property type
    /// </summary>
    /// <param name="columnType">table column type</param>
    /// <returns>field type</returns>
    public static FieldType GetCSharpPropertyType(DataType columnType)
    {
        try
        {
            return CSharPropertyTypeMapping.ColumnPropertyTypeMapping[columnType.GetType()];
        }
        catch (KeyNotFoundException)
        {
            // unknown / unsupported type?
            Console.WriteLine($"Unknown / unsupported type: {columnType} - {columnType.ToSql()}");
            return FieldType.Of("object");
        }
    }

    /// <summary>
    ///     Get C# property type
    /// </summary>
    /// <param name="jsonValueKind">json property value kind</param>
    /// <returns>field type</returns>
    public static FieldType GetCSharpPropertyType(JsonValueKind jsonValueKind)
    {
        return CSharPropertyTypeMapping.JsonPropertyTypeMapping[jsonValueKind];
    }

    /// <summary>
    ///     Get Java field type
    /// </summary>
    /// <param name="columnType">table column type</param>
    /// <returns>field type</returns>
    public static FieldType GetJavaFieldType(DataType columnType)
    {
        try
        {
            return JavaFieldTypeMapping.ColumnFieldTypeMapping[columnType.GetType()];
        }
        catch (KeyNotFoundException)
        {
            // unknown / unsupported type?
            return FieldType.Of("Object");
        }
    }
}
