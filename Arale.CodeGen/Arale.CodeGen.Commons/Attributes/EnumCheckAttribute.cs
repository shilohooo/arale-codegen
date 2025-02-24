using System.ComponentModel.DataAnnotations;

namespace Arale.CodeGen.Commons.Attributes;

/// <summary>
///     Enum value check attribute
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class EnumCheckAttribute(Type enumType, bool required = true) : ValidationAttribute
{
    /// <summary>
    ///     Enum type
    /// </summary>
    private readonly Type _enumType = enumType;

    /// <summary>
    ///     Is required
    /// </summary>
    private readonly bool _required = required;

    /// <inheritdoc />
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null || !_required) return ValidationResult.Success;

        return Enum.IsDefined(_enumType, value)
            ? ValidationResult.Success
            : new ValidationResult(ErrorMessage ?? $"{value} is not a valid value for type {_enumType.Name}.");
    }
}
