<!--
  * Monaco Editor
  * @author shiloh
  * @date 2025/1/23 17:29
-->
<template>
  <div ref="codeEditBox" class="code-edit-box" />
</template>

<script setup lang="ts">
import * as monaco from 'monaco-editor'
import { format as sqlFormat } from 'sql-formatter'
import type { EditorLanguage } from 'src/types/code-editor'
import { EDITOR_TAB_SIZE } from 'src/constant'

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

/**
 * Call format document action
 * @author shiloh
 * @date 2025/2/4 21:38
 */
function formatCode() {
  if (!editor.getValue()) {
    return
  }
  editor.getAction('editor.action.formatDocument')?.run()
}

// region init

let editor: monaco.editor.IStandaloneCodeEditor
const codeEditBox = ref<HTMLDivElement | null>()

/**
 * init editor
 * @author shiloh
 * @date 2025/1/23 17:33
 */
async function initEditor() {
  monaco.languages.typescript.javascriptDefaults.setDiagnosticsOptions({
    noSemanticValidation: true,
    noSyntaxValidation: true,
  })
  monaco.languages.typescript.javascriptDefaults.setCompilerOptions({
    target: monaco.languages.typescript.ScriptTarget.ESNext,
    allowNonTsExtensions: true,
  })
  // register sql formatter
  monaco.languages.registerDocumentFormattingEditProvider('sql', {
    provideDocumentFormattingEdits(model) {
      const formattedSqlCode = sqlFormat(model.getValue())
      return [
        {
          range: model.getFullModelRange(),
          text: formattedSqlCode,
        },
      ]
    },
  })

  // create editor
  editor = monaco.editor.create(codeEditBox.value!, {
    value: props.modelValue,
    language: props.language,
    theme: 'vs',
    minimap: { enabled: false },
    automaticLayout: true,
    readOnly: false,
    fontSize: 16,
    tabSize: EDITOR_TAB_SIZE[props.language] ?? 4,
  })

  // model content change callback
  editor.onDidChangeModelContent(() => {
    const code = editor.getValue()
    emits('update:modelValue', code)
    emits('change', code)
  })
}

// endregion

// region watch

watch(
  () => props.modelValue,
  (newValue) => {
    if (!editor) {
      return
    }

    const oldValue = editor.getValue()
    if (newValue === oldValue) {
      return
    }

    editor.setValue(newValue)
  },
)

watch(
  () => props.language,
  (newValue) => {
    if (!editor) {
      return
    }

    monaco.editor.setModelLanguage(editor.getModel()!, newValue)
  },
)

// endregion

// region mounted

onMounted(async () => {
  await initEditor()
  formatCode()
})

// endregion

// region before unmount

onBeforeUnmount(() => {
  editor?.dispose()
})

// endregion
</script>

<style scoped>
.code-edit-box {
  width: v-bind(width);
  height: v-bind(height);
}
</style>
