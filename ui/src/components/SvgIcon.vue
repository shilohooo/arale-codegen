<!--
  *
  * @author shiloh
  * @date 2025/4/8 15:56
-->
<template>
  <component :is="currentComponent" :style="svgStyle"></component>
</template>

<script setup lang="ts">
import { computed, type CSSProperties } from 'vue'

const props = withDefaults(
  defineProps<{
    name: string
    prefix?: string
    size?: string | number
    color?: string | undefined
  }>(),
  {
    prefix: 'icon',
    size: '1em',
  },
)

const modules = import.meta.glob('../assets/icons/*.svg', {
  import: 'default',
  eager: true,
})

const currentComponent = computed(() => {
  const fileName = `/${props.prefix}-${props.name}.svg`
  const key = Object.keys(modules).find((key) => key.endsWith(fileName))
  return modules[key!]
})

const svgStyle = computed((): CSSProperties => {
  const size = typeof props.size === 'number' ? `${props.size}px` : props.size
  return {
    width: size,
    height: size,
    ...(props.color ? { color: props.color } : {}),
  }
})
</script>

<style scoped></style>
