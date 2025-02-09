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
            if (columnType is not DataType.Custom)
                return CSharPropertyTypeMapping.ColumnPropertyTypeMapping[columnType.GetType()];

            // handle custom column type
            var fieldType = CSharPropertyTypeMapping.CustomColumnPropertyTypeMapping.Where(a =>
                    columnType.ToSql().Contains(a.Key, StringComparison.CurrentCultureIgnoreCase))
                .Select(a => a.Value)
                .FirstOrDefault();
            if (fieldType is not null) return fieldType;

            Console.WriteLine($"Custom column type: {columnType} - {columnType.ToSql()}");
            return FieldType.Of("object");
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
            if (columnType is not DataType.Custom)
                return JavaFieldTypeMapping.ColumnFieldTypeMapping[columnType.GetType()];

            // handle custom column type
            var fieldType = JavaFieldTypeMapping.CustomColumnPropertyTypeMapping.Where(a =>
                    columnType.ToSql().Contains(a.Key, StringComparison.CurrentCultureIgnoreCase))
                .Select(a => a.Value)
                .FirstOrDefault();
            if (fieldType is not null) return fieldType;

            Console.WriteLine($"Custom column type: {columnType} - {columnType.ToSql()}");
            return FieldType.Of("Object");
        }
        catch (KeyNotFoundException)
        {
            // unknown / unsupported type?
            Console.WriteLine($"Unknown / unsupported type: {columnType} - {columnType.ToSql()}");
            return FieldType.Of("Object");
        }
    }
}
