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
            :disable="!srcModel.value"
            icon="delete"
            size="sm"
            color="negative"
            label="Clear"
            no-caps
            @click="handleClearSrcCode"
          />
        </div>
      </div>
      <code-editor ref="srcModelEditor" :editor-models="[srcModel]" @change="handleSrcCodeChange" />
    </div>
    <!--endregion-->

    <q-separator vertical />

    <!--region target-->
    <div class="col">
      <div class="row items-center justify-between q-mb-md q-gutter-x-md">
        <div class="col">JSON</div>
        <div class="col">
          <q-select
            v-model="caseType"
            name="caseType"
            :options="JSON_PROPERTY_CASE_TYPE_OPTIONS"
            @update:model-value="handleGenerateTargetCode"
            label="Case type"
            map-options
            emit-value
          />
        </div>
        <div class="col text-right">
          <q-btn
            :disable="!targetModel.value"
            icon="content_copy"
            size="sm"
            color="primary"
            label="Copy"
            no-caps
            @click="copy(targetModel.value)"
          />
        </div>
      </div>
      <code-editor ref="targetModelEditor" :editor-models="[targetModel]" />
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
import { convertJsonPropertyCase, createEditorModel } from 'src/utils'
import { JsonPropertyCaseType, LanguageType } from 'src/enums'
import type { EditorModel } from 'src/types/code-editor'
import type { Uri } from 'monaco-editor'

const srcModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JSON,
    code: JSON_OBJECT_TEST_STR,
    fileName: 'Source.json',
  }),
)
const targetModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JSON,
    code: '',
    fileName: 'Target.json',
  }),
)
const caseType = ref(JsonPropertyCaseType.PascalCase)

const srcModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)
const targetModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
function handleClearSrcCode() {
  srcModel.value.value = ''
  srcModelEditor.value?.changeModelCode(srcModel.value.uri, srcModel.value.value)
  targetModel.value.value = ''
  targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
}

/**
 * Source code change callback - regenerate target code after 300ms
 * @param _uri model uri
 * @param code model code
 * @author shiloh
 * @date 2025/2/16 10:31
 */
const handleSrcCodeChange = debounce(async (_uri: Uri, code: string | undefined) => {
  srcModel.value.value = code ?? ''
  await handleGenerateTargetCode()
}, 300)

const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
async function handleGenerateTargetCode() {
  if (!srcModel.value.value) {
    targetModel.value.value = ''
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
    return
  }

  try {
    targetModel.value.value = convertJsonPropertyCase(srcModel.value.value, caseType.value)
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
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
</script>

<style scoped></style>
