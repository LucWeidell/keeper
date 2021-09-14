<template>
  <div class="masonry-with-flex px-2 mt-2 flex-grow-1">
    <div class="KeepCard flex-grow-1 h-100 col-md-3 d-flex align-items-end justify-content-between" data-toggle="modal" :data-target="'#keep-detail-'+keep.id" style="background-image: url('http://placebeard.it/300x300'); background-size: cover;">
      <h4>Name Keep</h4>
      <!-- FIXME add the keep creator id here in params -->
      <img class="rounded-pill" src="http://placebeard.it/50x50" alt="" @click.stop="profileNavigate">
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
    <!-- <div class="modal" tabindex="-1" role="dialog">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <h5 class="modal-title">
              Modal title
            </h5>
            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
              <span aria-hidden="true">&times;</span>
            </button>
          </div>
          <div class="modal-body">
            <p>Modal body text goes here.</p>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-primary">
              Save changes
            </button>
            <button type="button" class="btn btn-secondary" data-dismiss="modal">
              Close
            </button>
          </div>
        </div>
      </div>
    </div> -->
    <div class="modal-dialog" role="document">
      <div class="modal-content">
        <div class="model-header"></div>
        <div class="modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6">
                <img class="w-100 h-100" src="http://placebeard.it/500x500" alt="">
              </div>
              <div class="col-md-6">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <button type="button" class="btn btn-secondary" data-dismiss="modal">
                  Close
                </button>
                <button type="button" class="btn btn-primary">
                  Save
                </button>
              </div>
            </div>
          </div>
          <!-- <form @submit.prevent="editBug">
            <div class="form-group">
              <label :for="'title'+state.bug.id" class="">title </label>
              <input class="mb-2 ml-2"
                     type="string"
                     :name="'title'+state.bug.id"
                     :id="'title'+state.bug.id"
                     placeholder="title..."
                     required
                     v-model="state.bugCopy.title"
              >
              <label :for="'description'+state.bug.id" class="">description </label>
              <input class="mb-2 ml-2"
                     type="string"
                     :name="'description'+state.bug.id"
                     :id="'description'+state.bug.id"
                     placeholder="0"
                     required
                     v-model="state.bugCopy.description"
              >
              <div class="form-group">
                <label for="status">Status:&nbsp; </label>
                <select class="form-control action" name="status" id="status" v-model="state.bugCopy.closed" required>
                  <option value="false">
                    Open
                  </option>
                  <option value="true">
                    Closed
                  </option>
                </select>
              </div>
              <div v-if="state.bugCopy.closed" class="form-group">
                <label for="closeDate">Closed Date:&nbsp;</label>
                <input type="date"
                       class="form-control action"
                       name="closeDate"
                       id="closeDate"
                       aria-describedby="close-date"
                       v-model="state.bugCopy.closedDate"
                >
              </div>
              <div v-else class="form-group">
                <label for="closeDate">Closed Date:&nbsp;</label>
                <input type="date"
                       class="form-control"
                       name="closeDate"
                       id="closeDate"
                       aria-describedby="close-date"
                       v-model="state.bugCopy.closedDate"
                       readonly
                >
              </div> -->
          <!-- <button type="button" class="btn btn-secondary" data-dismiss="modal">
                Close
              </button>
              <button type="submit" class="btn btn-primary">
                Save
              </button> -->
          <!-- </div>
        </form> -->
          <!-- </div>
      </div>
    </div>
  </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { useRouter } from 'vue-router'
import Keep from '../models/Keep'
import Pop from '../utils/Notifier'

export default {
  name: 'KeepCard',
  props: {
    keep: {
      type: Keep,
      required: true
    }
  },
  setup(props) {
    const router = useRouter()
    return {
      router,
      async profileNavigate() {
        try {
          await router.push({ name: 'Profile', params: { id: props.keep.creatorId } })
        } catch (error) {
          Pop.error('Failed to got to Profile: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style lang="scss" scoped>
.masonry-with-flex {
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  .col-md-3 {
    max-width: 300px;
    min-width: 300px;
    min-height: 400px;
    // min-height:600px;
    color: white;
    margin: 0 1rem 1rem 0;
    font-family: system-ui;
    font-weight: 900;
    font-size: 2rem;
  }
}

</style>
