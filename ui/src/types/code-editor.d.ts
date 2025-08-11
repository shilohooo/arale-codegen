/**
 * Code Editor type definition
 * @author shiloh
 * @date 2025/2/4 17:36
 */
import type { LanguageType } from 'src/enums'
import type { Uri } from 'monaco-editor'

// editor model
export interface EditorModel {
  fileName: string
  uri: Uri
  languageType: LanguageType
  value: string
  iconName?: string
  iconColor?: string
}
