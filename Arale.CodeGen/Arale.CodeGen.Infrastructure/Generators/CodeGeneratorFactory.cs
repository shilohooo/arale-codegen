using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Code generator factory
/// </summary>
public static class CodeGeneratorFactory
{
    /// <summary>
    ///     Create code generator by target type
    /// </summary>
    /// <param name="targetType">target type</param>
    /// <returns>Code generator instance</returns>
    /// <exception cref="ArgumentException">If target type is not supported</exception>
    public static ICodeGenerator Create(TargetType targetType)
    {
        return targetType switch
        {
            TargetType.CSharpClass => new CSharpClassCodeGenerator(),
            TargetType.SqlSugarEntity => new SqlSugarEntityCodeGenerator(),
            TargetType.EFCoreEntity => new EFCoreEntityCodeGenerator(),
            TargetType.JavaClass => new JavaClassCodeGenerator(),
            TargetType.MyBatisPlusEntity => new MyBatisPlusEntityCodeGenerator(),
            TargetType.HibernateEntity => new HibernateEntityCodeGenerator(),
            _ => throw new ArgumentException($"Unsupported target type: {targetType}", nameof(targetType))
        };
    }
}
