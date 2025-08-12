/**
 * Monaco Editor boot file
 * @author shiloh
 * @date 2025/1/23 17:19
 */
import { loader } from '@guolao/vue-monaco-editor'
import { format as sqlFormat } from 'sql-formatter'

// Import worker loaders
import editorWorker from 'monaco-editor/esm/vs/editor/editor.worker?worker'
import jsonWorker from 'monaco-editor/esm/vs/language/json/json.worker?worker'
import cssWorker from 'monaco-editor/esm/vs/language/css/css.worker?worker'
import htmlWorker from 'monaco-editor/esm/vs/language/html/html.worker?worker'
import tsWorker from 'monaco-editor/esm/vs/language/typescript/ts.worker?worker'

// Configure Monaco Environment with workers
self.MonacoEnvironment = {
  getWorker: function (_: string, label: string) {
    switch (label) {
      case 'json':
        return new jsonWorker()
      case 'css':
      case 'scss':
      case 'less':
        return new cssWorker()
      case 'html':
      case 'handlebars':
      case 'razor':
        return new htmlWorker()
      case 'typescript':
      case 'javascript':
        return new tsWorker()
      default:
        return new editorWorker()
    }
  },
}

loader.config({
  paths: {
    vs: 'https://cdn.jsdelivr.net/npm/monaco-editor@0.52.2/min/vs',
  },
})
loader.init().then((monaco) => {
  monaco.languages.typescript.javascriptDefaults.setDiagnosticsOptions({
    noSemanticValidation: true,
    noSyntaxValidation: true,
    noSuggestionDiagnostics: true,
  })
  monaco.languages.typescript.typescriptDefaults.setDiagnosticsOptions({
    noSemanticValidation: true,
    noSyntaxValidation: true,
    noSuggestionDiagnostics: true,
  })
  monaco.languages.typescript.typescriptDefaults.setCompilerOptions({
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
})
