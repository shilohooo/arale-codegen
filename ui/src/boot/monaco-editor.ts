/**
 * Monaco Editor boot file
 * @author shiloh
 * @date 2025/1/23 17:19
 */
import JsonWorker from 'monaco-editor/esm/vs/language/json/json.worker?worker'
import CssWorker from 'monaco-editor/esm/vs/language/css/css.worker?worker'
import HtmlWorker from 'monaco-editor/esm/vs/language/html/html.worker?worker'
import TsWorker from 'monaco-editor/esm/vs/language/typescript/ts.worker?worker'
import EditorWorker from 'monaco-editor/esm/vs/editor/editor.worker?worker'

self.MonacoEnvironment = {
  getWorker(_: never, label: string) {
    if (label === 'json') {
      return new JsonWorker()
    }

    if (label === 'css' || label === 'scss' || label === 'less') {
      return new CssWorker()
    }

    if (label === 'html' || label === 'handlebars' || label === 'razor') {
      return new HtmlWorker()
    }

    if (label === 'typescript' || label === 'javascript') {
      return new TsWorker()
    }

    return new EditorWorker()
  },
}
