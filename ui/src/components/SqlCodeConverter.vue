<!--
  * SQL DDL Code convert component
  * @author shiloh
  * @date 2025/2/5 9:01
-->
<template>
  <div class="row q-gutter-x-md">
    <!--region source-->
    <div class="col">
      <div class="row items-center q-gutter-x-md justify-between q-mb-md">
        <div class="col">
          <q-select
            v-model="reqData.dbType"
            name="dbType"
            :options="DB_TYPE_OPTIONS"
            @update:model-value="handleGenerateTargetCode"
            label="Database type"
            map-options
            emit-value
          />
        </div>
        <div class="col">
          <q-input
            v-model.trim="reqData.tableNamePrefix"
            name="tableNamePrefix"
            placeholder="Input the table name prefix..."
            label="Table name prefix"
            @update:model-value="handleTableNamePrefixChange"
          />
        </div>
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
            :options="targetTypeOptions"
            name="targetType"
            @update:model-value="handleGenerateTargetCode"
            label="Convert target type"
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
import type { QSelectOption } from 'quasar'
import { debounce } from 'quasar'
import { generateCodeBySql } from 'src/api/code-generate-api'
import { DB_TYPE_OPTIONS } from 'src/constant'
import { DbType, LanguageType, type TargetType } from 'src/enums'
import type { CodeGenerateReq } from 'src/api/models/code-generate-models'
import { useClipboard } from 'src/hooks/useClipboard'
import { createEditorModel } from 'src/utils'
import type { EditorModel } from 'src/types/code-editor'
import type { Uri } from 'monaco-editor'

const props = withDefaults(
  defineProps<{
    sourceCode: string
    targetTypeOptions: QSelectOption<TargetType>[]
    defaultSelectedTargetType: TargetType
  }>(),
  {
    sourceCode: () => '',
    targetTypeOptions: () => [],
  },
)

const reqData = ref<CodeGenerateReq>({
  code: props.sourceCode,
  dbType: DbType.SQLServer,
  targetType: props.defaultSelectedTargetType,
  tableNamePrefix: 'T_',
})
const srcModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.SQL,
    code: props.sourceCode,
    fileName: 'Source.sql',
  }),
)
const targetModel = ref<EditorModel>(
  createEditorModel({
    language: LanguageType.CSharp,
    code: '',
    fileName: 'Target.cs',
  }),
)

const srcModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)
const targetModelEditor = ref<InstanceType<typeof CodeEditor> | null>(null)

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/5 11:13
 */
function handleClearSrcCode() {
  reqData.value.code = ''
  srcModel.value.value = ''
  srcModelEditor.value?.changeModelCode(srcModel.value.uri, srcModel.value.value)

  targetModel.value.value = ''
  targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
}

/**
 * Source code changed callback - regenerate target code after 300s
 * @param _uri model uri
 * @param code model code
 * @author shiloh
 * @date 2025/2/5 10:37
 */
const handleSrcCodeChange = debounce(async (_uri: Uri, code: string | undefined) => {
  srcModel.value.value = code ?? ''
  reqData.value.code = code ?? ''
  await handleGenerateTargetCode()
}, 300)

/**
 * Table name prefix changed callback - regenerate target code after 300ms
 * @author shiloh
 * @date 2025/8/2 22:01
 */
const handleTableNamePrefixChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 300)

/**
 * Generate target code
 * @author shiloh
 * @date 2025/2/5 10:37
 */
async function handleGenerateTargetCode() {
  if (!reqData.value.code) {
    targetModel.value.value = ''
    targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
    return
  }

  const res = await generateCodeBySql(reqData.value)
  if (!res.length) {
    targetModel.value.value = ''
    return
  }
  targetModel.value = createEditorModel(res[0]!)
  targetModelEditor.value?.changeModelCode(targetModel.value.uri, targetModel.value.value)
}

/**
 * Target code change callback
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
