<template>
  <div class="ProfilePage container-fluid">
    <div class="row">
      <div class="col-auto"></div>
    </div>
  </div>
</template>

<script>
import { onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: AppState.account,
      profile: AppState.activeProfile,
      vaults: AppState.activeProfileVaults,
      myVaults: AppState.vaults
    })
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await profilesService.getVaultsInProfile(state.profile.id)
      } catch (error) {
        Pop.toast('Cannot get Profile Info: ' + error, 'error')
      }
    })
    return {
      state
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
