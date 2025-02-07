using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Commons.Exceptions;
using Arale.CodeGen.Models.Entity;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility methods for field / property
/// </summary>
public static class FieldHelper
{
    /// <summary>
    ///     Get field / property name
    /// </summary>
    /// <param name="columnName">table column name</param>
    /// <param name="targetType">generate target type</param>
    /// <returns>field / property name</returns>
    public static string GetFieldName(string columnName, TargetType targetType)
    {
        var firstLetterOfName = columnName[0];
        var firstLetterIsUpperCase = char.IsUpper(firstLetterOfName);
        return targetType switch
        {
            // c# property naming convention is PascalCase
            TargetType.CSharpClass => firstLetterIsUpperCase
                ? columnName
                : (char)(firstLetterOfName - 32) + columnName[1..],
            // java field naming convention is camelCase
            TargetType.JavaClass => firstLetterIsUpperCase
                ? (char)(firstLetterOfName + 32) + columnName[1..]
                : columnName,
            _ => columnName
        };
    }

    /// <summary>
    ///     Get field / property type
    /// </summary>
    /// <param name="dataType">table column type</param>
    /// <param name="targetType">generate target type</param>
    /// <returns>field / property type</returns>
    /// <exception cref="UnsupportedTargetTypeException">if target type is not supported</exception>
    public static FieldType GetFieldType(DataType dataType, TargetType targetType)
    {
        return targetType switch
        {
            TargetType.CSharpClass => FieldTypeHelper.GetCSharpPropertyType(dataType),
            TargetType.JavaClass => FieldTypeHelper.GetJavaFieldType(dataType),
            _ => throw new UnsupportedTargetTypeException(targetType)
        };
    }
}
