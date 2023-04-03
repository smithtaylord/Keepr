<template>
    <div class="modal-dialog modal-xl modal-fullscreen-md-down">
        <div class="modal-content" id="keep-details">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-12 col-md-6 p-0">
                        <div class="position-relative">
                            <img class="keep-img img-fluid rounded-start " :src="keep?.img" :alt="keep?.name"
                                v-on:error="onImageError">
                            <button type="button"
                                class="text-light font-i text-shadow position-absolute top-0 end-0 pe-4 pt-2 d-md-none"
                                data-bs-dismiss="modal" aria-label="Close">X</button>
                        </div>
                    </div>
                    <div
                        class="col-12 col-md-6 bg-body-bg rounded-end d-flex flex-column justify-content-between mobile-height">
                        <section class="d-flex justify-content-center mt-4 font-s text-primary">
                            <div class="me-2"> <i class="px-2 mdi mdi-eye"></i> {{ keep?.views }}</div>
                            <div class=""> <i class="px-2 mdi mdi-alpha-k-box-outline"></i>
                                {{ keep?.kept }}</div>
                        </section>
                        <section class="px-5 mobile-scroll">
                            <h1 class="text-center font-mo mb-3 pb-2">{{ keep?.name }}</h1>
                            <p class="text-start text-primary font-i ">{{ keep?.description }}</p>
                        </section>
                        <section class="mb-4 px-4 d-flex align-items-center justify-content-between">
                            <div v-if="account.id">
                                <div v-if="isVaultRoute">
                                    <button @click="removeFromVault()" class="m-2" title="remove keep from vault"
                                        data-bs-dismiss="modal"> <i class="mdi mdi-cancel ms-0 me-2 fs-4"></i> Remove
                                    </button>
                                </div>
                                <div v-else class="dropdown">
                                    <p class="fw-bold font-size selectable px-3 py-1 mt-3 home-btn bg-warning text-body-bg text-shadow"
                                        data-bs-toggle="dropdown" aria-expanded="false">
                                        Save to Vault <i class="mdi mdi-menu-down"></i></p>
                                    <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4 drop-border">
                                        <li v-if="myVaults.length == 0" class="ps-3 bg-primary fs-6">No Vaults Found</li>
                                        <div v-else v-for="v in myVaults">
                                            <li @click="addToVault(v.id)" class="px-2 py-1 selectable" title="add to vault"
                                                data-bs-dismiss="modal">{{ v.name }}</li>
                                        </div>
                                    </ul>
                                </div>
                            </div>
                            <div v-if="keep?.creatorId">
                                <router-link :to="{ name: 'Profile', params: { profileId: keep?.creatorId } }">
                                    <div class="d-flex" data-bs-dismiss="modal">
                                        <img class="prof-pic rounded-circle selectable" title="go to profile"
                                            :src="keep?.creator.picture" :alt="keep?.creator.name">
                                        <p class="fw-bold font-o font-size ms-3 mt-2 text-dark">{{ keep?.creator.name }}</p>
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
import { useRoute } from 'vue-router';

export default {
    setup() {
        const route = useRoute()
        return {
            account: computed(() => AppState.account),
            keep: computed(() => AppState.activeKeep),
            myVaults: computed(() => AppState.myVaults),
            isVaultRoute: computed(() => route.path.startsWith('/vault/')),
            onImageError() {
                event.target.src = "https://t4.ftcdn.net/jpg/02/84/64/51/360_F_284645131_hE2W3bbPxFBkk2aNqNyiTgLiraaiAuDh.jpg";
            },
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
            },
            async removeFromVault() {
                try {
                    const foundVK = AppState.vaultKeeps.find(vk => vk.id == AppState.activeKeep.id)
                    const vkId = foundVK.vaultKeepId
                    logger.log(vkId, '[vk id]')
                    if (await Pop.confirm('Are you sure you want to remove your keep from this vault?'))
                        await vaultKeepsService.removeFromVault(vkId)
                } catch (error) {
                    Pop.error(error, '[remove from vault]')
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

.font-size {
    font-size: 18pt;
}

// CHANGES TO BUTTON
button {
    font-size: 20px;
    color: #877A8F;
    font-family: 'Oxygen', sans-serif;
    font-weight: 800;
    cursor: pointer;
    position: relative;
    border: none;
    background: none;
    text-transform: uppercase;
    transition-timing-function: cubic-bezier(0.25, 0.8, 0.25, 1);
    transition-duration: 400ms;
    transition-property: color;
}

button:focus,
button:hover {
    color: #FF5977;
}

button:focus:after,
button:hover:after {
    width: 100%;
    left: 0%;
}

button:after {
    content: "";
    pointer-events: none;
    bottom: -2px;
    left: 50%;
    position: absolute;
    width: 0%;
    height: 2px;
    background-color: #FF5977;
    transition-timing-function: cubic-bezier(0.25, 0.8, 0.25, 1);
    transition-duration: 400ms;
    transition-property: width, left;
}

@media screen and (max-width: 768px) {
    .keep-img {
        height: 50vh;
        width: 100%;
        object-fit: cover;
        border-radius: 0 !important;
    }

    .prof-pic {
        height: 35px;
        width: 35px;
        object-fit: cover;
    }

    .font-size {
        font-size: 12pt;
    }

    .mobile-height {
        height: 50vh;
    }

    .mobile-scroll {
        overflow-y: auto;
    }

}
</style>