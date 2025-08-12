using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
/// </summary>
public class SpringDataMongoDbEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.SpringDataMongoDbEntity;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.SpringDataMongoDbEntity;
    }
}
