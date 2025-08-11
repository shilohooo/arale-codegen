using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Commons.Exceptions;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
/// </summary>
public static class LanguageTypeHelper
{
    /// <summary>
    ///     Get language type by target type
    /// </summary>
    /// <param name="targetType">target type</param>
    /// <returns>language type</returns>
    /// <exception cref="UnsupportedTargetTypeException">if target type is not supported</exception>
    public static LanguageType GetByTargetType(TargetType targetType)
    {
        return targetType switch
        {
            TargetType.CSharpClass or TargetType.CSharpRecord or TargetType.CSharpStruct or TargetType.SqlSugarEntity
                or TargetType.EFCoreEntity or TargetType.CSharpMongoDbDriverEntity => LanguageType.CSharp,
            TargetType.JavaClass or TargetType.JavaRecord or TargetType.MyBatisPlusEntity or TargetType.HibernateEntity
                or TargetType.SpringDataMongoDbEntity
                => LanguageType.Java,
            _ => throw new UnsupportedTargetTypeException(targetType)
        };
    }
}
