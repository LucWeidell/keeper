<template>
  <div class="KeepCard">
    <div class="card p-0 action" :data-target="'#keep-detail-'+keep.id" data-toggle="modal" @click="incrementKeepViews">
      <div class="card-body card-img p-0">
        <img class="w-100" :src="keep.img" alt="" srcset="">
        <div v-if="showimg" class="card-img-overlay text-light d-flex justify-content-around align-items-end mb-1">
          <h4 class="shadower">
            {{ keep.name }}
          </h4>
          <img
            class="action rounded-pill"
            :src="keep.creator.picture"
            height="40"
            alt="profile-pic"
            @click.stop="profileNavigate"
          >
        </div>
        <div v-else class="card-img-overlay text-light d-flex justify-content-start align-items-end mb-1">
          <h4 class="shadower m-0">
            {{ keep.name }}
          </h4>
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
    <div class="modal-dialog modal-dialog-centered" style="max-width: 97%; " role="document">
      <div class="modal-content">
        <div class="model-header"></div>
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6">
                <img class="w-100" :src="keep.img" alt="">
              </div>
              <div class="col-md-6">
                <div class="row h-100 justify-content-around">
                  <div class="col-md-12 ">
                    <div class="row justify-content-center">
                      <div class="col-md-12">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                          <span aria-hidden="true">&times;</span>
                        </button>
                      </div>
                      <div v-if="state.vaultKeep != null" class=" d-flex justify-content-center">
                        <i class="mdi mdi-eye mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.vaultKeep.views }}</i>
                        <i class="mdi mdi-alpha-k-box-outline mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.vaultKeep.keeps }}</i>
                        <i class="mdi mdi-share-variant mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.vaultKeep.shares }}</i>
                        <button v-if=" profile.id===account.id && account != {}" type="button" class="btn btn-sm btn-danger" @click="removeKeepFromVault">
                          - from Vault
                        </button>
                      </div>
                      <div v-else class="col d-flex justify-content-center">
                        <i class="mdi mdi-eye mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.keeps.views }}</i>
                        <i class="mdi mdi-alpha-k-box-outline mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.keeps.keeps }}</i>
                        <i class="mdi mdi-share-variant mdi-24px px-1" style="color: green;" aria-hidden="true">:  {{ state.keeps.shares }}</i>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-11 modal-text-space">
                    <div class="row">
                      <div class="col-md-12 py-2 text-center">
                        <h1>{{ keep.name }}</h1>
                      </div>
                      <div class="col-md-11 pt-3">
                        <h5>
                          <span class="font-weight-normal">
                            {{ keep.description }}
                          </span>
                        </h5>
                      </div>
                    </div>
                  </div>
                  <div class="col-md-12 d-flex p-0 align-items-end ">
                    <div class="row flex-grow-1 justify-content-around mb-1">
                      <div class="col-auto col-md-5 d-flex p-0 align-items-center">
                        <div class="dropdown rounded bg-green">
                          <div
                            class="dropdown-toggle"
                            @click="state.dropOpen = !state.dropOpen"
                          >
                            <span class="text-light mx-1"><b>Add To Vault:</b> </span>
                          </div>
                          <div
                            class="dropdown-menu p-0 list-group w-100"
                            :class="{ show: state.dropOpen }"
                            @click="state.dropOpen = false"
                          >
                            <div v-for="v in vaults"
                                 :key="v.id"
                                 class="list-group-item list-group-item-action action hoverable"
                                 @click="addKeepToVault(v.id)"
                            >
                              {{ v.name }}
                            </div>
                          </div>
                        </div>
                        <i v-if="keep.creatorId===account.id" class="mdi mdi-delete-outline mdi-24px action" style="color: red;" aria-hidden="true" @click="removeKeep"></i>
                      </div>
                      <div class="col-auto d-flex p-0 pl-1 align-items-center action" @click.stop="profileNavigate">
                        <img class="rounded" :src="keep.creator.picture" height="25" alt="">
                        <p class="m-0 pl-1">
                          {{ keep.creator.name }}
                        </p>
                      </div>
                    </div>
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
import { computed, reactive } from '@vue/runtime-core'
import { useRouter } from 'vue-router'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultKeepsService } from '../services/VaultKeepsService'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'KeepCard',
  props: {
    keep: {
      type: Object,
      required: true
    },
    showimg: {
      type: Boolean,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    const state = reactive({
      dropOpen: false,
      keeps: AppState.keeps.find(k => k.id === props.keep.id),
      vaultKeep: AppState.activeVaultKeeps.find(k => k.id === props.keep.id)
    })
    return {
      state,
      router,
      account: computed(() => AppState.account),
      profile: computed(() => AppState.activeProfile),
      vaults: computed(() => AppState.vaults),

      async profileNavigate() {
        try {
          $(('#keep-detail-' + props.keep.id)).modal('hide')
          await router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
        } catch (error) {
          Pop.toast('Failed to got to Profile: ' + error, 'error')
        }
      },
      async addKeepToVault(vaultsId) {
        try {
          const createdVaultKeep = { KeepId: props.keep.id, VaultId: vaultsId }
          await vaultKeepsService.createVaultKeep(createdVaultKeep)
          await keepsService.addKeepDownload(props.keep.id)
          $(('#keep-detail-' + props.keep.id)).modal('hide')
          Pop.toast('Added Keep to Vault', 'success')
        } catch (error) {
          Pop.toast('Failed to put keep in vault: ' + error, 'error')
        }
      },
      async incrementKeepViews() {
        try {
          const keep = await keepsService.getKeepById(props.keep.id)
          if (state.keeps?.views) {
            state.keeps.views = keep.views
          }
          if (state.vaultKeep?.views) {
            state.vaultKeep.views = keep.views
          }
        } catch (error) {
          Pop.toast('Failed to add View Counter: ' + error, 'error')
        }
      },
      async removeKeep() {
        try {
          if (await Pop.confirm()) {
            $(('#keep-detail-' + props.keep.id)).modal('hide')
            await keepsService.removeKeep(props.keep.id)
            Pop.toast('Perma Deleted this Keep!', 'success')
          }
        } catch (error) {
          Pop.toast('Failed to add View Counter: ' + error, 'error')
        }
      },
      async removeKeepFromVault() {
        try {
          if (await Pop.confirm()) {
            // debugger
            $(('#keep-detail-' + props.keep.id)).modal('hide')
            await vaultKeepsService.removeVaultKeep(props.keep.vaultKeepId)
            Pop.toast('Removed Keep from Vault', 'success')
          }
        } catch (error) {
          Pop.toast('Failed to remove keep from vault: ' + error, 'error')
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
  max-height: 25vh;
  overflow-y: scroll;
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
    max-height: 70vh;
  }
}

.modal-text-space{
  min-height: 65%;
}

</style>
