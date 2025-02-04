using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Java Class Code Generator
/// </summary>
public class JavaClassCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.JavaClass;
    }
}
