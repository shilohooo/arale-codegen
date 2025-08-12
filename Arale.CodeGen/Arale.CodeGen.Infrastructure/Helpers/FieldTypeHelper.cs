using System.Globalization;
using System.Text.Json;
using System.Text.Json.Nodes;
using Arale.CodeGen.Infrastructure.Constants;
using Arale.CodeGen.Models.Entity;
using Humanizer;
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
        return FieldType.Of(CSharPropertyTypeMapping.CSharpObjectClassName);
    }

    /// <summary>
    ///     Get C# property type
    /// </summary>
    /// <param name="jsonNodeKv">json node key value pari</param>
    /// <returns>field type</returns>
    public static FieldType GetCSharpPropertyType(KeyValuePair<string, JsonNode?> jsonNodeKv)
    {
        var isNotObjectArr = false;
        while (true)
        {
            if (jsonNodeKv.Value is null) return FieldType.Of(CSharPropertyTypeMapping.CSharpObjectClassName);

            switch (jsonNodeKv.Value.GetValueKind())
            {
                case JsonValueKind.Object:
                {
                    var typeName = jsonNodeKv.Key.Pascalize();
                    return FieldType.Of(PluralizerHelper.Singularize(typeName),
                        GetCSharpObjectImportStatement(typeName));
                }

                case JsonValueKind.Array:
                {
                    var firstElement = jsonNodeKv.Value[0]!;
                    if (firstElement.GetValueKind() is JsonValueKind.Object)
                    {
                        var typeName = jsonNodeKv.Key.Pascalize();
                        return FieldType.Of($"List<{PluralizerHelper.Singularize(typeName)}>",
                            GetCSharpObjectImportStatement(typeName.Singularize()));
                    }

                    isNotObjectArr = true;
                    jsonNodeKv = KeyValuePair.Create<string, JsonNode?>(jsonNodeKv.Key, firstElement);
                    continue;
                }

                case JsonValueKind.Number:
                {
                    // handle number type
                    var typeName = jsonNodeKv.Value.ToString().Contains('.') ? "double" : "int";
                    return FieldType.Of(isNotObjectArr ? $"List<{typeName}>" : typeName);
                }

                case JsonValueKind.String:
                {
                    var typeName = isNotObjectArr ? $"List<{nameof(String)}>" : nameof(String);
                    // handle DateOnly and DateTime type
                    if (DateOnly.TryParse(jsonNodeKv.Value.ToString(), CultureInfo.CurrentCulture, out _))
                    {
                        typeName = isNotObjectArr ? $"List<{nameof(DateOnly)}>" : nameof(DateOnly);
                        return FieldType.Of(typeName);
                    }

                    if (!DateTime.TryParse(jsonNodeKv.Value.ToString(), CultureInfo.CurrentCulture, out _))
                        return FieldType.Of(typeName);

                    typeName = isNotObjectArr ? $"List<{nameof(DateTime)}>" : nameof(DateTime);
                    return FieldType.Of(typeName);
                }

                case JsonValueKind.True:
                case JsonValueKind.False:
                    return FieldType.Of(isNotObjectArr ? "List<bool>" : "bool");
                case JsonValueKind.Null:
                case JsonValueKind.Undefined:
                default:
                    return FieldType.Of(
                        isNotObjectArr
                            ? $"List<{CSharPropertyTypeMapping.CSharpObjectClassName}>"
                            : CSharPropertyTypeMapping.CSharpObjectClassName);
            }
        }
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
        return FieldType.Of(JavaFieldTypeMapping.JavaObjectClassName);
    }

    /// <summary>
    ///     Get Java field type
    /// </summary>
    /// <param name="jsonNodeKv">json node key value pari</param>
    /// <returns>field type</returns>
    public static FieldType GetJavaFieldType(KeyValuePair<string, JsonNode?> jsonNodeKv)
    {
        var isNotObjectArr = false;
        while (true)
        {
            if (jsonNodeKv.Value is null) return FieldType.Of(JavaFieldTypeMapping.JavaObjectClassName);

            switch (jsonNodeKv.Value.GetValueKind())
            {
                case JsonValueKind.Object:
                {
                    var typeName = jsonNodeKv.Key.Pascalize();
                    return FieldType.Of(PluralizerHelper.Singularize(typeName), GetJavaObjectImportStatement(typeName));
                }

                case JsonValueKind.Array:
                {
                    var firstElement = jsonNodeKv.Value[0]!;
                    if (firstElement.GetValueKind() is JsonValueKind.Object)
                    {
                        var typeName = jsonNodeKv.Key.Pascalize();
                        return FieldType.Of($"List<{PluralizerHelper.Singularize(typeName)}>",
                            GetJavaObjectImportStatement(typeName.Singularize()));
                    }

                    isNotObjectArr = true;
                    jsonNodeKv = KeyValuePair.Create<string, JsonNode?>(jsonNodeKv.Key, firstElement);
                    continue;
                }

                case JsonValueKind.Number:
                {
                    // handle number type
                    var typeName = jsonNodeKv.Value.ToString().Contains('.')
                        ? JavaFieldTypeMapping.JavaDoubleClassName
                        : JavaFieldTypeMapping.JavaIntegerClassName;
                    return isNotObjectArr
                        ? FieldType.Of($"List<{typeName}>", JavaFieldTypeMapping.JavaListClassImportStatement)
                        : FieldType.Of(typeName);
                }

                case JsonValueKind.String:
                {
                    // handle Date type
                    if (DateTime.TryParse(jsonNodeKv.Value.ToString(), CultureInfo.CurrentCulture, out _))
                        return isNotObjectArr
                            ? FieldType.Of($"List<{JavaFieldTypeMapping.JavaDateClassName}>",
                                JavaFieldTypeMapping.JavaListClassImportStatement)
                            : FieldType.Of(JavaFieldTypeMapping.JavaDateClassName,
                                JavaFieldTypeMapping.JavaDateClassImportStatement);

                    return isNotObjectArr
                        ? FieldType.Of($"List<{nameof(String)}>", JavaFieldTypeMapping.JavaListClassImportStatement)
                        : FieldType.Of(nameof(String));
                }

                case JsonValueKind.True:
                case JsonValueKind.False:
                    return isNotObjectArr
                        ? FieldType.Of($"List<{JavaFieldTypeMapping.JavaBooleanClassName}>",
                            JavaFieldTypeMapping.JavaListClassImportStatement)
                        : FieldType.Of(JavaFieldTypeMapping.JavaBooleanClassName);
                case JsonValueKind.Null:
                case JsonValueKind.Undefined:
                default:
                    return isNotObjectArr
                        ? FieldType.Of($"List<{JavaFieldTypeMapping.JavaObjectClassName}>",
                            JavaFieldTypeMapping.JavaListClassImportStatement)
                        : FieldType.Of(JavaFieldTypeMapping.JavaObjectClassName);
            }
        }
    }

    private static bool IsObjectType(string typeName)
    {
        return CSharPropertyTypeMapping.CSharpObjectClassName.Equals(typeName,
            StringComparison.InvariantCultureIgnoreCase);
    }

    private static string GetCSharpObjectImportStatement(string typeName)
    {
        return IsObjectType(typeName)
            ? string.Empty
            : $"using {CSharPropertyTypeMapping.CSharpBaseNamespace}.{typeName};";
    }

    private static string GetJavaObjectImportStatement(string typeName)
    {
        return IsObjectType(typeName) ? string.Empty : $"import {JavaFieldTypeMapping.JavaBasePackage}.{typeName};";
    }
}
