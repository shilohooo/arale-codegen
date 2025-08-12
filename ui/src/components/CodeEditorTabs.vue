<!--
  * Editor tabs
  * @author shiloh
  * @date 2025/7/30 10:13
-->
<template>
  <div class="tabs-wrapper">
    <q-tabs
      v-model="activeTab"
      v-for="(tab, index) in tabs"
      :key="tab.uri.toString()"
      dense
      indicator-color="primary"
      active-color="dark"
    >
      <q-tab :name="index" no-caps class="tab">
        <template #default>
          <div class="flex justify-center items-center q-gutter-sm q-py-xs">
            <q-icon
              v-if="tab.iconName"
              :name="tab.iconName"
              size="1.25rem"
              :style="{ color: tab.iconColor }"
            />
            <span :class="{ 'text-grey-6': activeTab !== index }">{{ tab.fileName }}</span>
          </div>
        </template>
      </q-tab>
    </q-tabs>
  </div>
</template>

<script setup lang="ts">
import type { EditorModel } from 'src/types/code-editor'

defineProps<{ tabs: EditorModel[] }>()

const activeTab = defineModel<number>({
  default: 0,
})
</script>

<style scoped>
.tabs-wrapper {
  display: flex;
  border-bottom: 1px solid #d1d5dc;
  margin-bottom: 16px;
}

.tab {
  border-right: 1px solid #d1d5dc;
}
</style>
