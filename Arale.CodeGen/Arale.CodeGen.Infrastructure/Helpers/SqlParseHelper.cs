using Arale.CodeGen.Commons.Constants;
using SqlParser;
using SqlParser.Ast;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility methods for parse sql to ast
/// </summary>
public static class SqlParseHelper
{
    private static readonly Parser Parser = new();

    /// <summary>
    ///     Parse sql to ast
    /// </summary>
    /// <param name="sql">sql code</param>
    /// <param name="dbType">db type</param>
    /// <returns>ast</returns>
    public static Sequence<Statement> Parse(string sql, DbType dbType)
    {
        return Parser.ParseSql(sql, DbDialectHelper.GetDialect(dbType));
    }
}
