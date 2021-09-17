<template>
  <nav class="navbar navbar-expand-lg navbar-dark p-0 px-1  bg-blue">
    <router-link class="navbar-brand p-0 d-flex" :to="{ name: 'Home' }">
      <div class="d-flex flex-column align-items-center">
        <i class="mdi mdi-alpha-k-box-outline mdi-48px px-1" style="color: black;" aria-hidden="true"></i>
      </div>
    </router-link>
    <button
      class="navbar-toggler"
      type="button"
      data-toggle="collapse"
      data-target="#navbarText"
      aria-controls="navbarText"
      aria-expanded="false"
      aria-label="Toggle navigation"
    >
      <span class="navbar-toggler-icon" />
    </button>
    <div class="collapse navbar-collapse" id="navbarText">
      <ul class="navbar-nav mr-auto">
        <li class="nav-item">
          <router-link :to="{name: 'Home'}" class="nav-link text-light">
            Home
          </router-link>
        </li>
      </ul>
      <span class="navbar-text">
        <button
          class="btn btn-outline-primary text-uppercase"
          @click="login"
          v-if="!state.user.isAuthenticated"
        >
          Login
        </button>

        <div class="dropdown" v-else>
          <div
            class="dropdown-toggle"
            @click="state.dropOpen = !state.dropOpen"
          >
            <img
              :src="state.user.picture"
              alt="user photo"
              height="40"
              class="rounded"
            />
            <span class="text-light mx-3">{{ state.user.name }}</span>
          </div>
          <div
            class="dropdown-menu p-0 list-group w-100"
            :class="{ show: state.dropOpen }"
            @click="state.dropOpen = false"
          >
            <div class="list-group-item list-group-item-action hoverable" @click.stop="accountPush">
              Account
            </div>
            <div
              class="list-group-item list-group-item-action hoverable"
              @click="logout"
            >
              logout
            </div>
          </div>
        </div>
      </span>
    </div>
  </nav>
</template>

<script>
import { AuthService } from '../services/AuthService'
import { AppState } from '../AppState'
import { computed, reactive } from 'vue'
import { useRouter } from 'vue-router'
import Pop from '../utils/Notifier'

export default {
  setup() {
    const router = useRouter()
    const state = reactive({
      dropOpen: false,
      user: computed(() => AppState.user),
      account: computed(() => AppState.account)
    })
    return {
      state,
      router,
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      },
      async accountPush() {
        try {
          state.dropOpen = false
          router.push({ name: 'Profile', params: { id: state.account.id } })
        } catch (error) {
          Pop.toast('Failed to go to account page: ' + error, 'error')
        }
      }
    }
  }
}
</script>

<style scoped>
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
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
</style>
