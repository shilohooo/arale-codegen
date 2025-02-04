using Arale.CodeGen.Commons.Constants;
using Arale.CodeGen.Extensions;
using Scriban;

namespace Arale.CodeGen.Infrastructure.Helpers;

/// <summary>
///     Utility methods for template operations
/// </summary>
public static class TemplateHelper
{
    private const string TemplateFolder = "Templates";
    private const string TemplateFileExtension = "txt";

    /// <summary>
    ///     Render template
    /// </summary>
    /// <param name="templateName">template name</param>
    /// <param name="model">model data</param>
    /// <returns>rendered template content</returns>
    public static async Task<string> RenderAsync(TemplateName templateName, object model)
    {
        var baseDir = AppDomain.CurrentDomain.BaseDirectory;
        var filePath = Path.Combine(baseDir, TemplateFolder,
            $"{templateName.GetDescription()}.{TemplateFileExtension}");
        var templateText = await File.ReadAllTextAsync(filePath);
        var template = Template.Parse(templateText);
        return await template.RenderAsync(new { model });
    }
}
