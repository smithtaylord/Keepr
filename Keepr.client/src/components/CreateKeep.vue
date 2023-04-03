<template>
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="d-flex justify-content-between px-4 pt-4">
                <h1 class="modal-title fs-3 fw-bold text-primary" id="create-keep">Add your keep</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="createKeep">
                <div class="input-container">
                    <input v-model="editable.name" type="text" id="keep-name" required maxlength="100" minlength="2">
                    <label for="name" class="label">Title</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.img" type="text" id="keep-image-url" required maxlength="500" minlength="10">
                    <label for="image-url" class="label">Image URL</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.description" type="text" id="keep-description" required maxlength="500"
                        minlength="2">
                    <label for="description" class="label">Keep Description</label>
                    <div class="underline"></div>
                </div>
                <div class="d-flex justify-content-end p-3">
                    <button type="submit" data-bs-dismiss="modal"
                        class="btn bg-dark selectable keeps-box-shadow">Create</button>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref } from 'vue'
import { logger } from '../utils/Logger.js'
import Pop from '../utils/Pop.js'
import { keepsService } from '../services/KeepsService.js'

export default {
    setup() {
        const editable = ref({})
        return {
            editable,
            async createKeep() {
                try {
                    const formData = editable.value
                    await keepsService.createKeep(formData)
                    editable.value = {}
                } catch (error) {
                    Pop.error(error, '[create keep]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>