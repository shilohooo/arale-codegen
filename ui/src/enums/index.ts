/**
 * enums
 * @author shiloh
 * @date 2025/2/5 10:26
 */

// supported database type
export enum DbType {
  MySQL = 1,
  SQLServer = 2,
  PostgreSQL = 3,
  SQLite = 4,
}

// generate target type
export enum TargetType {
  CSharpClass = 1,
  CSharpRecord = 2,
  CSharpStruct = 3,
  SqlSugarEntity = 4,
  EFCoreEntity = 5,
  JavaClass = 6,
  JavaRecord = 7,
  MyBatisPlusEntity = 8,
  HibernateEntity = 9,
}
