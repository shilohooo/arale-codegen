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
      <div class="row items-center justify-between q-mb-md q-gutter-x-md">
        <div class="col">Jsdoc</div>
        <div class="col">
          <q-input
            v-model="rootTypeName"
            label="Root type name"
            placeholder="Input the name of root type..."
            @update:model-value="handleSrcCodeChange"
          />
        </div>
        <div class="col">
          <q-input
            v-model="typesPrefix"
            label="Types prefix"
            placeholder="Input the types prefix..."
            @update:model-value="handleSrcCodeChange"
          />
        </div>
        <div class="col">
          <q-input
            v-model="typesSuffix"
            label="Types suffix"
            placeholder="Input the types suffix..."
            @update:model-value="handleSrcCodeChange"
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
      <code-editor v-model="targetCode" language="javascript" />
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

const srcCode = ref<string>('')
const targetCode = ref<string>('')

/**
 * Clear source code
 * @author shiloh
 * @date 2025/3/6 11:02
 */
function handleClearSrcCode() {
  srcCode.value = ''
  targetCode.value = ''
}

/**
 * Source code changed callback - regenerate target code after 1s
 * @author shiloh
 * @date 2025/3/6 11:02
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 500)

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
  if (!srcCode.value) {
    return
  }

  try {
    targetCode.value = jsonToJSDoc(srcCode.value, {
      rootTypeName: rootTypeName.value,
      typePrefix: typesPrefix.value,
      typeSuffix: typesSuffix.value,
    })
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
