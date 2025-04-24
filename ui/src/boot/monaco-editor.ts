/**
 * Monaco Editor boot file
 * @author shiloh
 * @date 2025/1/23 17:19
 */
import { loader } from '@guolao/vue-monaco-editor'
import { format as sqlFormat } from 'sql-formatter'

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
})
