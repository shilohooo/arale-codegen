/**
 * Class code generate api
 * @author shiloh
 * @date 2025/2/5 10:30
 */
import type { CodeGenerateReq, CodeGenerateResp } from 'src/api/models/code-generate-models'
import httpClient from 'src/utils/http'

// api base path
export const BASE_PATH = '/CodeGenerate'

// api path
export const CODE_GENERATE_API = {
  // generate by sql
  generateBySql: `${BASE_PATH}/GenerateBySql`,
  // generate by json
  generateByJson: `${BASE_PATH}/GenerateByJson`,
}

/**
 * Generate code by sql
 * @param data request params
 * @author shiloh
 * @date 2025/2/5 10:33
 */
export const generateCodeBySql = (data: CodeGenerateReq) => {
  return httpClient.post<CodeGenerateResp[]>({ data, url: CODE_GENERATE_API.generateBySql })
}

/**
 * Generate code by json
 * @param data request params
 * @author shiloh
 * @date 2025/2/24 22:46
 */
export const generateCodeByJson = (data: CodeGenerateReq) => {
  return httpClient.post<CodeGenerateResp[]>({ data, url: CODE_GENERATE_API.generateByJson })
}
