using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of generate target types
/// </summary>
public enum TargetType
{
    [Description("C# Class")] CSharpClass = 1,

    [Description("C# Record")] CSharpRecord = 2,

    // [Description("C# Struct")] CSharpStruct = 3,
    [Description("SqlSugar Entity")] SqlSugarEntity = 4,
    [Description("EF Core Entity")] EFCoreEntity = 5,
    [Description("Java Class")] JavaClass = 6,
    [Description("Java Record")] JavaRecord = 7,
    [Description("MyBatisPlus Entity")] MyBatisPlusEntity = 8,

    [Description("Hibernate (JPA) Entity")]
    HibernateEntity = 9
}
