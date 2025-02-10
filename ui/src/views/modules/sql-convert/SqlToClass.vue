<!--
  * sql to class
  * @author shiloh
  * @date 2025/2/4 21:53
-->
<template>
  <sql-code-converter :source-code="code" :target-type-options="CLASS_CODE_TARGET_TYPE_OPTIONS" />
</template>

<script setup lang="ts">
import SqlCodeConverter from 'components/SqlCodeConverter.vue'
import { SQL_SERVER_TEST_DDL } from 'src/constant/data/sql-ddl-examples'
import { CLASS_CODE_TARGET_TYPE_OPTIONS } from 'src/constant'

const code = ref<string>(SQL_SERVER_TEST_DDL)
</script>

<style scoped></style>
