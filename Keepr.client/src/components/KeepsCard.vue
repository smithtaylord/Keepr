<template>
    <div v-if="keep.creator?.picture" class="component">
        <div class="position-relative">
            <div class="selectable" @click="setKeepActive(keep.id)" title="open keeps details" data-bs-toggle="modal"
                data-bs-target="#keep-details">
                <img class="keep-img img-fluid rounded keeps-box-shadow" :src="keep.img" :alt="keep.name"
                    v-on:error="onImageError">
            </div>
            <div class="position-absolute bottom-0 start-0 w-100 d-flex align-items-center">
                <div class="d-flex justify-content-between flex-grow-1 p-md-2 mx-2">
                    <p class="text-light text-shadow font-mo font-size">{{ keep.name }}</p>
                    <img v-if="path == '/'" class="profile-img rounded-circle" :src="keep.creator.picture"
                        :alt="keep.creator.name" :title="keep.creator.name">
                </div>
            </div>
            <div v-if="account.id == keep.creatorId && !isVaultRoute" class="position-absolute top-0 end-0 move-up">
                <p @click.stop="deleteKeep(keep.id)" class="rounded-circle bg-danger text-light selectable delete"
                    title="delete keep">
                    <span class="x">&times;</span>
                </p>
            </div>
        </div>
    </div>
</template>


<script>
import { computed } from 'vue';
import { keepsService } from '../services/KeepsService.js';
import Pop from '../utils/Pop.js';
import { AppState } from '../AppState.js';
import { useRoute } from 'vue-router';

export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup() {
        const route = useRoute()
        return {
            account: computed(() => AppState.account),
            isVaultRoute: computed(() => route.path.startsWith('/vault/')),
            path: computed(() => route.path),
            onImageError() {
                event.target.src = "https://t4.ftcdn.net/jpg/02/84/64/51/360_F_284645131_hE2W3bbPxFBkk2aNqNyiTgLiraaiAuDh.jpg";
            },
            async setKeepActive(id) {
                try {
                    AppState.activeKeep = null
                    await keepsService.getOneKeep(id)
                } catch (error) {
                    Pop.error(error, '[set keep active]')
                }
            },

            async deleteKeep(id) {
                try {
                    if (await Pop.confirm('Are you sure you want to remove your keep?'))
                        await keepsService.deleteKeep(id)
                } catch (error) {
                    Pop.error(error, '[delete keep]')
                }
            }
        }
    }
}
</script>


<style lang="scss" scoped>
.keep-img {
    width: 350px;
}

.profile-img {
    height: 40px;
    width: 40px;
    object-fit: cover;
}

.delete {
    width: 20px;
    height: 20px;
    display: flex;
    justify-content: center;
    align-items: center;

}

.x {
    font-size: 20px;
}

.move-up {
    transform: translate(5px, -5px);
}

.font-size {
    font-size: 24pt;
}

@media screen and (max-width: 768px) {

    .profile-img {
        height: 30px;
        width: 30px;
        object-fit: cover;
    }

    .font-size {
        font-size: 12pt;
    }
}
</style>