<template>
  <div class="HomePage container-fluid">
    <div class="row">
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
.masonry-with-flex {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  max-height: 1000px;
  div {
    max-width: 200px;
    min-height:500px;
    color: white;
    margin: 0 1rem 1rem 0;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
}

</style>
