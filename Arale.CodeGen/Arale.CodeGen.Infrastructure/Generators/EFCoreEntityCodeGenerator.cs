using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     EFCore entity code generator
/// </summary>
public class EFCoreEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.EFCoreEntity;
    }
}
