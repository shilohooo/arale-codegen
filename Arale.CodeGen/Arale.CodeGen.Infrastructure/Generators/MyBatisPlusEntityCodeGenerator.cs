using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     MyBatisPlus Entity Code Generator
/// </summary>
public class MyBatisPlusEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.MyBatisPlusEntity;
    }
}
