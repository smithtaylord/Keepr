<template>
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="d-flex justify-content-between px-4 pt-4">
                <h1 class="modal-title fs-3 fw-bold text-primary" id="edit-account">Edit Account</h1>
                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <form @submit.prevent="editAccount">
                <div class="input-container">
                    <input v-model="editable.name" type="text" id="name" required maxlength="100" minlength="2">
                    <label for="name" class="label">Name</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.picture" type="text" id="picture" required maxlength="500" minlength="10">
                    <label for="picture" class="label">Profile Picture</label>
                    <div class="underline"></div>
                </div>
                <div class="input-container">
                    <input v-model="editable.coverImg" type="text" id="coverImg" required maxlength="500" minlength="2">
                    <label for="coverImg" class="label">Cover Image</label>
                    <div class="underline"></div>
                </div>
                <div class="d-flex justify-content-end p-3">
                    <button type="submit" data-bs-dismiss="modal"
                        class="btn bg-dark selectable keeps-box-shadow">Save</button>
                </div>
            </form>
        </div>
    </div>
</template>


<script>
import { ref, watchEffect } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { accountService } from '../services/AccountService.js';

export default {
    setup() {
        const editable = ref({})
        watchEffect(() => {
            if (AppState.account != null) {
                editable.value = { ...AppState.account }
            } else {
                editable.value = {}
            }
        })
        return {
            editable,

            async editAccount() {
                try {
                    const formData = editable.value
                    await accountService.editAccount(formData)
                } catch (error) {
                    Pop.error(error, '[edit account]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped></style>