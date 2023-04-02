<template>
    <div v-if="vault" class="container-fluid">
        <div class="row">
            <div class="col-4 m-auto">
                <div class="position-relative">
                    <div>
                        <img class="vault-pic img-fluid rounded mt-4" :src="vault.img" :alt="vault.name">
                    </div>
                    <div class="position-absolute bottom-0 w-100">
                        <div class="text-center pb-3">
                            <h1 class="text-light text-shadow font-q">{{ vault.name }}</h1>
                            <p class="text-light text-shadow font-q fs-5">by {{ vault.creator.name }}</p>
                        </div>
                    </div>
                </div>
                <div class="text-end px-2 dropdown">
                    <i class="mdi mdi-dots-horizontal text-dark selectable fs-3" title="edit or delete vault"
                        data-bs-toggle="dropdown" aria-expanded="false"></i>
                    <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4  p-0">
                        <li class="text-center selectable border-dark border-bottom pb-1 pt-1" data-bs-toggle="modal"
                            data-bs-target="#edit-vault">edit vault</li>
                        <li @click="deleteVault" class="text-center selectable pt-1 bg-danger pb-1 rounded-bottom">
                            delete vault</li>
                    </ul>

                </div>
                <div class="row">
                    <div class="col-4 m-auto">
                        <div class="text-center">
                            <p v-if="vaultKeeps.length == 1" class="keeps-count bg-secondary font-o fs-4 fw-bold p-1">{{
                                vaultKeeps.length }} Keep</p>
                            <p v-else class="keeps-count bg-secondary font-o fs-4 fw-bold p-1">{{ vaultKeeps.length }} Keeps
                            </p>

                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-8 m-auto">
                <section class="bricks">
                    <div v-for="k in vaultKeeps">
                        <KeepsCard :keep="k" />
                    </div>
                </section>
            </div>
        </div>
    </div>
</template>


<script>
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop.js';
import { vaultsService } from '../services/VaultsService.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';
import { watchEffect, computed } from 'vue';
import { AppState } from '../AppState.js';

export default {
    setup() {
        const route = useRoute()
        const router = useRouter()
        async function getVault() {
            try {
                await vaultsService.getVault(route.params.vaultId)
            } catch (error) {
                Pop.error(error, '[get vault]')
                router.push({ name: "Home" })
            }
        }
        async function getVaultKeeps() {
            try {
                await vaultKeepsService.getVaultKeeps(route.params.vaultId)
            } catch (error) {
                Pop.error(error, '[get vault keeps]')
            }
        }

        watchEffect(() => {
            if (route.params.vaultId) {
                getVault()
                getVaultKeeps()
            }
        })
        return {
            vault: computed(() => AppState.vault),
            vaultKeeps: computed(() => AppState.vaultKeeps),

            async deleteVault() {
                try {
                    if (await Pop.confirm('Are you sure you want to remove your vault?'))
                        await vaultsService.deleteVault(route.params.vaultId)
                    Pop.toast("Vault has been deleted", 'success', 'top-end', 3000)
                    router.push({ name: "Home" })
                } catch (error) {
                    Pop.error(error, '[delete vault]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.vault-pic {
    height: 250px;
    width: 100%;
    object-fit: cover;
}

.keeps-count {
    border-radius: 15px;
}

.bricks {
    columns: 200px;
    column-gap: .5em;

    &>div {
        margin-top: 1em;
        display: inline-block;
    }
}
</style>