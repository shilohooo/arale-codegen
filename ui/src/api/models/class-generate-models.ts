/**
 * Api request & response models
 * @author shiloh
 * @date 2025/2/5 10:16
 */
import type { TargetType } from 'src/enums';
import { type DbType } from 'src/enums'

// class code generate request params
export interface ClassCodeGenerateReq {
  code: string
  dbType: DbType
  targetType: TargetType
  tableNamePrefix: string
}
