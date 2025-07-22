<!--
  * Convert JSON to Entity
  * @author shiloh
  * @date 2025-07-22 16:34
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
      <code-editor v-model="reqData.code" language="json" @change="handleSrcCodeChange" />
    </div>
    <!--endregion-->

    <q-separator vertical />

    <!--region target-->
    <div class="col">
      <div class="row items-center justify-between q-mb-md">
        <div class="col-lg-10 col-md-8 col-sm-6">
          <q-select
            v-model="reqData.targetType"
            :options="JSON_TO_ENTITY_CODE_TARGET_TYPE_OPTIONS"
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
            @click="copy(targetCode)"
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
import { debounce, useQuasar } from 'quasar'
import { useClipboard } from 'src/hooks/useClipboard'
import { JSON_OBJECT_TEST_STR } from 'src/constant/data/json-examples'
import { DbType, TargetType } from 'src/enums'
import type { CodeGenerateReq } from 'src/api/models/code-generate-models'
import { JSON_TO_ENTITY_CODE_TARGET_TYPE_OPTIONS, TARGET_TYPE_LANGUAGE_MAPPING } from 'src/constant'
import { generateCodeByJson } from 'src/api/code-generate-api'

const targetCode = ref<string>('')
const reqData = ref<CodeGenerateReq>({
  code: '',
  dbType: DbType.SQLServer,
  targetType: TargetType.SpringDataMongoDBEntity,
  tableNamePrefix: 'T_',
})
const targetLanguage = computed(() => TARGET_TYPE_LANGUAGE_MAPPING[reqData.value.targetType])

/**
 * Clear source code
 * @author shiloh
 * @date 2025-07-22 16:34
 */
function handleClearSrcCode() {
  reqData.value.code = ''
  targetCode.value = ''
}

/**
 * Source code changed callback - regenerate target code after 1s
 * @author shiloh
 * @date 2025-07-22 16:34
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 500)

const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025-07-22 16:34
 */
async function handleGenerateTargetCode() {
  if (!reqData.value.code) {
    return
  }

  try {
    targetCode.value = await generateCodeByJson(reqData.value)
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
  reqData.value.code = JSON_OBJECT_TEST_STR
})

// endregion
</script>

<style scoped></style>
