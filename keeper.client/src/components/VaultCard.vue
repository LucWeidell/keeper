<template>
  <div class="VaultCard">
    <div class="card p-0 action">
      <router-link :to="{name: 'Vault', params: { id: state.profile.id, vaultId: vault.id }}" title="Got To This Vault">
        <div class="card-body card-img p-0">
          <img class="w-100 h-100" src="http://placebeard.it/187x187" alt="vault-image">
          <div class="card-img-overlay text-light d-flex justify-content-start align-items-end mb-1">
            <h4 class="shadower m-0">
              {{ vault.name }}
            </h4>
          </div>
        </div>
      </router-link>
    </div>
  </div>
</template>

<script>
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  name: 'VaultCard',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      profile: computed(() => AppState.activeProfile)
    })
    return { state }
  }
}
</script>

<style lang="scss" scoped>

</style>
