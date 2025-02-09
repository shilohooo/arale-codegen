using Humanizer;

namespace Arale.CodeGen.Tests;

/// <summary>
///     Unit test of change naming style
///     <remarks>
///         camelCase => PascalCase
///         PascalCase => camelCase
///         snake_case => camelCase
///         camelCase => snake_case
///         kebab-case => camelCase
///         camelCase => kebab-case
///         kebab-case => snake_case
///         snake_case => PascalCase
///         PascalCase => snake_case
///         snake_case => kebab-case
///     </remarks>
/// </summary>
public class HumanizrTests
{
    [Fact]
    public void TestCamelCaseToPascalCase()
    {
        const string fileName = "camelCaseFieldName";
        Assert.Equal("CamelCaseFieldName", fileName.Pascalize());
    }

    [Fact]
    public void TestPascalCaseToCamelCase()
    {
        const string fileName = "PascalCaseFieldName";
        Assert.Equal("pascalCaseFieldName", fileName.Camelize());
    }

    [Fact]
    public void TestSnakeCaseToCamelCase()
    {
        const string fileName = "snake_case_field_name";
        Assert.Equal("snakeCaseFieldName", fileName.Camelize());
    }

    [Fact]
    public void TestCamelCaseToSnakeCase()
    {
        const string fileName = "camelCaseFieldName";
        Assert.Equal("camel_case_field_name", fileName.Underscore());
    }

    [Fact]
    public void TestKebabCaseToCamelCase()
    {
        const string fileName = "kebab-case-field-name";
        Assert.Equal("kebabCaseFieldName", fileName.Camelize());
    }

    [Fact]
    public void TestCamelCaseToKebabCase()
    {
        const string fileName = "camelCaseFieldName";
        Assert.Equal("camel-case-field-name", fileName.Underscore().Dasherize());
    }

    [Fact]
    public void TestSnakeCaseToPascalCase()
    {
        const string fileName = "snake_case_field_name";
        Assert.Equal("SnakeCaseFieldName", fileName.Pascalize());
    }

    [Fact]
    public void TestPascalCaseToSnakeCase()
    {
        const string fileName = "PascalCaseFieldName";
        Assert.Equal("pascal_case_field_name", fileName.Underscore());
    }

    [Fact]
    public void TestSnakeCaseToKebabCase()
    {
        const string fileName = "snake_case_field_name";
        Assert.Equal("snake-case-field-name", fileName.Dasherize());
    }
}
