using Arale.CodeGen.Commons.Constants;
using SqlParser.Dialects;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility  methods for db type
/// </summary>
public static class DbDialectHelper
{
    /// <summary>
    ///     Get db dialect by specified db type
    /// </summary>
    /// <param name="dbType"></param>
    /// <returns>db dialect instance</returns>
    /// <exception cref="ArgumentException">when an unsupported db type passed</exception>
    public static Dialect GetDialect(DbType dbType)
    {
        return dbType switch
        {
            DbType.MySql => new MySqlDialect(),
            DbType.SqlServer => new MsSqlDialect(),
            DbType.PostgreSql => new PostgreSqlDialect(),
            DbType.Sqlite => new SQLiteDialect(),
            _ => throw new ArgumentException($"Unsupported db type: {dbType}", nameof(dbType))
        };
    }
}
