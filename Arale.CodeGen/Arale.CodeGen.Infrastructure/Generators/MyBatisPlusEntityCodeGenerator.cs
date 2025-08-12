using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     MyBatisPlus Entity Code Generator
/// </summary>
public class MyBatisPlusEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.MyBatisPlusEntity;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.MyBatisPlusEntity;
    }
}
