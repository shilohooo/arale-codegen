using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Supported database types
/// </summary>
public enum DbType
{
    [Description("MySQL")] MySql = 1,
    [Description("SQL Server")] SqlServer = 2,
    [Description("PostgreSQL")] PostgreSql = 3,
    [Description("SQLite")] Sqlite = 4
}
