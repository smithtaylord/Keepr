<template>
  <header>
    <Navbar />
  </header>
  <main>
    <router-view />
  </main>
  <Modal id="create-keep">
    <CreateKeep />
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
</template>

<script>
import { computed } from 'vue'
import { AppState } from './AppState'
import Navbar from './components/Navbar.vue'
import CreateKeep from './components/CreateKeep.vue'
import KeepDetails from './components/KeepDetails.vue'
import EditAccount from './components/EditAccount.vue'
import CreateVault from './components/CreateVault.vue'

export default {
  setup() {
    return {
      appState: computed(() => AppState)
    }
  },
  components: { Navbar, CreateKeep, KeepDetails, EditAccount, CreateVault }
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
