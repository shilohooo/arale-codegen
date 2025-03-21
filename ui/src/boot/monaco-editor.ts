/**
 * Monaco Editor boot file
 * @author shiloh
 * @date 2025/1/23 17:19
 */
import { loader } from '@guolao/vue-monaco-editor'
loader.config({
  paths: {
    vs: 'https://cdn.jsdelivr.net/npm/monaco-editor@0.52.2/min/vs',
  },
})
