using System.ComponentModel;
using Arale.CodeGen.Extensions;

namespace Arale.CodeGen.Tests;

/// <summary>
///     TemplateHelper unit tests
/// </summary>
public class TemplateHelperTests
{
    /// <summary>
    ///     Test getting Enum description by Enum extension method
    /// </summary>
    [Fact]
    public void TestGetEnumDescription()
    {
        Assert.Equal("星期一", Week.Monday.GetDescription());
    }
}

/// <summary>
///     Enum of weeks
/// </summary>
public enum Week
{
    [Description("星期一")] Monday
}
