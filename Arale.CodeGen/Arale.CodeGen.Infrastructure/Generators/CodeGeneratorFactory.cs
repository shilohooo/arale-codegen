using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Code generator factory
/// </summary>
public class CodeGeneratorFactory(IEnumerable<ICodeGenerator> codeGenerators)
{
    /// <summary>
    ///     Create code generator by target type
    /// </summary>
    /// <param name="targetType">target type</param>
    /// <returns>Code generator instance</returns>
    /// <exception cref="ArgumentException">If target type is not supported</exception>
    public ICodeGenerator Create(TargetType targetType)
    {
        return codeGenerators.First(codeGenerator => targetType.Equals(codeGenerator.SupportedTargetType));
    }
}
