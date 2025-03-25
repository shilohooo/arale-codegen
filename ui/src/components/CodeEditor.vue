<!--
  * Monaco Editor
  * @author shiloh
  * @date 2025/1/23 17:29
-->
<template>
  <div class="code-edit-box">
    <vue-monaco-editor
      v-model:value="code"
      :options="editorOptions"
      @mount="initEditor"
      :language="language"
      theme="vs"
      @change="handleCodeChange"
    />
  </div>
</template>

<script setup lang="ts">
import { VueMonacoEditor } from '@guolao/vue-monaco-editor'
import type { EditorLanguage } from 'src/types/code-editor'
import { EDITOR_TAB_SIZE } from 'src/constant'
import type * as monaco from 'monaco-editor'

defineOptions({ name: 'CodeEditor' })
const emits = defineEmits(['update:modelValue', 'change'])
const props = withDefaults(
  defineProps<{
    modelValue: string
    language: EditorLanguage
    width?: string | number
    height?: string | number
  }>(),
  {
    modelValue: 'interface User {}',
    language: 'typescript',
    width: '100%',
    height: '100vh',
  },
)

// region init
const code = ref()
const editor = shallowRef<monaco.editor.IStandaloneCodeEditor>()
const editorOptions = ref<monaco.editor.IStandaloneEditorConstructionOptions>({})

/**
 * init editor
 * @author shiloh
 * @date 2025/1/23 17:33
 */
async function initEditor(editorInstance: monaco.editor.IStandaloneCodeEditor) {
  // create editor
  editor.value = editorInstance
}

/**
 * code change callback
 * @param code new code
 * @author shiloh
 * @date 2025/3/21 16:05
 */
function handleCodeChange(code: string | undefined) {
  emits('update:modelValue', code)
  emits('change', code)
}

// endregion

// region watch

watch(
  () => props.modelValue,
  (newValue) => {
    if (newValue === code.value) {
      return
    }

    code.value = newValue
  },
)

// endregion

// region mounted

onMounted(async () => {
  editorOptions.value = {
    minimap: { enabled: false },
    automaticLayout: true,
    readOnly: false,
    fontSize: 16,
    tabSize: EDITOR_TAB_SIZE[props.language] ?? 4,
  }
  code.value = props.modelValue
  handleCodeChange(code.value)
})

// endregion

// region before unmount

onBeforeUnmount(() => {
  editor.value?.dispose()
})

// endregion
</script>

<style scoped>
.code-edit-box {
  width: v-bind(width);
  height: v-bind(height);
}
</style>
