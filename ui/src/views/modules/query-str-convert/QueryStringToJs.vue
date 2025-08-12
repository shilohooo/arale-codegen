<!--
  * Convert URL Query String to JS object literal
  * @author shiloh
  * @date 2025/3/25 22:12
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center q-gutter-x-md justify-between q-mb-md">
        <div class="col">Query String</div>
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
      <q-input
        v-model="srcCode"
        name="srcCode"
        filled
        type="textarea"
        placeholder="Paste your query string here, or enter something like a=1&b=2..."
        @update:model-value="handleSrcCodeChange"
      />
    </div>
    <!--endregion-->

    <q-separator vertical />

    <!--region target-->
    <div class="col">
      <div class="row items-center justify-between q-mb-md">
        <div class="col">JavaScript</div>
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
        ref="targetCodeEditor"
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
import { TEST_QUERY_STR } from 'src/constant/data/query-str-example'
import qs from 'qs'
import { stringify } from 'javascript-stringify'
import type { EditorModel } from 'src/types/code-editor'
import { createEditorModel } from 'src/utils'
import { LanguageType } from 'src/enums'
import type { Uri } from 'monaco-editor'

const srcCode = ref<string>(TEST_QUERY_STR)
const targetModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JavaScript,
    code: '',
    fileName: 'Target.js',
  }),
)

const targetCodeEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/3/25 22:12
 */
function handleClearSrcCode() {
  srcCode.value = ''
  targetModel.value.value = ''
  targetCodeEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
}

/**
 * Source code changed callback - regenerate target code after 300ms
 * @author shiloh
 * @date 2025/3/25 22:12
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 300)

const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025/3/25 22:12
 */
async function handleGenerateTargetCode() {
  if (!srcCode.value) {
    targetModel.value.value = ''
    targetCodeEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
    return
  }

  const jsonObj = JSON.parse(JSON.stringify(qs.parse(srcCode.value)))
  try {
    const result = stringify(jsonObj, null, 2)!
    targetModel.value.value = `const targetObject = ${result}`
    targetCodeEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
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
  handleGenerateTargetCode()
})

// endregion
</script>

<style scoped></style>
