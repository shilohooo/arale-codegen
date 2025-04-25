# Arale CodeGen

<div align="center">
  <img src="./project-logo.png" alt="project-logo" />
</div>

> [!IMPORTANT]
> 🚧 The project is currently under active development and features are subject to change at any time.

[Arale CodeGen](https://shiloh595.top/arale-codegen-ui/) is a code generation tool that can read SQL DDL or JSON
Object/Array to generate the specified code.

The goal of this project is to reduce duplication of work in CRUD, such as writing entity code based on the table
structure, or write the corresponding DTO code based on the structure of the JSON object.

## 🚀 Features

- [Read the SQL DDL](https://shiloh595.top/arale-codegen-ui/#/sql-convert/sql-to-entity) / JSON to generate entity of
  ORM / persistence framework, such
  as [Hibernate](https://hibernate.org/orm/), [Entity Framework Core](https://docs.microsoft.com/ef/),
  [SqlSugar](https://www.donet5.com/home/doc), [MyBatisPlus](https://baomidou.com/)
- [Read the SQL DDL](https://shiloh595.top/arale-codegen-ui/#/sql-convert/sql-to-class) / [JSON](https://shiloh595.top/arale-codegen-ui/#/json-convert/json-to-class)
  to generate class,
  record, struct
  for [Java](https://openjdk.org/), [C#](https://learn.microsoft.com/en-us/dotnet/csharp/).
- [Read the JSON](https://shiloh595.top/arale-codegen-ui/#/json-convert/json-to-ts) to generate type /
  interface for [TypeScript](https://www.typescriptlang.org/).
- [Read the JSON](https://shiloh595.top/arale-codegen-ui/#/json-convert/json-to-js) to generate object
  for [JavaScript](https://developer.mozilla.org/en-US/docs/Web/JavaScript).
- [JSON object property naming style](https://shiloh595.top/arale-codegen-ui/#/json-convert/json-property-case)
  conversion (camelCase, snake_case, PascalCase, kebab-case).
- [Convert URL query string to TS](https://shiloh595.top/arale-codegen-ui/#/query-str-convert/query-str-to-ts) type
  alias or interface
- [Convert URL query string to JS](https://shiloh595.top/arale-codegen-ui/#/query-str-convert/query-str-to-js) object
  literal
- [Convert JSON object or array to JSDoc comment](https://shiloh595.top/arale-codegen-ui/#/json-convert/json-to-jsdoc)

> [!NOTE]
> Only the following databases are supported

- [SQL Server](https://learn.microsoft.com/en-us/sql/?view=sql-server-ver16)
- [MySQL](https://www.mysql.com/)
- [PostgreSQL](https://www.postgresql.org/)
- [SQLite](https://www.sqlite.org/)

## 🪄 Credits

The logo has been designed by [Free Logo Design](https://www.freelogodesign.org/).

## 💻 Stack

### Backend

| Name                   | Version       | Documentation                                                        | Remark                   |
|------------------------|---------------|----------------------------------------------------------------------|--------------------------|
| C#                     | 12            | <https://learn.microsoft.com/en-us/dotnet/csharp/>                   | Language                 |
| SqlParser-cs           | 0.6.2         | <https://github.com/TylerBrinks/SqlParser-cs>                        | SQL Parser               |
| Scriban                | 5.12.1        | <https://github.com/scriban/scriban>                                 | Template Engine          |
| Pluralize.NET          | 1.0.2         | <https://github.com/sarathkcm/Pluralize.NET>                         |                          |
| ASP.NET Core (Web API) | .NET 8.0      | <https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-8.0> | Web Framework            |
| Npgsql                 | 9.0.2         | <https://www.npgsql.org/>                                            | PostgreSQL Data Provider |
| Humanizr               | 3.0.0-beta.54 | <https://github.com/Humanizr/Humanizer>                              | Change naming style      |
| xUnit.net              | 2.5.3         | <https://xunit.net/>                                                 | Unit Tests               |

### Frontend

| Name                 | Version | Documentation                                         | Remark                       |
|----------------------|---------|-------------------------------------------------------|------------------------------|
| NodeJS               | 20.11.1 | <https://nodejs.org>                                  |                              |
| PNPM                 | 8.5.13  | <https://pnpm.io/>                                    | Package Manager              |
| TypeScript           | 5.4     | <https://www.typescriptlang.org/docs/>                |                              |
| Vue                  | 3.4.27  | <https://vuejs.org/>                                  |                              |
| Quasar               | 2.16.0  | <https://quasar.dev/docs>                             | UI Framework                 |
| JsonToTs             | 2.1.0   | <https://github.com/MariusAlch/json-to-ts>            | JSON to TypeScript           |
| UnpluginAutoImport   | 0.18.2  | <https://github.com/unplugin/unplugin-auto-import>    |                              |
| Monaco Vue           | v1.5.4  | <https://github.com/imguolao/monaco-vue>              | Code Editor                  |
| sql-formatter        | 15.5.1  | <https://github.com/sql-formatter-org/sql-formatter>  |                              |
| debounce             | 2.2.0   | <https://github.com/sindresorhus/debounce>            | Delay function               |
| javascript-stringify | 2.1.0   | <https://github.com/blakeembrey/javascript-stringify> | JSON to JS object literal    |
| json-case-converter  | 1.4.4   | <https://github.com/MIRTAHAALI/json-case-convertor>   | JSON property case converter |
| qs                   | 6.14.0  | <https://github.com/ljharb/qs>                        | Query String Parser          |
| json-to-jsdoc        | 1.0.3   | <https://github.com/shilohooo/json-to-jsdoc>          |                              |

### Deployment

| Name           | Version | Documentation                         | Remark |
|----------------|---------|---------------------------------------|--------|
| GitHub Actions |         | <https://github.com/features/actions> | CI/CD  |
| Nginx          | 1.26.0  | <https://nginx.org/en/docs/>          |        |
| Docker         | 27.1    | <https://docs.docker.com/manuals/>    |        |

## 📖 References

- [C# & SQL Server Data Type Mapping](https://learn.microsoft.com/zh-cn/sql/language-extensions/how-to/c-sharp-to-sql-data-types?view=sql-server-ver16)
- [C# & MySQL Data Type Mapping](https://zontroy.com/mysql-to-csharp-type-mapping)
- [C# & SQLite Data Type Mapping](https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/types)
- [C# & PostgreSQL Data Type Mapping](https://zontroy.com/postgresql-to-csharp-type-mapping)
- [Java & SQL Server Data Type Mapping](https://learn.microsoft.com/en-us/sql/language-extensions/how-to/java-to-sql-data-types?view=sql-server-ver16)
- [Java & MySQL Data Type Mapping](https://dev.mysql.com/doc/connector-j/en/connector-j-reference-type-conversions.html)
- [Java & PostgreSQL Data Type Mapping](https://zontroy.com/postgresql-to-java-type-mapping)
- [A polyglot web converter](https://github.com/ritz078/transform)

## 📝 Changelog

[CHANGELOG.md](./CHANGELOG.md)

## 💪 Contributors

|                                             Shiloh                                              |
|:-----------------------------------------------------------------------------------------------:|
| [![Shiloh](https://avatars.githubusercontent.com/u/46670399?v=4)](https://github.com/shilohooo) |

## 🔖 License

Copyright © 2025-present Shiloh

[MIT](./LICENSE)
