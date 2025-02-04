using Pluralize.NET;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility methods for singular and plural words
/// </summary>
public static class PluralizerHelper
{
    private static readonly Pluralizer Pluralizer = new();

    /// <summary>
    ///     Singularize a word
    /// </summary>
    /// <param name="word">word</param>
    /// <returns>word after singularization</returns>
    public static string Singularize(string word)
    {
        return Pluralizer.Singularize(word);
    }
}
