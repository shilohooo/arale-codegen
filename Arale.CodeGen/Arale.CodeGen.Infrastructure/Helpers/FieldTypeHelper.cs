using System.Text.Json;
using Arale.CodeGen.Infrastructure.Constants;
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
    /// <returns>name of c# property type</returns>
    public static string GetCSharpPropertyType(DataType columnType)
    {
        try
        {
            return CSharPropertyTypeMapping.ColumnPropertyTypeMapping[columnType.GetType()];
        }
        catch (KeyNotFoundException)
        {
            // unknown / unsupported type?
            return "object";
        }
    }

    /// <summary>
    ///     Get C# property type
    /// </summary>
    /// <param name="jsonValueKind">json property value kind</param>
    /// <returns>name of c# property type</returns>
    public static string GetCSharpPropertyType(JsonValueKind jsonValueKind)
    {
        return CSharPropertyTypeMapping.JsonPropertyTypeMapping[jsonValueKind];
    }

    /// <summary>
    ///     Get Java field type
    /// </summary>
    /// <param name="columnType">table column type</param>
    /// <returns>name of java field</returns>
    public static string GetJavaFieldType(DataType columnType)
    {
        try
        {
            return JavaFieldTypeMapping.ColumnFieldTypeMapping[columnType.GetType()];
        }
        catch (KeyNotFoundException)
        {
            // unknown / unsupported type?
            return "Object";
        }
    }
}
