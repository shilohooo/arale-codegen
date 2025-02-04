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
        var tableInfo = new TableInfo
        {
            TableName = tableElement.Name.ToString(),
            ClassName = PluralizerHelper.Singularize(tableElement.Name.ToString()
                .Replace(codeGenerateBySqlReq.TableNamePrefix, string.Empty))
        };

        tableInfo.Comment = tableElement.Comment?.ToString() ?? $"{tableInfo.ClassName} class";

        var columnDefs = tableElement.Columns.ToList();
        List<ColumnInfo> columns = [];
        foreach (var (columnName, dataType, _, columnDefOptions) in columnDefs)
        {
            Console.WriteLine($"columnName: {columnName}, dataType: {dataType}");
            var name = columnName.ToString();
            var columnInfo = new ColumnInfo
            {
                Name = name,
                FieldName = codeGenerateBySqlReq.TargetType switch
                {
                    // c# property naming convention is PascalCase
                    TargetType.CSharpClass => (char)(name[0] - 32) + name[1..],
                    _ => name
                },
                Comment = name,
                FieldType = codeGenerateBySqlReq.TargetType switch
                {
                    TargetType.CSharpClass => FieldTypeHelper.GetCSharpPropertyType(dataType),
                    TargetType.JavaClass => FieldTypeHelper.GetJavaFieldType(dataType),
                    _ => throw new UnsupportedTargetTypeException(codeGenerateBySqlReq.TargetType)
                }
            };
            // check data type is bool?
            if (dataType.ToSql().Contains("bit"))
                columnInfo.FieldType = codeGenerateBySqlReq.TargetType switch
                {
                    TargetType.CSharpClass => "bool",
                    TargetType.JavaClass => "Boolean",
                    _ => throw new UnsupportedTargetTypeException(codeGenerateBySqlReq.TargetType)
                };

            if (columnDefOptions?.Count > 0)
                foreach (var columnDefOption in columnDefOptions)
                    switch (columnDefOption.Option)
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

            var match = RegexConstant.LengthPattern().Match(dataType.ToSql());
            columnInfo.Length = match.Groups[1].Value;

            columns.Add(columnInfo);
        }

        tableInfo.Columns = columns;
        return tableInfo;
    }
}
