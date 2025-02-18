using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Infrastructure.Generators;

/// <summary>
///     Hibernate (JPA) entity code generator
/// </summary>
public class HibernateEntityCodeGenerator : BaseCodeGenerator
{
    /// <inheritdoc />
    protected override TemplateName GetTemplateName()
    {
        return TemplateName.HibernateEntity;
    }
}
