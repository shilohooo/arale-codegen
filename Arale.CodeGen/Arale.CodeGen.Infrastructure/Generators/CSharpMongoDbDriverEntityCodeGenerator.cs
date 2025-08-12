using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
/// </summary>
public class CSharpMongoDbDriverEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.CSharpMongoDbDriverEntity;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.CSharpMongoDbDriverEntity;
    }
}
