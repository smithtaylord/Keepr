<template>
  <nav class=" bg-body-bg px-md-3 d-flex align-items-center justify-content-between nav-shadow">
    <div class="container-fluid">
      <div class="row">
        <div class="d-flex align-items-center col-4 order-md-1 order-2">
          <router-link class="" :to="{ name: 'Home' }">
            <p class="fw-bold fs-4 bg-info selectable px-2 px-md-3 py-1 home-btn ms-md-5 me-3 mt-3 d-none d-md-block">Home
            </p>
          </router-link>
          <div v-if="account.id" class="dropdown">
            <p class="fw-bold fs-4 selectable px-1 px-md-3 py-1 mt-3 home-btn ms-1" data-bs-toggle="dropdown"
              aria-expanded="false">
              Create <i class="mdi mdi-menu-down"></i></p>
            <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4 drop-border">
              <li class="text-center selectable border-dark border-bottom pb-1" data-bs-toggle="modal"
                data-bs-target="#create-keep">new keep</li>
              <li class="text-center selectable pt-1" data-bs-toggle="modal" data-bs-target="#create-vault">new vault</li>
            </ul>
          </div>
        </div>

        <div class="col-4 order-md-2 order-1">
          <div class="d-flex justify-content-center mt-1 mt-md-0">
            <router-link class="" :to="{ name: 'Home' }">
              <p class="font-mo text-dark custom-border my-2 ">
                the<br>
                keepr<br>
                co.
              </p>
            </router-link>
          </div>
        </div>
        <!-- LOGIN COMPONENT HERE -->
        <div class="col-4 text-end d-flex align-items-center justify-content-end order-md-3 order-3">
          <Login />
        </div>
      </div>

    </div>
  </nav>
</template>

<script>

import { computed } from 'vue';
import Login from './Login.vue'
import { AppState } from '../AppState.js';
export default {
  setup() {
    return {
      account: computed(() => AppState.account)
    }
  },
  components: { Login }
}
</script>

<style scoped>
.home-btn {
  border-radius: 15px;
}

.nav {
  height: 90px;
}

.custom-border {
  border: 1.66787px solid #2D2D2D;
  border-radius: 4.76534px;
  padding-left: .3em;
  padding-right: 1.3em;
}

.nav-shadow {
  box-shadow: 0px 0px 9px rgba(0, 0, 0, 0.25);
}

.drop-border {
  border: 1px solid #2D2D2D;
  border-radius: 10px;
}
</style>
