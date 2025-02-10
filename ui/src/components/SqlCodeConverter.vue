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
            placeholder="Input the table name prefix..."
            label="Table name prefix"
            @update:model-value="handleSrcCodeChange"
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
      <code-editor v-model="reqData.code" language="sql" @change="handleSrcCodeChange" />
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
            @update:model-value="handleGenerateTargetCode"
            label="Convert target type"
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
            @click="handleCopyTargetCode"
          />
        </div>
      </div>
      <code-editor v-model="targetCode" :language="targetLanguage" />
    </div>
    <!--endregion-->
  </div>
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'
import type { QSelectOption } from 'quasar'
import { debounce, useQuasar } from 'quasar'
import { generateClassCodeBySql } from 'src/api/class-generate-api'
import { DB_TYPE_OPTIONS, TARGET_TYPE_LANGUAGE_MAPPING } from 'src/constant'
import { DbType, TargetType } from 'src/enums'
import type { ClassCodeGenerateReq } from 'src/api/models/class-generate-models'

const props = withDefaults(
  defineProps<{ sourceCode: string; targetTypeOptions: QSelectOption<TargetType>[] }>(),
  {
    sourceCode: () => '',
    targetTypeOptions: () => [],
  },
)

const targetCode = ref<string>('')
const reqData = ref<ClassCodeGenerateReq>({
  code: props.sourceCode,
  dbType: DbType.SQLServer,
  targetType: TargetType.CSharpClass,
  tableNamePrefix: 'T_',
})
const targetLanguage = computed(() => TARGET_TYPE_LANGUAGE_MAPPING[reqData.value.targetType])

/**
 * Clear source code
 * @author shiloh
 * @date 2025/2/5 11:13
 */
function handleClearSrcCode() {
  reqData.value.code = ''
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
  if (!reqData.value.code) {
    return
  }

  const res = await generateClassCodeBySql(reqData.value)
  targetCode.value = res.replace(/\\n/g, '\n').trim()
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
</script>

<style scoped></style>
