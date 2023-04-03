<template>
    <div class="modal-dialog">
        <div class="modal-content ">
            <div class="d-flex justify-content-between px-4 pt-4">
                <h1 class="modal-title fs-3 fw-bold text-primary" id="create-vault">Add your Vault</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="createVault">
                <div class="input-container">
                    <input v-model="editable.name" type="text" id="vault-name" required maxlength="100" minlength="2">
                    <label for="name" class="label">Title</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.img" type="url" id="vault-image-url" required maxlength="500" minlength="10">
                    <label for="image-url" class="label">Image URL</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.description" type="text" id="vault-description" required maxlength="500"
                        minlength="2">
                    <label for="description" class="label">Vault Description</label>
                    <div class="underline"></div>
                </div>
                <div class="d-flex justify-content-between align-items-center px-4 pb-3 mx-4">
                    <div>
                        <small class="text-primary">Private Vaults can only be seen by you</small>
                        <div class="d-flex">
                            <div class="cntr mt-2">
                                <input v-model="editable.isPrivate" type="checkbox" id="cbx" class="hidden-xs-up">
                                <label for="cbx" class="cbx"></label>
                            </div>
                            <p class="ms-3 mt-2 font-o fw-bold fs-5">Make Vault Private?</p>
                        </div>
                    </div>
                    <div>
                        <button type="submit" data-bs-dismiss="modal"
                            class="btn bg-dark selectable keeps-box-shadow">Create</button>
                    </div>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { vaultsService } from '../services/VaultsService.js'


export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createVault() {
                try {
                    const formData = editable.value
                    logger.log(formData)
                    if (formData.isPrivate == null) {
                        formData.isPrivate = false
                    }
                    logger.log(formData)
                    await vaultsService.createVault(formData)
                    Pop.toast('Vault Created!', 'success', 'top-end', 3000)
                    editable.value = {}
                } catch (error) {
                    Pop.error(error, '[create vault]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>