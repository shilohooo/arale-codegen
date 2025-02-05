/**
 * constants
 * @author shiloh
 * @date 2025/2/5 11:18
 */
import type { QSelectOption } from 'quasar'
import { DbType, TargetType } from 'src/enums'
import type { EditorLanguage } from 'src/types/code-editor'

// supported database type options
export const DB_TYPE_OPTIONS: QSelectOption<DbType>[] = [
  { label: DbType[DbType.MySQL], value: DbType.MySQL },
  { label: DbType[DbType.PostgreSQL], value: DbType.PostgreSQL },
  { label: DbType[DbType.SQLServer], value: DbType.SQLServer },
  { label: DbType[DbType.SQLite], value: DbType.SQLite },
]

// supported target type options
export const TARGET_TYPE_OPTIONS: QSelectOption<TargetType>[] = [
  { label: TargetType[TargetType.CSharpClass], value: TargetType.CSharpClass },
  { label: TargetType[TargetType.JavaClass], value: TargetType.JavaClass },
]

// target type & editor language mapping
export const TARGET_TYPE_LANGUAGE_MAPPING: Record<TargetType, EditorLanguage> = {
  [TargetType.CSharpClass]: 'csharp',
  [TargetType.JavaClass]: 'java',
}
