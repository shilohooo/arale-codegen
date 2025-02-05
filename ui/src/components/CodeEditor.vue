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

defineOptions({ name: 'CodeEditor' })
const emits = defineEmits(['update:modelValue', 'change'])
const props = defineProps<{ modelValue: string; language: EditorLanguage }>()

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
    theme: 'vs-dark',
    minimap: { enabled: false },
    automaticLayout: true,
    readOnly: false,
  })

  // model content change callback
  editor.onDidChangeModelContent(() => {
    formatCode()
    const code = editor.getValue()
    emits('update:modelValue', code)
    emits('change', code)
  })
}

// endregion

// region mounted

onMounted(async () => {
  await initEditor()
  formatCode()
  // const res = await api.post('/CodeGenerate/GenerateBySql', {
  //   code: 'create table T_Users(id bigint primary key, username varchar(50) not null, address nvarchar(255) not null, gender char(1), enabled bit(1))',
  //   dbType: 2,
  //   targetType: 1,
  //   tableNamePrefix: 'T_',
  // })
  // console.log('res', res)
  // editor.setValue(res.data)
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
  width: 100vw;
  height: 100vh;
}
</style>
