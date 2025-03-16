<!--
  * JSON property case converter
  * @author shiloh
  * @date 2025/3/16 11:42
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center q-gutter-x-md justify-between q-mb-md">
        <div class="col">JSON</div>
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
      <code-editor v-model="srcCode" language="json" @change="handleSrcCodeChange" />
    </div>
    <!--endregion-->

    <q-separator vertical />

    <!--region target-->
    <div class="col">
      <div class="row items-center justify-between q-mb-md">
        <div class="col">JSON</div>
        <div class="col-lg-10 col-md-8 col-sm-6">
          <q-select
            v-model="caseType"
            :options="JSON_PROPERTY_CASE_TYPE_OPTIONS"
            @update:model-value="handleGenerateTargetCode"
            label="Case type"
            map-options
            emit-value
          />
        </div>
        <div class="col text-right">
          <q-btn
            :disable="!targetCode"
            icon="content_copy"
            size="sm"
            color="primary"
            label="Copy"
            no-caps
            @click="copy(targetCode)"
          />
        </div>
      </div>
      <code-editor v-model="targetCode" language="json" />
    </div>
    <!--endregion-->
  </div>
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'
import { debounce, useQuasar } from 'quasar'
import { useClipboard } from 'src/hooks/useClipboard'
import { JSON_OBJECT_TEST_STR } from 'src/constant/data/json-examples'
import { JSON_PROPERTY_CASE_TYPE_OPTIONS } from 'src/constant'
import { convertJsonPropertyCase } from 'src/utils'
import { JsonPropertyCaseType } from 'src/enums'

const srcCode = ref('')
const targetCode = ref<string>('')
const caseType = ref(JsonPropertyCaseType.PascalCase)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
function handleClearSrcCode() {
  srcCode.value = ''
  targetCode.value = ''
}

/**
 * Source code changed callback - regenerate target code after 1s
 * @author shiloh
 * @date 2025/2/24 23:00
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 500)

const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
async function handleGenerateTargetCode() {
  if (!srcCode.value) {
    return
  }

  try {
    targetCode.value = convertJsonPropertyCase(srcCode.value, caseType.value)
  } catch (e) {
    $q.notify({
      color: 'negative',
      position: 'top',
      icon: 'error',
      message: (e as Error).message,
    })
    console.error(e)
  }
}

// region copy

const { copy } = useClipboard()

// endregion

// region mounted

onMounted(() => {
  srcCode.value = JSON_OBJECT_TEST_STR
})

// endregion
</script>

<style scoped></style>
