﻿using System.Text.Json.Nodes;
using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Commons.Exceptions;
using Arale.CodeGen.Models.Entity;
using Humanizer;
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
        return targetType switch
        {
            // c# property naming convention is PascalCase
            TargetType.CSharpClass or TargetType.CSharpRecord or TargetType.CSharpStruct or TargetType.SqlSugarEntity
                or TargetType.EFCoreEntity or TargetType.CSharpMongoDbDriverEntity => columnName.Pascalize(),
            // java field naming convention is camelCase
            TargetType.JavaClass or TargetType.JavaRecord or TargetType.MyBatisPlusEntity or TargetType.HibernateEntity
                or TargetType.SpringDataMongoDbEntity
                => columnName.Camelize(),
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
            TargetType.CSharpClass or TargetType.CSharpRecord or TargetType.CSharpStruct or TargetType.SqlSugarEntity
                or TargetType.EFCoreEntity
                or TargetType.CSharpMongoDbDriverEntity => FieldTypeHelper.GetCSharpPropertyType(dataType),
            TargetType.JavaClass or TargetType.JavaRecord or TargetType.MyBatisPlusEntity or TargetType.HibernateEntity
                or TargetType.SpringDataMongoDbEntity
                => FieldTypeHelper.GetJavaFieldType(dataType),
            _ => throw new UnsupportedTargetTypeException(targetType)
        };
    }

    /// <summary>
    ///     Get field / property type
    /// </summary>
    /// <param name="jsonKv">json key value pari</param>
    /// <param name="targetType">generate target type</param>
    /// <returns>field / property type</returns>
    /// <exception cref="UnsupportedTargetTypeException">if target type is not supported</exception>
    public static FieldType GetFieldType(KeyValuePair<string, JsonNode?> jsonKv, TargetType targetType)
    {
        return targetType switch
        {
            TargetType.CSharpClass or TargetType.CSharpRecord or TargetType.CSharpStruct or TargetType.SqlSugarEntity
                or TargetType.EFCoreEntity
                or TargetType.CSharpMongoDbDriverEntity => FieldTypeHelper.GetCSharpPropertyType(jsonKv),
            TargetType.JavaClass or TargetType.JavaRecord or TargetType.MyBatisPlusEntity or TargetType.HibernateEntity
                or TargetType.SpringDataMongoDbEntity
                => FieldTypeHelper.GetJavaFieldType(jsonKv),
            _ => throw new UnsupportedTargetTypeException(targetType)
        };
    }
}
