<!--
  * sql to class
  * @author shiloh
  * @date 2025/2/4 21:53
-->
<template>
  <sql-code-converter
    :source-code="code"
    :target-type-options="ENTITY_CODE_TARGET_TYPE_OPTIONS"
    :default-selected-target-type="TargetType.SqlSugarEntity"
  />
</template>

<script setup lang="ts">
import SqlCodeConverter from 'components/SqlCodeConverter.vue'
import { SQL_SERVER_TEST_DDL } from 'src/constant/data/sql-ddl-examples'
import { ENTITY_CODE_TARGET_TYPE_OPTIONS } from 'src/constant'
import { TargetType } from 'src/enums'

const code = ref<string>(SQL_SERVER_TEST_DDL)
</script>

<style scoped></style>
