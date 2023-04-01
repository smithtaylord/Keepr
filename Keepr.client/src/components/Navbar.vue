<template>
  <nav class="nav bg-body-bg px-3 d-flex align-items-center justify-content-between nav-shadow">
    <div class="container-fluid">
      <div class="row">
        <div class="d-flex align-items-center col-4">
          <router-link class="" :to="{ name: 'Home' }">
            <p class="fw-bold fs-4 bg-info selectable px-3 py-1 home-btn ms-5 me-3 mt-3">Home</p>
          </router-link>
          <div class="dropdown">
            <p class="fw-bold fs-4 selectable px-3 py-1 mt-3 home-btn" data-bs-toggle="dropdown" aria-expanded="false">
              Create <i class="mdi mdi-menu-down"></i></p>
            <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4 drop-border">
              <li class="text-center selectable border-dark border-bottom pb-1" data-bs-toggle="modal"
                data-bs-target="#create-keep">new keep</li>
              <li class="text-center selectable pt-1">new vault</li>
            </ul>
          </div>
        </div>

        <div class="col-4">
          <div class="d-flex justify-content-center">
            <p class="font-mo custom-border my-2 ">
              the<br>
              keepr<br>
              co.
            </p>
          </div>
        </div>
        <!-- LOGIN COMPONENT HERE -->
        <div class="col-4 text-end d-flex align-items-center justify-content-end">
          <Login />
        </div>
      </div>

    </div>
  </nav>
</template>

<script>
import Login from './Login.vue'
export default {
  setup() {
    return {}
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
