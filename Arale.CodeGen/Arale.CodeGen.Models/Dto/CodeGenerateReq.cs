using System.ComponentModel.DataAnnotations;
using Arale.CodeGen.Commons.Attributes;
using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Models.Dto;

/// <summary>
///     Code generate request params
/// </summary>
public class CodeGenerateReq
{
    /// <summary>
    ///     SQL / JSON code to convert
    /// </summary>
    [Required(ErrorMessage = "SQL / JSON code is required")]
    [MinLength(1, ErrorMessage = "SQL / JSON code can't not be empty")]
    public required string Code { get; set; }

    /// <summary>
    ///     Database type
    /// </summary>
    [EnumCheck(typeof(DbType), false, ErrorMessage = "Unsupported dbType")]
    public DbType DbType { get; set; }

    /// <summary>
    ///     Generate target type
    /// </summary>
    [EnumCheck(typeof(TargetType), false, ErrorMessage = "Unsupported targetType")]
    public TargetType TargetType { get; set; }

    /// <summary>
    ///     Table name prefix
    /// </summary>
    public string? TableNamePrefix { get; init; } = string.Empty;

    /// <inheritdoc />
    public override string ToString()
    {
        return
            $"{nameof(Code)}: {Code}, {nameof(DbType)}: {DbType}, {nameof(TargetType)}: {TargetType}, {nameof(TableNamePrefix)}: {TableNamePrefix}";
    }
}
