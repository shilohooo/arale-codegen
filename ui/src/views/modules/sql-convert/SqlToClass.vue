<!--
  * sql to class
  * @author shiloh
  * @date 2025/2/4 21:53
-->
<template>
  <code-converter :source-code="code" />
</template>

<script setup lang="ts">
import CodeConverter from 'components/CodeConverter.vue'
import { SQL_SERVER_TEST_DDL } from 'src/constant/data/sql-ddl-examples'

const code = ref<string>(SQL_SERVER_TEST_DDL)
</script>

<style scoped></style>
