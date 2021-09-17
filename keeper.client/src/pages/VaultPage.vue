<template>
  <div class="VaultPage container">
    <div class="row d-flex">
      <h1 class="pl-1">
        {{ state.vault.name }}
      </h1>
      <i v-if="state.vault.creatorId===state.account.id"
         class="mdi mdi-delete-outline mdi-36px action"
         style="color: red;"
         title="Delete Vault"
         aria-hidden="true"
         @click="removeVault"
      ></i>
    </div>
    <div class="row">
      <h5 class="pl-1">
        Keeps:  {{ state.keeps.length }}
      </h5>
    </div>
    <div class="card-columns flex-grow-1 mt-4">
      <KeepCard v-for="k in state.keeps" :key="k.id" :keep="k" :showimg="true" />
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute, useRouter } from 'vue-router'
import { vaultsService } from '../services/VaultsService'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'VaultPage',
  setup() {
    const route = useRoute()
    const router = useRouter()
    const state = reactive({
      vault: computed(() => AppState.activeVault),
      keeps: computed(() => AppState.activeVaultKeeps),
      account: computed(() => AppState.account)
    })
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await vaultsService.getVaultById(route.params.vaultId)
        await vaultsService.getKeepsInVault(route.params.vaultId)
      } catch (error) {
        Pop.toast('Invalid Vault:', 'error')
        await router.push({ name: 'Home' })
      }
    })
    return {
      route,
      router,
      state,
      async removeVault() {
        try {
          if (await Pop.confirm()) {
            await vaultsService.removeVault(state.vault.id)
            await router.push({ name: 'Profile', params: { id: state.vault.creatorId } })
          }
        } catch (error) {
          Pop.toast('Failed to delete vault: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
