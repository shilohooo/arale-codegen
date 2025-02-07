using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Commons.Exceptions;
using Arale.CodeGen.Models.Db;
using Arale.CodeGen.Models.Dto;
using SqlParser;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Util methods for generate entity
/// </summary>
public static class ClassHelper
{
    /// <summary>
    ///     Parse SQL DDL to class code
    /// </summary>
    /// <remarks>TODO: import / using dependencies when generate class code</remarks>
    /// <param name="codeGenerateBySqlReq">code generate params</param>
    /// <exception cref="ArgumentException">if sql ddl invalid</exception>
    /// <returns>Entity</returns>
    public static TableInfo ToClassBySql(CodeGenerateBySqlReq codeGenerateBySqlReq)
    {
        var ast = SqlParseHelper.Parse(codeGenerateBySqlReq.Code, codeGenerateBySqlReq.DbType);
        var createTable = ast[0] as Statement.CreateTable;
        if (createTable == null)
            throw new ArgumentException($"Invalid create table statement: {codeGenerateBySqlReq.Code}");

        var tableElement = createTable.Element;
        var tableInfo = CreateTableInfo(codeGenerateBySqlReq, tableElement);
        tableInfo.Columns = CreateColumnInfos(codeGenerateBySqlReq, tableElement);
        return tableInfo;
    }

    /// <summary>
    ///     Create table info by SQL DDL
    /// </summary>
    /// <param name="codeGenerateBySqlReq">class code generate params</param>
    /// <param name="tableElement">table element</param>
    /// <returns>table info</returns>
    private static TableInfo CreateTableInfo(CodeGenerateBySqlReq codeGenerateBySqlReq, CreateTable tableElement)
    {
        var tableName = tableElement.Name.ToString();
        var tableInfo = new TableInfo
        {
            TableName = tableName,
            ClassName = string.IsNullOrWhiteSpace(codeGenerateBySqlReq.TableNamePrefix)
                ? tableName
                : tableName.Replace(codeGenerateBySqlReq.TableNamePrefix, string.Empty)
        };
        tableInfo.Comment = tableElement.Comment?.ToString() ?? $"{tableInfo.ClassName} class";
        return tableInfo;
    }

    /// <summary>
    ///     Create columns by SQL DDL
    /// </summary>
    /// <param name="codeGenerateBySqlReq">class code generate params</param>
    /// <param name="tableElement">table element</param>
    /// <returns>columns</returns>
    private static List<ColumnInfo> CreateColumnInfos(CodeGenerateBySqlReq codeGenerateBySqlReq,
        CreateTable tableElement)
    {
        return tableElement.Columns.Select(columnDef =>
        {
            var name = columnDef.Name.ToString();
            var columnInfo = new ColumnInfo
            {
                Name = name,
                FieldName = FieldHelper.GetFieldName(name, codeGenerateBySqlReq.TargetType),
                Comment = name,
                FieldType = FieldHelper.GetFieldType(columnDef.DataType, codeGenerateBySqlReq.TargetType)
            };
            // check data type is bool?
            if (columnDef.DataType.ToSql().Contains("bit"))
                columnInfo.FieldType = codeGenerateBySqlReq.TargetType switch
                {
                    TargetType.CSharpClass => "bool",
                    TargetType.JavaClass => "Boolean",
                    _ => throw new UnsupportedTargetTypeException(codeGenerateBySqlReq.TargetType)
                };

            if (columnDef.Options?.Count > 0)
                foreach (var columnOptionDef in columnDef.Options)
                    switch (columnOptionDef.Option)
                    {
                        case ColumnOption.Comment comment:
                            columnInfo.Comment = comment.Value;
                            break;
                        case ColumnOption.Unique { IsPrimary: true }:
                            columnInfo.IsPrimaryKey = true;
                            break;
                        case ColumnOption.NotNull:
                            columnInfo.Mandatory = true;
                            break;
                    }

            var match = RegexConstant.LengthPattern().Match(columnDef.DataType.ToSql());
            columnInfo.Length = match.Groups[1].Value;
            return columnInfo;
        }).ToList();
    }
}
