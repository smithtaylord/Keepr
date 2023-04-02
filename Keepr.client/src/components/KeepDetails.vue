<template>
    <div class="modal-dialog modal-xl ">
        <div class="modal-content" id="keep-details">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-6 p-0">
                        <img class="keep-img img-fluid rounded-start " :src="keep?.img" :alt="keep?.name">
                    </div>
                    <div class="col-6 bg-body-bg rounded-end d-flex flex-column justify-content-between">
                        <section class="d-flex justify-content-center mt-4 font-s text-primary">
                            <div class="me-2"> <i class="px-2 mdi mdi-eye"></i> {{ keep?.views }}</div>
                            <div class=""> <i class="px-2 mdi mdi-alpha-k-box-outline"></i>
                                {{ keep?.kept }}</div>
                        </section>
                        <section class="px-5">
                            <h1 class="text-center font-mo mb-3 pb-2">{{ keep?.name }}</h1>
                            <p class="text-start text-primary font-i">{{ keep?.description }}</p>
                        </section>
                        <section class="mb-4 px-4 d-flex align-items-center justify-content-between">
                            <div class="dropdown">
                                <p class="fw-bold fs-5 selectable px-3 py-1 mt-3 home-btn bg-warning text-body-bg text-shadow"
                                    data-bs-toggle="dropdown" aria-expanded="false">
                                    Save to Vault <i class="mdi mdi-menu-down"></i></p>
                                <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4 drop-border">
                                    <div v-for="v in myVaults">
                                        <li @click="addToVault(v.id)" class="ps-3 selectable" title="add to vault"
                                            data-bs-dismiss="modal">{{ v.name }}</li>
                                    </div>
                                </ul>
                            </div>
                            <div v-if="keep?.creatorId">
                                <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                                    <div class="d-flex" data-bs-dismiss="modal">
                                        <img class="prof-pic rounded-circle selectable" title="go to profile"
                                            :src="keep?.creator.picture" :alt="keep?.creator.name">
                                        <p class="fw-bold font-o fs-5 ms-3 mt-2 text-dark">{{ keep?.creator.name }}</p>
                                    </div>
                                </router-link>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { AppState } from '../AppState.js';
import Pop from '../utils/Pop.js';
import { logger } from '../utils/Logger.js';
import { vaultKeepsService } from '../services/VaultKeepsService.js';

export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            myVaults: computed(() => AppState.myVaults),
            async addToVault(vaultId) {
                try {
                    const vkData = {
                        vaultId,
                        keepId: AppState.activeKeep.id
                    };
                    logger.log('[vault keep data]', vkData)
                    await vaultKeepsService.createVaultKeep(vkData)
                    Pop.toast('Keep Added', 'success', 'top-end', 3000)
                } catch (error) {
                    Pop.error(error, '[add to vault]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.keep-img {
    height: 60vh;
    width: 100%;
    object-fit: cover;
}

.prof-pic {
    height: 50px;
    width: 50px;
    object-fit: cover;
}

.home-btn {
    border-radius: 15px;
}
</style>