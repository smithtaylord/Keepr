<template>
    <div v-if="profile" class="container-fluid">
        <div class="row">
            <div class="col-8 m-auto">
                <img class="img-fluid cover-img rounded keeps-box-shadow mt-5" :src="profile.coverImg" :alt="profile.name">
                <div class="text-center">
                    <img class="rounded-circle img-fluid profile-pic" :src="profile.picture" :alt="profile.name">
                </div>
                <div class="font-up">
                    <h1 class="text-center fw-bold font-o mb-3">NAME: {{ profile.name }}</h1>
                    <p class="text-center font-o fs-4">{{ profileVaults.length }} Vaults | {{ profileKeeps.length }} Keeps
                    </p>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
import { watchEffect } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import Pop from '../utils/Pop.js';
import { profilesService } from '../services/ProfilesService.js';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';
import { vaultsService } from '../services/VaultsService.js';
import { keepsService } from '../services/KeepsService.js';

export default {
    setup() {
        const route = useRoute()
        const router = useRouter()

        async function getProfile() {
            try {
                await profilesService.getProfile(route.params.profileId)
            } catch (error) {
                Pop.error(error, '[get profile]')
                router.push({ name: 'Home' })
            }
        }

        async function getProfileKeeps() {
            try {
                await keepsService.getProfileKeeps(route.params.profileId)
            } catch (error) {
                Pop.error(error, '[get keeps]')
            }
        }

        async function getProfileVaults() {
            try {
                await vaultsService.getProfileVaults(route.params.profileId)
            } catch (error) {
                Pop.error(error, '[get vaults]')
            }
        }

        watchEffect(() => {
            if (route.params.profileId) {
                getProfile()
                getProfileVaults()
                getProfileKeeps()
            }
        })

        return {
            profile: computed(() => AppState.profile),
            profileKeeps: computed(() => AppState.profileKeeps),
            profileVaults: computed(() => AppState.profileVaults)
        }
    }
}
</script>


<style lang="scss" scoped>
.cover-img {
    height: 400px;
    width: 100%;
    object-fit: cover;
}

.profile-pic {
    height: 150px;
    width: 150px;
    object-fit: cover;
    border: 1px solid #FFFFFF;
    box-shadow: 0px 4px 4px rgba(0, 0, 0, 0.25);
    transform: translateY(-75px);
}

.font-up {
    transform: translateY(-40px);
}
</style>