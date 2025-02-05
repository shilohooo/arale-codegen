<!--
  * Code convert component
  * @author shiloh
  * @date 2025/2/5 9:01
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center justify-between">
        <div class="col-lg-10 col-md-8 col-sm-6">
          <q-select
            v-model="dbType"
            :options="DB_TYPE_OPTIONS"
            @change="handleSrcCodeChange"
            label="DatabaseType"
            map-options
          />
        </div>
        <div class="col text-right">
          <q-btn
            :disable="!srcCode"
            icon="delete"
            size="sm"
            color="negative"
            label="Clear"
            no-caps
            @click="handleClearSrcCode"
          />
        </div>
      </div>
      <q-separator spaced />
      <code-editor v-model="srcCode" language="sql" @change="handleSrcCodeChange" />
    </div>
    <!--endregion-->

    <q-separator vertical />

    <!--region target-->
    <div class="col">
      <div class="row items-center justify-between">
        <div class="col text-h6">{{ targetLanguage.toUpperCase() }}</div>
        <q-btn
          :disable="!targetCode"
          icon="content_copy"
          size="sm"
          color="primary"
          label="Copy"
          no-caps
          @click="handleCopyTargetCode"
        />
      </div>
      <q-separator spaced />
      <code-editor v-model="targetCode" :language="targetLanguage" />
    </div>
    <!--endregion-->
  </div>
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'
import type { EditorLanguage } from 'src/types/code-editor'
import { debounce, useQuasar } from 'quasar'
import { generateClassCodeBySql } from 'src/api/class-generate-api'
import { DB_TYPE_OPTIONS } from 'src/constant'

const props = defineProps<{
  sourceCode: string
  targetLanguage: EditorLanguage
}>()

const dbType = ref(2)
const srcCode = ref<string>(props.sourceCode)
const targetCode = ref<string>('')

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/5 11:13
 */
function handleClearSrcCode() {
  srcCode.value = ''
  targetCode.value = ''
}

/**
 * Source code changed callback - regenerate target code after 1s
 * @author shiloh
 * @date 2025/2/5 10:37
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 500)

/**
 * Generate target code
 * @author shiloh
 * @date 2025/2/5 10:37
 */
async function handleGenerateTargetCode() {
  if (!srcCode.value) {
    return
  }

  const res = await generateClassCodeBySql({
    code: srcCode.value,
    dbType: dbType.value,
    targetType: 1,
    tableNamePrefix: 'T_',
  })
  targetCode.value = res.data
}

// region copy

const $q = useQuasar()

/**
 * Copy target code
 * @author shiloh
 * @date 2025/2/5 10:11
 */
function handleCopyTargetCode() {
  navigator.clipboard.writeText(targetCode.value)
  $q.notify({
    message: 'Copied:)',
    color: 'positive',
    position: 'top',
  })
}

// endregion

// region mounted

onMounted(async () => {
  await handleGenerateTargetCode()
})

// endregion
</script>

<style scoped></style>
