using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     EFCore entity code generator
/// </summary>
public class EFCoreEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.EFCoreEntity;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.EFCoreEntity;
    }
}
