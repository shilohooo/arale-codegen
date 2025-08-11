using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     C# struct code generator
/// </summary>
public class CSharpStructCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.CSharpStruct;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.CSharpStruct;
    }
}
