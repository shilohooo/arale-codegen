/**
 * Code Editor type definition
 * @author shiloh
 * @date 2025/2/4 17:36
 */

// out-of-the-box themes
export const MONACO_EDITOR_THEMES = ['vs', 'vs-dark', 'hc-black', 'hc-light'] as const
export type EditorTheme = (typeof MONACO_EDITOR_THEMES)[number]

// supported languages
export const MONACO_EDITOR_SUPPORTED_LANGUAGES = [
  'javascript',
  'typescript',
  'java',
  'sql',
  'json',
  'csharp',
] as const
export type EditorLanguage = (typeof MONACO_EDITOR_SUPPORTED_LANGUAGES)[number]
