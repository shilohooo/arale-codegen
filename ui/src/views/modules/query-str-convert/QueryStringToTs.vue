<!--
  * Convert URL Query String to TS
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
        <div class="col">TypeScript</div>
        <div class="col">
          <q-toggle
            v-model="useTypeAlias"
            label="Use type alias"
            :true-value="true"
            :false-value="false"
            @update:model-value="handleGenerateTargetCode"
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
      <code-editor v-model="targetCode" language="typescript" />
    </div>
    <!--endregion-->
  </div>
</template>

<script setup lang="ts">
import CodeEditor from 'components/CodeEditor.vue'
import { debounce, useQuasar } from 'quasar'
import { useClipboard } from 'src/hooks/useClipboard'
import JsonToTS from 'json-to-ts'
import { TEST_QUERY_STR } from 'src/constant/data/query-str-example'
import qs from 'qs'

const srcCode = ref<string>('')
const useTypeAlias = ref<boolean>(false)
const targetCode = ref<string>('')

/**
 * Clear source code
 * @author shiloh
 * @date 2025/3/25 22:12
 */
function handleClearSrcCode() {
  srcCode.value = ''
  targetCode.value = ''
}

/**
 * Source code changed callback - regenerate target code after 1s
 * @author shiloh
 * @date 2025/3/25 22:12
 */
const handleSrcCodeChange = debounce(async () => {
  await handleGenerateTargetCode()
}, 500)

const $q = useQuasar()

/**
 * Generate target code
 * @author shiloh
 * @date 2025/3/25 22:12
 */
async function handleGenerateTargetCode() {
  if (!srcCode.value) {
    return
  }

  const jsonObj = JSON.parse(JSON.stringify(qs.parse(srcCode.value)))
  try {
    targetCode.value = ''
    const typeInterfaces = JsonToTS(jsonObj, {
      rootName: 'Root',
      useTypeAlias: useTypeAlias.value,
    })
    typeInterfaces.forEach((typeInterface, index) => {
      if (index < typeInterfaces.length - 1) {
        targetCode.value += `${typeInterface}\n\n`
      } else {
        targetCode.value += `${typeInterface}\n`
      }
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
  srcCode.value = TEST_QUERY_STR
  handleGenerateTargetCode()
})

// endregion
</script>

<style scoped></style>
