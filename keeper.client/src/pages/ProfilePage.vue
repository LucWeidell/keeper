<template>
  <div class="ProfilePage container">
    <div class="row align-items-center mt-3">
      <div class="col-auto">
        <img :src="state.profile.picture" alt="profile-pic">
      </div>
      <div class="col-auto">
        <h1>{{ state.profile.name }}</h1>
        <h3> Vaults: {{ state.vaults.length }}</h3>
        <h3> Keeps: {{ state.keeps.length }}</h3>
      </div>
    </div>
    <VaultsThread :vaults="state.vaults" />
    <KeepsThread :keeps="state.keeps" />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'
import { AppState } from '../AppState'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    const state = reactive({
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.activeProfileVaults),
      keeps: computed(() => AppState.keeps)
    })
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
        await profilesService.getVaultsInProfile(state.profile.id)
        await profilesService.getKeepsInProfile(state.profile.id)
      } catch (error) {
        Pop.toast('Cannot get Profile Data: ' + error, 'error')
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
