<template>
  <div class="flex q-pa-sm bg-grey-2" style="gap: 1rem">
    <div style="flex: 1">
      <q-card>
        <q-card-section>
          <div class="text-h6">Dependencies</div>
        </q-card-section>
        <q-card-section>
          <q-scroll-area style="height: 700px">
            <q-list bordered separator dense>
              <q-item v-for="dep in deps" :key="dep.name" clickable>
                <q-item-section>
                  {{ dep.name }}
                </q-item-section>
                <q-item-section side>
                  {{ dep.version }}
                </q-item-section>
              </q-item>
            </q-list>
          </q-scroll-area>
        </q-card-section>
      </q-card>
    </div>

    <div style="flex: 1">
      <q-card>
        <q-card-section>
          <div class="text-h6">DevDependencies</div>
        </q-card-section>
        <q-card-section>
          <q-scroll-area style="height: 700px">
            <q-list bordered separator dense>
              <q-item v-for="dep in devDeps" :key="dep.name">
                <q-item-section>
                  {{ dep.name }}
                </q-item-section>
                <q-item-section side>
                  {{ dep.version }}
                </q-item-section>
              </q-item>
            </q-list>
          </q-scroll-area>
        </q-card-section>
      </q-card>
    </div>
  </div>
</template>

<script setup lang="ts">
import { dependencies, devDependencies } from '../../package.json'

defineOptions({
  name: 'IndexPage',
})

type Dep = {
  name: string
  version: string
}

const deps = ref<Dep[]>([])
const devDeps = ref<Dep[]>([])

onMounted(async () => {
  deps.value = Object.entries(dependencies).map(([name, version]) => ({
    name,
    version,
  }))
  devDeps.value = Object.entries(devDependencies).map(([name, version]) => ({
    name,
    version,
  }))
})
</script>
