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

// const code = ref<string>(
//   'create table T_Users(Id bigint primary key, Username varchar(50) not null, Address nvarchar(255) not null, Gender char(1), Enabled bit(1))',
// )
const code = ref<string>(
  'create table T_Users(Id bigint primary key, Username varchar(50) not null, Address nvarchar(255) not null, Gender char(1), Enabled bit(1))',
)
</script>

<style scoped></style>
