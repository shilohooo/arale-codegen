using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
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
            return mappedFiledType;

        // handle custom column type
        if (CSharPropertyTypeMapping.CustomColumnPropertyTypeMapping.TryGetValue(columnType.ToSql().ToLower(),
                out var fieldType))
            return fieldType;

        Console.WriteLine($"Not Custom column type or not mapped column type: {columnType} - {columnType.ToSql()}");
        return FieldType.Of("object");
    }

    /// <summary>
    ///     Get C# property type
    /// </summary>
    /// <param name="jsonNodeKv">json node key value pari</param>
    /// <returns>field type</returns>
    public static FieldType GetCSharpPropertyType(KeyValuePair<string, JsonNode?> jsonNodeKv)
    {
        if (jsonNodeKv.Value is null) return FieldType.Of("object?");

        if (jsonNodeKv.Value.GetValueKind() is JsonValueKind.Number)
        {
            if (jsonNodeKv.Value.ToString().Contains('.')) return FieldType.Of("double");

            if (int.TryParse(jsonNodeKv.Value.ToString(), out _)) return FieldType.Of("int");

            if (long.TryParse(jsonNodeKv.Value.ToString(), out _)) return FieldType.Of("long");
        }


        if (DateTimeOffset.TryParse(jsonNodeKv.Value.ToString(), CultureInfo.CurrentCulture, out _))
            return FieldType.Of(nameof(DateTime));

        Console.WriteLine($"Unprocessed json property type: {jsonNodeKv.Key}: {jsonNodeKv.Value}");
        return CSharPropertyTypeMapping.JsonPropertyTypeMapping.TryGetValue(jsonNodeKv.Value.GetValueKind(),
            out var fieldType)
            ? fieldType
            : FieldType.Of("object");
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
            return mappedFieldType;

        // handle custom column type
        if (JavaFieldTypeMapping.CustomColumnPropertyTypeMapping.TryGetValue(columnType.ToSql().ToLower(),
                out var fieldType)) return fieldType;

        Console.WriteLine($"Not Custom column type or not mapped column type: {columnType} - {columnType.ToSql()}");
        return FieldType.Of("Object");
    }

    /// <summary>
    ///     Get Java field type
    /// </summary>
    /// <param name="jsonNodeKv">json node key value pari</param>
    /// <returns>field type</returns>
    public static FieldType GetJavaFieldType(KeyValuePair<string, JsonNode?> jsonNodeKv)
    {
        if (jsonNodeKv.Value is null) return FieldType.Of("Object");

        if (jsonNodeKv.Value.GetValueKind() is JsonValueKind.Number)
        {
            if (jsonNodeKv.Value.ToString().Contains('.')) return FieldType.Of("double");

            if (int.TryParse(jsonNodeKv.Value.ToString(), out _)) return FieldType.Of("Int");

            if (long.TryParse(jsonNodeKv.Value.ToString(), out _)) return FieldType.Of("Long");
        }


        if (DateTimeOffset.TryParse(jsonNodeKv.Value.ToString(), CultureInfo.CurrentCulture, out _))
            return FieldType.Of(nameof(DateTime));

        Console.WriteLine($"Unprocessed json property type: {jsonNodeKv.Key}: {jsonNodeKv.Value}");
        return JavaFieldTypeMapping.JsonPropertyTypeMapping.TryGetValue(jsonNodeKv.Value.GetValueKind(),
            out var fieldType)
            ? fieldType
            : FieldType.Of("Object");
    }
}
