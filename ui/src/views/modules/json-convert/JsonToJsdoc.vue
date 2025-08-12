<!--
  * Convert JSON to JS object literal
  * @author shiloh
  * @date 2025/3/6 11:02
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center q-gutter-x-md justify-between q-mb-md">
        <div class="col">JSON</div>
        <div class="col text-right">
          <q-btn
            :disable="!srcModel"
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
        <div class="col">JsDoc</div>
        <div class="col">
          <q-input
            v-model="rootTypeName"
            name="rootTypeName"
            label="Root type name"
            placeholder="Input the name of root type..."
            @update:model-value="handleSrcCodeChange(srcModel.uri, srcModel.value)"
          />
        </div>
        <div class="col">
          <q-input
            v-model="typesPrefix"
            name="typesPrefix"
            label="Types prefix"
            placeholder="Input the types prefix..."
            @update:model-value="handleSrcCodeChange(srcModel.uri, srcModel.value)"
          />
        </div>
        <div class="col">
          <q-input
            v-model="typesSuffix"
            name="typesSuffix"
            label="Types suffix"
            placeholder="Input the types suffix..."
            @update:model-value="handleSrcCodeChange(srcModel.uri, srcModel.value)"
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
import { JSON_OBJECT_TEST_STR } from 'src/constant/data/json-examples'
import { jsonToJSDoc } from 'json-to-jsdoc'
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
const targetModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JavaScript,
    code: '',
    fileName: 'Target.js',
  }),
)

const srcModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)
const targetModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/3/6 11:02
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
const rootTypeName = ref('Root')
const typesPrefix = ref('')
const typesSuffix = ref('')

/**
 * Generate target code
 * @author shiloh
 * @date 2025/3/6 11:02
 */
async function handleGenerateTargetCode() {
  if (!srcModel.value.value) {
    targetModel.value.value = ''
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
    return
  }

  try {
    targetModel.value.value = jsonToJSDoc(srcModel.value.value, {
      rootTypeName: rootTypeName.value,
      typePrefix: typesPrefix.value,
      typeSuffix: typesSuffix.value,
    })
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
</script>

<style scoped></style>
