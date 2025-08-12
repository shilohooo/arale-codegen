using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     C# Record Code Generator
/// </summary>
public class CSharpRecordCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    public override TargetType SupportedTargetType => TargetType.CSharpRecord;

    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.CSharpRecord;
    }
}
