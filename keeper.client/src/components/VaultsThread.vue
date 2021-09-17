<template>
  <div class="VaultsThread my-3">
    <div class="d-flex">
      <h2>Vaults:</h2>
      <i v-if="state.profile.id === state.account.id"
         class="mdi mdi-plus-thick mdi-24px action"
         style="color: green;"
         data-toggle="modal"
         data-target="#createVault"
      ></i>
    </div>
    <div class="mt-3 card-columns flex-grow-1">
      <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
    </div>
  </div>
  <!-- Modal -->
  <div class="modal fade"
       id="createVault"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="modal-title">
            New Vault
          </h3>
          <button type="button" class="close" style="color: red;" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault">
            <div class="form-group">
              <div class="">
                <label for="title">Title</label>
                <input type="text"
                       v-model="state.createdVault.name"
                       placeholder="Title..."
                       id="title"
                       name="name"
                       class="form-control"
                >
              </div>
              <!-- <div class="mt-2">
                <label for="img">Image Url</label>
                <input type="text"
                       v-model="state.createdKeep.name"
                       placeholder="URL..."
                       id="img"
                       name="img"
                       class="form-control"
                >
              </div> -->
              <div class="mt-2">
                <label for="description">Description</label>
                <textarea v-model="state.createdVault.description"
                          placeholder="Vault Description"
                          class="form-control"
                          name="description"
                          id="description"
                          rows="4"
                ></textarea>
              </div>
              <div class="mt-2">
                <div class="form-check">
                  <label class="form-check-label">
                    <input v-model="state.createdVault.isPrivate"
                           type="checkbox"
                           class="form-check-input"
                           name="isPrivate"
                           id="isPrivate"
                           value="checkedValue"
                    >
                    Private?
                  </label>
                </div>
              </div>
              <div class="d-flex mt-3 align-items-baseline justify-content-end">
                <button type="submit" class="btn btn-success">
                  Create
                </button>
              </div>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import $ from 'jquery'
import { reactive } from '@vue/reactivity'
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
import { vaultsService } from '../services/VaultsService'

export default {
  name: 'VaultsThread',
  props: {
    vaults: {
      type: Array,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      vaults: computed(() => AppState.activeProfileVaults),
      profile: computed(() => AppState.activeProfile),
      account: computed(() => AppState.account),
      createdVault: {}
    })
    return {
      state,
      async createVault() {
        try {
          state.createdVault.creatorId = state.account.id
          await vaultsService.createVault(state.createdVault)
          $(('#createVault')).modal('hide')
          Pop.toast('Added a vault', 'success')
          state.createdVault = {}
        } catch (error) {
          Pop.toast('Failed to add vault: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
