/**
 * enums
 * @author shiloh
 * @date 2025/2/5 10:26
 */

// supported database type
export enum DbType {
  MySQL = 1,
  SQLServer = 2,
  PostgreSQL = 3,
  SQLite = 4,
}

// generate target type
export enum TargetType {
  CSharpClass = 1,
  CSharpRecord = 2,
  CSharpStruct = 3,
  SqlSugarEntity = 4,
  EFCoreEntity = 5,
  JavaClass = 6,
  JavaRecord = 7,
  MyBatisPlusEntity = 8,
  HibernateEntity = 9,
  SpringDataMongoDBEntity = 10,
  CSharpMongoDBDriverEntity = 11,
}

// JSON property case type
export enum JsonPropertyCaseType {
  LowerCase = 1,
  UpperCase = 2,
  CamelCase = 3,
  PascalCase = 4,
  SnakeCase = 5,
  KebabCase = 6,
  SnakeCaseUpper = 7,
  KebabCaseUpper = 8,
}

// supported languages
export enum LanguageType {
  CSharp = 1,
  Java = 2,
  JavaScript = 3,
  TypeScript = 4,
  SQL = 5,
  JSON = 6,
}
