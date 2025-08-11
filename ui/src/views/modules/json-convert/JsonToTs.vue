<!--
  * Convert JSON to TS
  * @author shiloh
  * @date 2025/2/16 10:31
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
      <div class="row items-center justify-between q-mb-md">
        <div class="col">TypeScript</div>
        <div class="col">
          <q-toggle
            v-model="useTypeAlias"
            label="Use type alias"
            name="useTypeAlias"
            :true-value="true"
            :false-value="false"
            @update:model-value="handleGenerateTargetCode"
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
      <code-editor
        ref="targetModelEditor"
        :editor-models="[targetModel]"
        @change="handleTargetCodeChange"
      />
    </div>
    <!--endregion-->
  </div>
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'
import { debounce, useQuasar } from 'quasar'
import { useClipboard } from 'src/hooks/useClipboard'
import JsonToTS from 'json-to-ts'
import { JSON_OBJECT_TEST_STR } from 'src/constant/data/json-examples'
import type { EditorModel } from 'src/types/code-editor'
import { createEditorModel } from 'src/utils'
import { LanguageType } from 'src/enums'
import type { Uri } from 'monaco-editor'

const srcModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JSON,
    code: JSON_OBJECT_TEST_STR,
    fileName: 'Source.json',
  }),
)
const useTypeAlias = ref<boolean>(false)
const targetModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.TypeScript,
    code: '',
    fileName: 'Target.ts',
  }),
)

const srcModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)
const targetModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/16 10:31
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
 * @date 2025/2/16 10:31
 */
async function handleGenerateTargetCode() {
  if (!srcModel.value.value) {
    targetModel.value.value = ''
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
    return
  }

  try {
    const typeInterfaces = JsonToTS(JSON.parse(srcModel.value.value), {
      rootName: 'Root',
      useTypeAlias: useTypeAlias.value,
    })
    let code = ''
    typeInterfaces.forEach((typeInterface, index) => {
      if (index < typeInterfaces.length - 1) {
        code += `${typeInterface}\n\n`
      } else {
        code += `${typeInterface}\n`
      }
    })
    targetModel.value.value = code
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, code)
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

/**
 * Target code change callback
 * @param _uri model uri
 * @param code model code
 * @author shiloh
 * @date 2025/2/16 10:31
 */
const handleTargetCodeChange = debounce(async (_uri: Uri, code: string | undefined) => {
  if (!code) {
    return
  }

  targetModel.value.value = code
}, 300)

// region copy

const { copy } = useClipboard()

// endregion

// region mounted

onMounted(() => {
  srcModel.value.value = JSON_OBJECT_TEST_STR
})

// endregion
</script>

<style scoped></style>
