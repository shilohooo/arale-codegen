﻿using Arale.CodeGen.Commons.Constants;

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
            TargetType.CSharpRecord => new CSharpRecordCodeGenerator(),
            TargetType.CSharpStruct => new CSharpStructCodeGenerator(),
            TargetType.SqlSugarEntity => new SqlSugarEntityCodeGenerator(),
            TargetType.EFCoreEntity => new EFCoreEntityCodeGenerator(),
            TargetType.JavaClass => new JavaClassCodeGenerator(),
            TargetType.JavaRecord => new JavaRecordCodeGenerator(),
            TargetType.MyBatisPlusEntity => new MyBatisPlusEntityCodeGenerator(),
            TargetType.HibernateEntity => new HibernateEntityCodeGenerator(),
            TargetType.SpringDataMongoDbEntity => new SpringDataMongoDbEntityCodeGenerator(),
            TargetType.CSharpMongoDbDriverEntity => new CSharpMongoDbDriverEntityCodeGenerator(),
            _ => throw new ArgumentException($"Unsupported target type: {targetType}", nameof(targetType))
        };
    }
}
