<!--
  * Monaco editor with tabs
  * @author shiloh
  * @date 2025/1/23 17:29
-->
<template>
  <div class="code-edit-box">
    <code-editor-tabs v-model="activeTab" :tabs="editorModels" />
    <div ref="editor" class="editor" />
  </div>
</template>

<script setup lang="ts">
import * as monaco from 'monaco-editor'
import CodeEditorTabs from 'components/CodeEditorTabs.vue'
import type { LanguageType } from 'src/enums'
import { EDITOR_TAB_SIZE } from 'src/constant'
import { debounce } from 'quasar'
import type { EditorModel } from 'src/types/code-editor'

defineOptions({ name: 'CodeEditor' })
const emits = defineEmits<{
  (e: 'change', uri: monaco.Uri, code: string | undefined): void
  (e: 'switch-tab', currentTabIdx: number, prevTabIdx?: number): void
}>()
const props = withDefaults(
  defineProps<{
    width?: string | number
    height?: string | number
    editorModels: EditorModel[]
  }>(),
  {
    width: '100%',
    height: '100vh',
    editorModels: () => [],
  },
)

// region init
const activeTab = ref<number>(0)
const editor = ref<HTMLDivElement | null>(null)
let editorInstance: monaco.editor.IStandaloneCodeEditor | null = null
// model & view state cache
const textModelMap = new Map<string, monaco.editor.ITextModel | null>()
const viewStateMap = new Map<string, monaco.editor.ICodeEditorViewState | null>()

/**
 * code change callback
 * @param uri model uri
 * @param code model value
 * @author shiloh
 * @date 2025/3/21 16:05
 */
function handleCodeChange(uri: monaco.Uri, code: string | undefined) {
  emits('change', uri, code)
}

/**
 * Update  editor tab size by model language
 * @param language model language
 * @author shiloh
 * @date 2025/8/2 23:12
 */
function updateEditorTabSize(language: LanguageType) {
  editorInstance?.updateOptions({
    tabSize: EDITOR_TAB_SIZE[language],
  })
}

/**
 * create monaco editor's text models
 * @author shiloh
 * @date 2025/7/30 18:11
 */
function createTextModels() {
  const currentTextModel = editorInstance?.getModel()
  if (!props.editorModels?.length) {
    currentTextModel?.dispose()
    textModelMap.forEach((model, key) => {
      model?.dispose()
      monaco.editor.getModel(monaco.Uri.file(key))?.dispose()
    })
    textModelMap?.clear()
    viewStateMap?.clear()
    editorInstance?.setModel(null)
    return
  }

  const modelUris = props.editorModels.map((editorModel) => editorModel.uri.toString())
  for (const editorModel of props.editorModels) {
    let textModel = textModelMap.get(editorModel.uri.toString())
    if (textModel) {
      // textModel has been removed
      if (!modelUris.includes(editorModel.uri.toString())) {
        textModel.dispose()
        textModelMap.delete(editorModel.uri.toString())
        viewStateMap.delete(editorModel.uri.toString())
        continue
      }

      textModel.setValue(editorModel.value)
      continue
    }

    // textModel not exist
    textModel = monaco.editor.createModel(editorModel.value, undefined, editorModel.uri)
    textModel.onDidChangeContent(
      debounce(() => {
        handleCodeChange(editorModel.uri, textModel.getValue())
      }, 300),
    )
    textModelMap?.set(editorModel.uri.toString(), textModel)
  }

  if (activeTab.value > props.editorModels.length - 1) {
    activeTab.value = 0
  }

  const editorModel = props.editorModels[activeTab.value]!
  if (currentTextModel && currentTextModel.uri.toString() === editorModel.uri.toString()) {
    currentTextModel.setValue(editorModel.value)
  } else {
    const newModel = textModelMap.get(editorModel.uri.toString())!
    editorInstance?.setModel(newModel)
  }

  updateEditorTabSize(editorModel.languageType)

  handleCodeChange(editorModel.uri, editorModel.value)
}

// endregion

/**
 * switch tab callback
 * @param currentTabIdx current tab index
 * @param prevTabIdx previous tab index
 * @author shiloh
 * @date 2025/8/7 22:17
 */
function handleSwitchTab(currentTabIdx: number, prevTabIdx?: number) {
  if (!editorInstance) {
    return
  }

  if (prevTabIdx !== null && prevTabIdx !== undefined && props.editorModels[prevTabIdx]) {
    viewStateMap?.set(
      props.editorModels[prevTabIdx].uri.toString(),
      editorInstance?.saveViewState(),
    )
  }

  const editorModel = props.editorModels[currentTabIdx]!
  if (textModelMap.has(editorModel.uri.toString())) {
    editorInstance?.setModel(textModelMap.get(editorModel.uri.toString())!)
    updateEditorTabSize(editorModel.languageType)
  }

  const viewState = viewStateMap?.get(editorModel.uri.toString())
  if (viewState) {
    editorInstance?.restoreViewState(viewState)
    editorInstance?.focus()
  }

  handleCodeChange(editorModel.uri, editorModel.value)
}

// region watch

watch(
  () => props.editorModels.length,
  () => {
    createTextModels()
  },
)

watch(
  () => activeTab.value,
  (value, oldValue) => {
    handleSwitchTab(value, oldValue)
  },
)

// endregion

// region expose

/**
 * change model code by code tab id
 * @param uri model uri
 * @param value model code
 * @author shiloh
 * @date 2025/8/3 22:59
 */
function changeModelCode(uri: monaco.Uri, value: string) {
  let textModel = textModelMap.get(uri.toString())
  if (textModel) {
    if (textModel.getValue() !== value) {
      textModel?.setValue(value)
    }
    return
  }

  textModel = monaco.editor.createModel(value, undefined, uri)
  textModel.onDidChangeContent(
    debounce(() => {
      handleCodeChange(uri, textModel.getValue())
    }, 300),
  )
  textModelMap.set(uri.toString(), textModel)
  editorInstance?.setModel(textModel)
}

defineExpose({
  changeModelCode,
})

// endregion

// region mounted

onMounted(async () => {
  await nextTick()
  if (!editor.value) {
    console.error('editor element not found:(')
    return
  }

  editorInstance = monaco.editor.create(editor.value, {
    minimap: { enabled: false },
    automaticLayout: true,
    readOnly: false,
    fontSize: 16,
    tabSize: 4,
  })

  createTextModels()
})

// endregion

// region before unmount

onBeforeUnmount(() => {
  textModelMap.forEach((model, key) => {
    model?.dispose()
    monaco.editor.getModel(monaco.Uri.file(key))?.dispose()
  })
  textModelMap.clear()
  viewStateMap.clear()
  editorInstance?.dispose()
})

// endregion
</script>

<style scoped>
.code-edit-box {
  width: v-bind(width);
  height: calc(v-bind(height) - 140px);
}

.editor {
  width: 100%;
  height: 100%;
}
</style>
