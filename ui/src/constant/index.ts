/**
 * constants
 * @author shiloh
 * @date 2025/2/5 11:18
 */
import type { QSelectOption } from 'quasar'
import { DbType, JsonPropertyCaseType, LanguageType, TargetType } from 'src/enums'

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

// editor tab size
export const EDITOR_TAB_SIZE: Record<LanguageType, number> = {
  [LanguageType.CSharp]: 4,
  [LanguageType.Java]: 4,
  [LanguageType.JSON]: 2,
  [LanguageType.SQL]: 2,
  [LanguageType.TypeScript]: 2,
  [LanguageType.JavaScript]: 2,
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

// language file extension mapping
export const LANGUAGE_FILE_EXTENSION_MAPPING: Record<LanguageType, string> = {
  [LanguageType.CSharp]: 'cs',
  [LanguageType.Java]: 'java',
  [LanguageType.JavaScript]: 'js',
  [LanguageType.TypeScript]: 'ts',
  [LanguageType.SQL]: 'sql',
  [LanguageType.JSON]: 'json',
}

// language file icon name of https://pictogrammers.com/library/mdi/ in kebab-case
export const LANGUAGE_FILE_ICON_NAME_MAPPING: Record<LanguageType, string> = {
  [LanguageType.CSharp]: 'mdi-language-csharp',
  [LanguageType.Java]: 'mdi-language-java',
  [LanguageType.JavaScript]: 'mdi-language-javascript',
  [LanguageType.TypeScript]: 'mdi-language-typescript',
  [LanguageType.SQL]: 'mdi-database-search',
  [LanguageType.JSON]: 'mdi-code-json',
}

// language file icon color
export const LANGUAGE_FILE_ICON_COLOR_MAPPING: Record<LanguageType, string> = {
  [LanguageType.CSharp]: '#68217A',
  [LanguageType.Java]: '#6699FF',
  [LanguageType.JavaScript]: '#F5DD1E',
  [LanguageType.TypeScript]: '#0288D1',
  [LanguageType.SQL]: '#1989fa',
  [LanguageType.JSON]: '#00A000',
}
