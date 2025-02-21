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
        if (columnType is not DataType.Custom &&
            CSharPropertyTypeMapping.ColumnPropertyTypeMapping.TryGetValue(columnType.GetType(),
                out var mappedFiledType))
        {
            return mappedFiledType;
        }

        // handle custom column type
        if (CSharPropertyTypeMapping.CustomColumnPropertyTypeMapping.TryGetValue(columnType.ToSql().ToLower(),
                out var fieldType))
        {
            return fieldType;
        }

        Console.WriteLine($"Not Custom column type or not mapped column type: {columnType} - {columnType.ToSql()}");
        return FieldType.Of("object");
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
        if (columnType is not DataType.Custom &&
            JavaFieldTypeMapping.ColumnFieldTypeMapping.TryGetValue(columnType.GetType(), out var mappedFieldType))
        {
            return mappedFieldType;
        }

        // handle custom column type
        if (JavaFieldTypeMapping.CustomColumnPropertyTypeMapping.TryGetValue(columnType.ToSql().ToLower(), out var fieldType))
        {
            return fieldType;
        }

        Console.WriteLine($"Not Custom column type or not mapped column type: {columnType} - {columnType.ToSql()}");
        return FieldType.Of("Object");
    }
}
