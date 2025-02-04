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
import { api } from 'boot/axios'

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

  // create editor
  editor = monaco.editor.create(codeEditBox.value!, {
    value: '',
    language: 'csharp',
    theme: 'vs-dark',
    minimap: { enabled: false },
    automaticLayout: true,
    readOnly: false,
  })
  // model content change callback
  editor.onDidChangeModelContent(() => {
    // TODO return code to client by emit
  })
}

onMounted(async () => {
  await initEditor()
  const res = await api.post('/CodeGenerate/GenerateBySql', {
    code: 'create table T_Users(id bigint primary key, username varchar(50) not null, address nvarchar(255) not null, gender char(1), enabled bit(1))',
    dbType: 2,
    targetType: 1,
    tableNamePrefix: 'T_',
  })
  console.log('res', res)
  editor.setValue(res.data)
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
