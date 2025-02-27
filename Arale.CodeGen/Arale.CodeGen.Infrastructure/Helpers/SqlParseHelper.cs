using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Models;
using Arale.CodeGen.Models.Dto;
using Humanizer;
using SqlParser;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Util methods for generate entity
/// </summary>
public static class SqlParseHelper
{
    private static readonly Parser Parser = new();

    /// <summary>
    ///     Parse SQL DDL to model info
    /// </summary>
    /// <param name="codeGenerateReq">code generate params</param>
    /// <exception cref="ArgumentException">if SQL DDL invalid</exception>
    /// <returns>table info instance</returns>
    public static ModelInfo Parse(CodeGenerateReq codeGenerateReq)
    {
        var ast = Parser.ParseSql(codeGenerateReq.Code, DbDialectHelper.GetDialect(codeGenerateReq.DbType));
        var createTable = ast[0] as Statement.CreateTable;
        if (createTable == null)
            throw new ArgumentException($"Invalid create table statement: {codeGenerateReq.Code}");

        var tableElement = createTable.Element;
        var model = CreateModelInfo(codeGenerateReq, tableElement);
        model.Properties = CreateProperties(codeGenerateReq, tableElement);
        model.ImportStatements = model.Properties
            .Where(c => c.FieldType is not null && !string.IsNullOrWhiteSpace(c.FieldType?.ImportStatement))
            .Select(c => c.FieldType!.ImportStatement!)
            .ToHashSet();
        return model;
    }

    /// <summary>
    ///     Create model info by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">code generate params</param>
    /// <param name="tableElement">table element</param>
    /// <returns>model info</returns>
    private static ModelInfo CreateModelInfo(CodeGenerateReq codeGenerateReq, CreateTable tableElement)
    {
        var modelName = tableElement.Name.ToString();
        var model = new ModelInfo
        {
            Name = modelName,
            ClassName = GetClassName(codeGenerateReq, modelName)
        };
        model.Comment = tableElement.Comment?.ToString() ?? $"{model.ClassName}";
        return model;
    }

    /// <summary>
    ///     Create properties by SQL DDL
    /// </summary>
    /// <param name="codeGenerateReq">code generate params</param>
    /// <param name="tableElement">table element</param>
    /// <returns>properties</returns>
    private static List<PropertyInfo> CreateProperties(CodeGenerateReq codeGenerateReq, CreateTable tableElement)
    {
        return tableElement.Columns.Select(columnDef =>
        {
            var name = columnDef.Name.ToString();
            var propertyInfo = new PropertyInfo
            {
                Name = name,
                ColType = columnDef.DataType.ToSql().ToLower(),
                FieldName = FieldHelper.GetFieldName(name, codeGenerateReq.TargetType),
                Comment = name,
                FieldType = FieldHelper.GetFieldType(columnDef.DataType, codeGenerateReq.TargetType)
            };
            if (columnDef.Options?.Count > 0)
                foreach (var columnOptionDef in columnDef.Options)
                    switch (columnOptionDef.Option)
                    {
                        case ColumnOption.Comment comment:
                            propertyInfo.Comment = comment.Value;
                            break;
                        case ColumnOption.Unique { IsPrimary: true }:
                            propertyInfo.IsPrimaryKey = true;
                            break;
                        case ColumnOption.NotNull:
                            propertyInfo.Mandatory = true;
                            break;
                    }

            var match = RegexConstant.LengthPattern().Match(columnDef.DataType.ToSql());
            propertyInfo.Length = match.Groups[1].Value;
            return propertyInfo;
        }).ToList();
    }

    /// <summary>
    ///     Get class name
    /// </summary>
    /// <param name="codeGenerateReq">class code generate params</param>
    /// <param name="tableName">table name</param>
    /// <returns>class name</returns>
    private static string GetClassName(CodeGenerateReq codeGenerateReq, string tableName)
    {
        var className = string.IsNullOrWhiteSpace(codeGenerateReq.TableNamePrefix)
            ? tableName
            : tableName.Replace(codeGenerateReq.TableNamePrefix, string.Empty);
        return className.Pascalize();
    }
}
