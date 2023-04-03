<template>
  <header class="sticky-top d-none d-md-block">
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <Modal id="create-keep">
    <CreateKeep />
  </Modal>
  <Modal id="create-vault">
    <CreateVault />
  </Modal>
  <Modal id="edit-vault">
    <EditVault />
  </Modal>
  <Modal id="keep-details">
    <KeepDetails />
  </Modal>
  <Modal id="edit-account">
    <EditAccount />
  </Modal>
  <header class="sticky-bottom d-md-none">
    <Navbar />
  </header>
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeep from './components/CreateKeep.vue'
import KeepDetails from './components/KeepDetails.vue'
import EditAccount from './components/EditAccount.vue'
import CreateVault from './components/CreateVault.vue'
import EditVault from './components/EditVault.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeep, KeepDetails, EditAccount, CreateVault, EditVault }
}
</script>
<style lang="scss">
@import "./assets/scss/main.scss";

:root {
  --main-height: calc(100vh - 32px - 64px);
}


footer {
  display: grid;
  place-content: center;
  height: 32px;
}
</style>
