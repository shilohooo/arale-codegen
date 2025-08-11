<!--
  * Convert JSON to Class
  * @author shiloh
  * @date 2025/2/24 22:57
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center q-gutter-x-md justify-between q-mb-md">
        <div class="col">JSON</div>
        <div class="col text-right">
          <q-btn
            :disable="!reqData.code"
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
        <div class="col-lg-10 col-md-8 col-sm-6">
          <q-select
            v-model="reqData.targetType"
            name="targetType"
            :options="JSON_TO_ENTITY_CODE_TARGET_TYPE_OPTIONS"
            @update:model-value="handleGenerateTargetCode"
            label="Convert target type"
            map-options
            emit-value
          />
        </div>
        <div class="col text-right">
          <q-btn
            :disable="!currentTargetCode"
            icon="content_copy"
            size="sm"
            color="primary"
            label="Copy"
            no-caps
            @click="copy(currentTargetCode)"
          />
        </div>
      </div>
      <code-editor
        ref="targetCodeEditor"
        :editor-models="targetModels"
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
import { LanguageType, TargetType } from 'src/enums'
import type { CodeGenerateReq } from 'src/api/models/code-generate-models'
import { JSON_TO_ENTITY_CODE_TARGET_TYPE_OPTIONS } from 'src/constant'
import { generateCodeByJson } from 'src/api/code-generate-api'
import { createEditorModel } from 'src/utils'
import type { EditorModel } from 'src/types/code-editor'
import type { Uri } from 'monaco-editor'

const reqData = ref<CodeGenerateReq>({
  code: JSON_OBJECT_TEST_STR,
  targetType: TargetType.SpringDataMongoDBEntity,
})
const srcModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.JSON,
    code: JSON_OBJECT_TEST_STR,
    fileName: 'Source.json',
  }),
)
const targetModels = ref<EditorModel[]>([])
const currentTargetCode = ref<string>('')

const srcModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
function handleClearSrcCode() {
  reqData.value.code = ''
  srcModel.value.value = ''
  srcModelEditor.value?.changeModelCode(srcModel.value.uri, srcModel.value.value)

  targetModels.value = []
  currentTargetCode.value = ''
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
  reqData.value.code = code ?? ''
  await handleGenerateTargetCode()
}, 300)

const targetCodeEditor = ref<InstanceType<typeof CodeEditor> | null>(null)
const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025/2/24 23:00
 */
async function handleGenerateTargetCode() {
  if (!reqData.value.code) {
    targetModels.value = []
    currentTargetCode.value = ''
    return
  }

  try {
    const res = await generateCodeByJson(reqData.value)
    res.forEach((item) => {
      item.code = item.code?.replace(/\\n/g, '\n').trim()
    })
    const isFirstTime = !targetModels.value?.length
    targetModels.value = res.map(createEditorModel)
    if (isFirstTime) {
      return
    }

    targetModels.value.forEach((targetModel) =>
      targetCodeEditor.value?.changeModelCode(targetModel.uri, targetModel.value),
    )
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
 * @param uri model uri
 * @param code model code
 * @author shiloh
 * @date 2025/2/16 10:31
 */
const handleTargetCodeChange = debounce(async (uri: Uri, code: string | undefined) => {
  if (!code) {
    return
  }

  const model = targetModels.value.find((item) => item.uri.toString() === uri.toString())
  if (model) {
    model.value = code
    currentTargetCode.value = code
  }
}, 300)

// region copy

const { copy } = useClipboard()

// endregion

// region mounted

onMounted(() => {
  reqData.value.code = JSON_OBJECT_TEST_STR
})

// endregion
</script>

<style scoped></style>
