using Arale.CodeGen.Commons.Constants;

namespace Arale.CodeGen.Commons.Exceptions;

/// <summary>
///     Unsupported target type exception
///     <param name="targetType">target type</param>
/// </summary>
public class UnsupportedTargetTypeException(TargetType targetType) : Exception($"Unsupported targetType: {targetType}");
