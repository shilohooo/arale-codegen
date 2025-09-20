/**
 * Utility functions for code generation.
 * @author shiloh
 * @date 2025/3/16 11:43
 */

import {
  camelCaseKeys,
  kebabCaseKeys,
  lowerCaseKeys,
  pascalCaseKeys,
  snakeCaseKeys,
  upperCaseKeys,
} from 'json-case-convertor'
import { JsonPropertyCaseType } from 'src/enums'
import type { CodeGenerateResp } from 'src/api/models/code-generate-models'
import { LANGUAGE_FILE_ICON_COLOR_MAPPING, LANGUAGE_FILE_ICON_NAME_MAPPING } from 'src/constant'
import type { EditorModel } from 'src/types/code-editor'
import { Uri } from 'monaco-editor'

/**
 * convert json property case
 * @author shiloh
 * @date 2025/3/16 11:43
 */
export function convertJsonPropertyCase(jsonStr: string, jsonPropertyCase: JsonPropertyCaseType) {
  switch (jsonPropertyCase) {
    case JsonPropertyCaseType.LowerCase:
      return JSON.stringify(lowerCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.UpperCase:
      return JSON.stringify(upperCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.CamelCase:
      return JSON.stringify(camelCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.PascalCase:
      return JSON.stringify(pascalCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.SnakeCase:
      return JSON.stringify(snakeCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.KebabCase:
      return JSON.stringify(kebabCaseKeys(JSON.parse(jsonStr)), null, 2)
    case JsonPropertyCaseType.SnakeCaseUpper:
      return JSON.stringify(upperCaseKeys(snakeCaseKeys(JSON.parse(jsonStr))), null, 2)
    case JsonPropertyCaseType.KebabCaseUpper:
      return JSON.stringify(upperCaseKeys(kebabCaseKeys(JSON.parse(jsonStr))), null, 2)
    default:
      return jsonStr
  }
}

/**
 * Get fileName without extension
 * @param fileName
 * @author shiloh
 * @date 2025/8/11 22:18
 */
export function getFileNameWithoutExtension(fileName?: string) {
  if (!fileName) {
    return ''
  }

  return fileName.substring(0, fileName.lastIndexOf('.'))
}

/**
 * Get file extension from fileName
 * @param fileName
 * @author shiloh
 * @date 2025/8/11 22:20
 */
export function getFileExtension(fileName?: string) {
  if (!fileName) {
    return ''
  }

  return fileName.substring(fileName.lastIndexOf('.') + 1)
}

/**
 * Create editor model
 * @param codeGenerateResp Code generate response
 * @author shiloh
 * @date 2025/8/2 22:07
 */
export function createEditorModel(codeGenerateResp: CodeGenerateResp): EditorModel {
  const fileName = getFileNameWithoutExtension(codeGenerateResp.fileName)
  const fileExtension = getFileExtension(codeGenerateResp.fileName)
  const uri = Uri.file(`${fileName}.${fileExtension}`)
  return {
    uri,
    fileName: codeGenerateResp.fileName,
    languageType: codeGenerateResp.language,
    value: codeGenerateResp.code,
    iconName: LANGUAGE_FILE_ICON_NAME_MAPPING[codeGenerateResp.language],
    iconColor: LANGUAGE_FILE_ICON_COLOR_MAPPING[codeGenerateResp.language],
  }
}
