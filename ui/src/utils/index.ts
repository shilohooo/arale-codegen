/**
 * Utility functions for code generation.
 * @author shiloh
 * @date 2025/3/16 11:43
 */

import {
  lowerCaseKeys,
  upperCaseKeys,
  camelCaseKeys,
  pascalCaseKeys,
  snakeCaseKeys,
  kebabCaseKeys,
} from 'json-case-convertor'
import { JsonPropertyCaseType } from 'src/enums'

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
