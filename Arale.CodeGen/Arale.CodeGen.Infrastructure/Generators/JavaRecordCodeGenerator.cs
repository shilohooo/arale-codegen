using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Java Record Code Generator
/// </summary>
public class JavaRecordCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.JavaRecord;
 /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.JavaRecord;
    }
}
