<template>
    <div class="modal-dialog">
        <div class="modal-content ">
            <div class="d-flex justify-content-between px-4 pt-4">
                <h1 class="modal-title fs-3 fw-bold text-primary" id="edit-vault">Edit Vault</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="editVault">
                <div class="input-container">
                    <input v-model="editable.name" type="text" id="edit-vault-name" required maxlength="100" minlength="2">
                    <label for="name" class="label">Title</label>
                    <div class="underline"></div>
                </div>
                <div class="d-flex justify-content-between align-items-center px-4 pb-3 mx-4">
                    <div>
                        <small class="text-primary">Private Vaults can only be seen by you</small>
                        <div class="d-flex">
                            <div class="cntr mt-2">
                                <input v-model="editable.isPrivate" type="checkbox" id="cbx-2" class="hidden-xs-up">
                                <label for="cbx-2" class="cbx-2"></label>
                            </div>
                            <p class="ms-3 mt-2 font-o fw-bold fs-5">Make Vault Private?</p>
                        </div>
                    </div>
                    <div>
                        <button type="submit" data-bs-dismiss="modal"
                            class="btn bg-dark selectable keeps-box-shadow">Save</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref, watchEffect } from 'vue'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'
import { AppState } from '../AppState.js'


export default {
    setup() {
        const editable = ref({})
        watchEffect(() => {
            if (AppState.vault != null) {
                editable.value = { ...AppState.vault }
            } else {
                editable.value = {}
            }
        })
        return {
            editable,
            async editVault() {
                try {
                    const formData = editable.value
                    formData.vaultId = AppState.vault.id
                    await vaultsService.editVault(formData)
                    editable.value = {}
                } catch (error) {
                    Pop.error(error, '[edit vault]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>