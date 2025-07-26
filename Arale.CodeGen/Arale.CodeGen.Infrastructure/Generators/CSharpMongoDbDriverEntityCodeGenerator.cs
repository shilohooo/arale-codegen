using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
/// </summary>
public class CSharpMongoDbDriverEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.CSharpMongoDbDriverEntity;
    }
}
