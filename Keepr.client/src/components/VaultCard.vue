<template>
    <div class="component">
        <div class="position-relative selectable" title="open vault page">
            <img class="img-fluid vault-img mb-3 rounded keeps-box-shadow" :src="vault.img" :alt="vault.name">
            <div class="position-absolute bottom-0 start-0 w-100">
                <div class="px-3 d-flex justify-content-between align-items-end text-light text-shadow ">
                    <p class="font-q fs-4">{{ vault.name }}</p>
                    <i v-if="account.id == profile.id && vault.isPrivate" class="mdi mdi-lock mb-4"></i>
                </div>
            </div>
        </div>

    </div>
</template>


<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
    props: {
        vault: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile)
        }
    }
}
</script>


<style lang="scss" scoped>
.vault-img {
    height: 150px;
    width: 100%;
    object-fit: cover;
}
</style>