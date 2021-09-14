<template>
  <div class="ProfilePage">
  </div>
</template>

<script>
import { onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import Pop from '../utils/Notifier'
import { profilesService } from '../services/ProfilesService'

export default {
  name: 'ProfilePage',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profilesService.getById(route.params.id)
      } catch (error) {
        Pop.toast('Cannot get Profile: ' + error, 'error')
      }
    })
    return {}
  }
}
</script>

<style lang="scss" scoped>

</style>
