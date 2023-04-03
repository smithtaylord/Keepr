<template>
  <span class="navbar-text">
    <button class="btn selectable text-primary font-i fs-5" @click="login" v-if="!user.isAuthenticated">
      Login
    </button>
    <div v-else>
      <div class="dropdown dropstart my-2 my-lg-0">
        <div type="button" class=" border-0 selectable no-select" data-bs-toggle="dropdown" aria-expanded="false">
          <div v-if="account.picture || user.picture">
            <img :src="account.picture || user.picture" alt="account photo" height="64" class="rounded-circle" />
          </div>
        </div>
        <div class="dropdown-menu dropdown-menu-lg-left p-0 bg-secondary" aria-labelledby="authDropdown">
          <div class="list-group">
            <router-link :to="{ name: 'Profile', params: { profileId: account?.id } }">
              <div class="list-group-item dropdown-item list-group-item-action bg-secondary fw-bold font-o fs-5">
                My Profile
              </div>
            </router-link>
            <div class="list-group-item dropdown-item list-group-item-action fw-bold font-o fs-5 selectable bg-danger"
              @click="logout">
              <i class="mdi mdi-logout"></i>
              logout
            </div>
          </div>
        </div>
      </div>
    </div>
  </span>
</template>

<script>
import { computed } from 'vue'
import { AppState } from '../AppState'
import { AuthService } from '../services/AuthService'
export default {
  setup() {
    return {
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      async login() {
        AuthService.loginWithPopup()
      },
      async logout() {
        AuthService.logout({ returnTo: window.location.origin })
      }
    }
  }
}
</script>

<style lang="scss" scoped></style>
