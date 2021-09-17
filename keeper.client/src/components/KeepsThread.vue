<template>
  <div class="KeepsThread my-3">
    <div class="d-flex">
      <h2>Keeps:</h2>
      <i v-if="state.profile.id === state.account.id" class="mdi mdi-plus-thick mdi-24px action" style="color: green;"></i>
    </div>
    <div class="mt-3 card-columns flex-grow-1">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" :showimg="false" />
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'

export default {
  name: 'KeepsThread',
  props: {
    keeps: {
      type: Array,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account)
    })
    return { state }
  }
}
</script>

<style lang="scss" scoped>

</style>
