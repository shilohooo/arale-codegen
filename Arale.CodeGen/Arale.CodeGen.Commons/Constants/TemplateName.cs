﻿using System.ComponentModel;

namespace Arale.CodeGen.Commons.Constants;

/// <summary>
///     Enum of Scriban template file name
/// </summary>
public enum TemplateName
{
    /// <summary>
    ///     C# class template
    /// </summary>
    [Description("CSharp/CSharpClass")] CSharpClass,

    /// <summary>
    ///     C# record template
    /// </summary>
    [Description("CSharp/CSharpRecord")] CSharpRecord,

    /// <summary>
    ///     C# struct template
    /// </summary>
    [Description("CSharp/CSharpStruct")] CSharpStruct,

    /// <summary>
    ///     SqlSugar entity template
    /// </summary>
    [Description("CSharp/SqlSugarEntity")] SqlSugarEntity,

    /// <summary>
    ///     EF Core entity template
    /// </summary>
    [Description("CSharp/EFCoreEntity")] EFCoreEntity,

    /// <summary>
    ///     Java class template
    /// </summary>
    [Description("Java/JavaClass")] JavaClass,

    /// <summary>
    ///     Java record template
    /// </summary>
    [Description("Java/JavaRecord")] JavaRecord,

    /// <summary>
    ///     MyBatisPlus entity template
    /// </summary>
    [Description("Java/MyBatisPlusEntity")]
    MyBatisPlusEntity,

    /// <summary>
    ///     Hibernate (JPA) entity template
    /// </summary>
    [Description("Java/HibernateEntity")] HibernateEntity,

    /// <summary>
    ///     Spring Data MongoDB entity template
    /// </summary>
    [Description("Java/SpringDataMongoDBEntity")]
    SpringDataMongoDbEntity,

    /// <summary>
    ///     CSharp MongoDB.Driver entity template
    /// </summary>
    [Description("CSharp/CSharpMongoDBDriverEntity")]
    CSharpMongoDbDriverEntity
}
