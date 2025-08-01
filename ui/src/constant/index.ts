/**
 * constants
 * @author shiloh
 * @date 2025/2/5 11:18
 */
import type { QSelectOption } from 'quasar'
import { DbType, JsonPropertyCaseType, TargetType } from 'src/enums'
import type { EditorLanguage } from 'src/types/code-editor'

// supported database type options
export const DB_TYPE_OPTIONS: QSelectOption<DbType>[] = [
  { label: DbType[DbType.MySQL], value: DbType.MySQL },
  { label: DbType[DbType.PostgreSQL], value: DbType.PostgreSQL },
  { label: DbType[DbType.SQLServer], value: DbType.SQLServer },
  { label: DbType[DbType.SQLite], value: DbType.SQLite },
]

// supported target type options for generate class code
export const CLASS_CODE_TARGET_TYPE_OPTIONS: QSelectOption<TargetType>[] = [
  { label: TargetType[TargetType.CSharpClass], value: TargetType.CSharpClass },
  { label: TargetType[TargetType.CSharpRecord], value: TargetType.CSharpRecord },
  { label: TargetType[TargetType.CSharpStruct], value: TargetType.CSharpStruct },
  { label: TargetType[TargetType.JavaClass], value: TargetType.JavaClass },
  { label: TargetType[TargetType.JavaRecord], value: TargetType.JavaRecord },
]

// supported target type options for generate ORM entity
export const ENTITY_CODE_TARGET_TYPE_OPTIONS: QSelectOption<TargetType>[] = [
  { label: TargetType[TargetType.SqlSugarEntity], value: TargetType.SqlSugarEntity },
  { label: TargetType[TargetType.EFCoreEntity], value: TargetType.EFCoreEntity },
  { label: TargetType[TargetType.MyBatisPlusEntity], value: TargetType.MyBatisPlusEntity },
  { label: TargetType[TargetType.HibernateEntity], value: TargetType.HibernateEntity },
]

// supported target type options for generate ORM entity by json code
export const JSON_TO_ENTITY_CODE_TARGET_TYPE_OPTIONS: QSelectOption<TargetType>[] = [
  {
    label: TargetType[TargetType.SpringDataMongoDBEntity],
    value: TargetType.SpringDataMongoDBEntity,
  },
  {
    label: TargetType[TargetType.CSharpMongoDBDriverEntity],
    value: TargetType.CSharpMongoDBDriverEntity,
  },
]

// target type & editor language mapping
export const TARGET_TYPE_LANGUAGE_MAPPING: Record<TargetType, EditorLanguage> = {
  [TargetType.CSharpClass]: 'csharp',
  [TargetType.CSharpRecord]: 'csharp',
  [TargetType.CSharpStruct]: 'csharp',
  [TargetType.SqlSugarEntity]: 'csharp',
  [TargetType.EFCoreEntity]: 'csharp',
  [TargetType.CSharpMongoDBDriverEntity]: 'csharp',
  [TargetType.JavaClass]: 'java',
  [TargetType.JavaRecord]: 'java',
  [TargetType.MyBatisPlusEntity]: 'java',
  [TargetType.HibernateEntity]: 'java',
  [TargetType.SpringDataMongoDBEntity]: 'java',
}

// editor tab size
export const EDITOR_TAB_SIZE: Record<EditorLanguage, number> = {
  csharp: 4,
  java: 4,
  json: 2,
  sql: 2,
  typescript: 2,
  javascript: 2,
}

// JSON property case type options
export const JSON_PROPERTY_CASE_TYPE_OPTIONS: QSelectOption<JsonPropertyCaseType>[] = [
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.LowerCase],
    value: JsonPropertyCaseType.LowerCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.UpperCase],
    value: JsonPropertyCaseType.UpperCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.CamelCase],
    value: JsonPropertyCaseType.CamelCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.PascalCase],
    value: JsonPropertyCaseType.PascalCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.SnakeCase],
    value: JsonPropertyCaseType.SnakeCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.KebabCase],
    value: JsonPropertyCaseType.KebabCase,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.SnakeCaseUpper],
    value: JsonPropertyCaseType.SnakeCaseUpper,
  },
  {
    label: JsonPropertyCaseType[JsonPropertyCaseType.KebabCaseUpper],
    value: JsonPropertyCaseType.KebabCaseUpper,
  },
]
