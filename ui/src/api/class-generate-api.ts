/**
 * Class code generate api
 * @author shiloh
 * @date 2025/2/5 10:30
 */
import type { ClassCodeGenerateReq } from 'src/api/models/class-generate-models'
import { api } from 'boot/axios'

// api base path
export const BASE_PATH = '/ClassCodeGenerate'

// api path
export const CLASS_CODE_GENERATE_API = {
  // generate by sql
  generateBySql: `${BASE_PATH}/GenerateBySql`,
}

/**
 * Generate class code by sql
 * @param data request params
 * @author shiloh
 * @date 2025/2/5 10:33
 */
export const generateClassCodeBySql = (data: ClassCodeGenerateReq) => {
  return api.post<string>(CLASS_CODE_GENERATE_API.generateBySql, data)
}
