<!--
  * sql to class
  * @author shiloh
  * @date 2025/2/4 21:53
-->
<template>
  <CodeEditor v-model="code" language="sql" />
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'

const code = ref<string>(
  'create table T_Users(id bigint primary key, username varchar(50) not null, address nvarchar(255) not null, gender char(1), enabled bit(1))',
)
</script>

<style scoped></style>
