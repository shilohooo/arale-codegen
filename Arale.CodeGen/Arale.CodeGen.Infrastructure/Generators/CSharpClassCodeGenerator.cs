using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     C# Class Code Generator
/// </summary>
public class CSharpClassCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.CSharpClass;
    }
}
