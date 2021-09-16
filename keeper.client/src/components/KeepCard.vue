<template>
  <div class="KeepCard">
    <div class="card p-0 action" :data-target="'#keep-detail-'+keep.id" data-toggle="modal" @click="incrementKeepViews">
      <div class="card-body card-img p-0">
        <img class="w-100 h-100" :src="keep.img" alt="" srcset="">
        <div class="card-img-overlay text-light d-flex justify-content-around align-items-end mb-1">
          <h4 class="shadower">
            {{ keep.name }}
          </h4>
          <img class="action rounded-pill" :src="keep.creator.picture" height="40" alt="profile-pic" @click.stop="profileNavigate">
        </div>
      </div>
    </div>
  </div>
  <!-- Modal for Edits -->
  <div class="modal fade"
       :id="'keep-detail-'+keep.id"
       tabindex="-1"
       role="dialog"
       :aria-labelledby="'keep-detail-'+keep.id"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-dialog-centered" style="max-width: 90%; overflow-y: auto;" role="document">
      <div class="modal-content">
        <div class="model-header"></div>
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6">
                <img class="w-100" :src="keep.img" alt="">
              </div>
              <div class="col-md-6">
                <div class="row justify-content-center">
                  <div class="col-md-12">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="col-md-4">
                    <i class="mdi mdi-eye mdi-24px px-1" style="fill: green;" aria-hidden="true">{{ state.keeps.views }}</i>
                    <i class="mdi mdi-alpha-k-box-outline mdi-24px px-1" style="fill: green;" aria-hidden="true">{{ state.keeps.keeps }}</i>
                    <i class="mdi mdi-share-variant mdi-24px px-1" style="fill: green;" aria-hidden="true">{{ state.keeps.shares }}</i>
                  </div>
                  <div class="col-md-9">
                    <h3>{{ keep.name }}</h3>
                  </div>
                  <div class="col-md-11">
                    <p>{{ keep.description }}</p>
                  </div>
                  <div class="col-md-6">
                    <div class="dropdown rounded bg-green">
                      <div
                        class="dropdown-toggle"
                        @click="state.dropOpen = !state.dropOpen"
                      >
                        <span class="text-light mx-3"><b>Add To Vault: </b> </span>
                      </div>
                      <div
                        v-for="v in vaults"
                        :key="v.id"
                        class="dropdown-menu p-0 list-group w-100"
                        :class="{ show: state.dropOpen }"
                        @click="state.dropOpen = false"
                      >
                        <div class="list-group-item list-group-item-action hoverable" @click="addKeepToVault(v.id)">
                          {{ v.name }}
                        </div>
                      </div>
                    </div>
                    <i v-if="keep.creatorId===account.id" class="mdi mdi-delete-outline mdi-24px" style="fill: red;" aria-hidden="true" @click="removeKeep"></i>
                  </div>
                  <div class="col-md-6 d-flex align-items-center justify-content-between action" @click.stop="profileNavigate">
                    <img class="rounded-pill w-25" :src="keep.creator.picture" alt="">
                    <h6>{{ keep.creator.name }}</h6>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { computed, onBeforeMount, reactive } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'

export default {
  name: 'KeepCard',
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    onBeforeMount(async() => {
      try {
        // logger.log('I AM IN THE KEEP CAR: ', props.keep)
        // logger.log('I AM IN THE KEEP CARD APP ACCOUNT: ', AppState.account.name)
        // await keepsService.getKeepById(props.keep.id)
      } catch (error) {
        // Pop.toast('Failed to get Keep by Id: ' + error, 'error')
      }
    })
    const router = useRouter()
    const state = reactive({
      dropOpen: false,
      keeps: AppState.keeps.find(k => k.id === props.keep.id)
    })
    return {
      state,
      router,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults),

      async profileNavigate() {
        try {
          await router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
        } catch (error) {
          Pop.toast('Failed to got to Profile: ' + error, 'error')
        }
      },
      async addKeepToVault(vaultsId) {
        try {
          const createdVaultKeep = { keepId: props.keep.id, vaultId: vaultsId }
          await vaultKeepsService.createVaultKeep(createdVaultKeep)
          $(('#keep-detail-' + props.keep.id)).modal('hide')
          Pop.toast('Added Keep to Vault', 'success')
        } catch (error) {
          Pop.toast('Failed to put keep in vault: ' + error, 'error')
        }
      },
      async incrementKeepViews() {
        try {
          const keep = await keepsService.getKeepById(props.keep.id)
          state.keeps = keep
        } catch (error) {
          Pop.toast('Failed to add View Counter: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.contain {
  object-fit: fill;
}
.dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}

.modal-body{
  img{
    max-height: 85vh;
  }
}

</style>
