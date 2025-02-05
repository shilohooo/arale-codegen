<!--
  * sql to class
  * @author shiloh
  * @date 2025/2/4 21:53
-->
<template>
  <code-converter :source-code="code" target-language="csharp" />
</template>

<script setup lang="ts">
import CodeConverter from 'components/CodeConverter.vue'

const code = ref<string>(
  'create table T_Users(id bigint primary key, username varchar(50) not null, address nvarchar(255) not null, gender char(1), enabled bit(1))',
)
</script>

<style scoped></style>
