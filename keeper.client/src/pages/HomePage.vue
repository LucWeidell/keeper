<template>
  <div class="HomePage container-fluid flex-grow-1">
    <div class="row card-columns flex-grow-1">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
// import waitForImages from '../utils/Masonry'
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
export default {
  name: 'HomePage',
  setup() {
    onMounted(async() => {
      try {
        // await keepsService.getKeeps()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
