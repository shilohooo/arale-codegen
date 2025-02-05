<!--
  * 顶部
  * @author shiloh
  * @date 2024/7/16 17:14
-->
<template>
  <q-header elevated class="bg-primary text-white" height-hint="98">
    <q-toolbar>
      <q-btn dense flat round class="q-mr-md" icon="menu" @click="sidebarStore.toggleCollapsed()" />
      <breadcrumb-view />

      <q-toolbar-title />

      <menu-search-box />

      <div class="row q-ml-md q-gutter-x-sm">
        <a href="https://github.com/shilohooo/arale-codegen" target="_blank">
          <q-icon name="ion-logo-github" color="white" size="1.5rem" />
        </a>
        <a href="https://github.com/shilohooo/arale-codegen/issues" target="_blank">
          <q-icon name="bug_report" color="white" size="1.5rem" />
        </a>
      </div>
    </q-toolbar>
  </q-header>
</template>

<script setup lang="ts">
import { useSidebarStore } from 'stores/sidebar'
import BreadcrumbView from 'layouts/components/BreadcrumbView.vue'
import MenuSearchBox from 'components/MenuSearchBox.vue'

const sidebarStore = useSidebarStore()

defineOptions({ name: 'HeaderView' })
</script>

<style scoped></style>
