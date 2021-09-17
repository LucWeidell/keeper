<template>
  <div class="KeepsThread my-3">
    <div class="d-flex">
      <h2>Keeps:</h2>
      <i v-if="state.profile.id === state.account.id"
         class="mdi mdi-plus-thick mdi-24px action"
         style="color: green;"
         data-toggle="modal"
         data-target="#createKeep"
      ></i>
    </div>
    <div class="mt-3 card-columns flex-grow-1">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" :showimg="false" />
    </div>
  </div>
  <!-- Modal -->
  <div class="modal fade"
       id="createKeep"
       tabindex="-1"
       role="dialog"
       aria-labelledby="modelTitleId"
       aria-hidden="true"
  >
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="modal-header">
          <h3 class="modal-title">
            New Keep
          </h3>
          <button type="button" class="close" style="color: red;" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep">
            <div class="form-group">
              <div class="">
                <label for="titleKeep">Title</label>
                <input type="text"
                       v-model="state.createdKeep.name"
                       placeholder="Title..."
                       id="titleKeep"
                       name="titleKeep"
                       class="form-control"
                       required
                       minlength="3"
                       maxlength="20"
                >
              </div>
              <div class="mt-2">
                <label for="imgKeep">Image Url</label>
                <input type="text"
                       v-model="state.createdKeep.img"
                       placeholder="URL..."
                       id="imgKeep"
                       name="imgKeep"
                       class="form-control"
                       required
                >
              </div>
              <div class="mt-2">
                <label for="descriptionKeep">Description</label>
                <textarea v-model="state.createdKeep.description"
                          placeholder="Keep Description"
                          class="form-control"
                          name="descriptionKeep"
                          id="descriptionKeep"
                          rows="4"
                          required
                          minlength="3"
                          maxlength="40"
                ></textarea>
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
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Notifier'

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
      account: computed(() => AppState.account),
      createdKeep: {}
    })
    return {
      state,
      async createKeep() {
        try {
          state.createdKeep.creatorId = state.account.id
          await keepsService.createKeep(state.createdKeep)
          $(('#createKeep')).modal('hide')
          Pop.toast('Added a Keep', 'success')
          state.createdKeep = {}
        } catch (error) {
          Pop.toast('Failed to create keep: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>

</style>
