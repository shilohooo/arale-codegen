/**
 * type definition of https://github.com/MIRTAHAALI/json-case-convertor
 * @author shiloh
 * @date 2025/3/16 11:59
 */
declare module 'json-case-convertor' {
  export function lowerCaseKeys<T>(obj: T): T

  export function upperCaseKeys<T>(obj: T): T

  export function camelCaseKeys<T>(obj: T): T

  export function pascalCaseKeys<T>(obj: T): T

  export function snakeCaseKeys<T>(obj: T): T

  export function kebabCaseKeys<T>(obj: T): T
}
