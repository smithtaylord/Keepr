<template>
    <div v-if="keep.creator.picture" class="component">
        <div class="position-relative">
            <img class="keep-img rounded keeps-box-shadow" :src="keep.img" :alt="keep.name">
            <div class="position-absolute bottom-0 start-0 w-100 d-flex align-items-center">
                <div class="d-flex justify-content-between flex-grow-1 p-2 mx-2">
                    <p class="fs-5 text-light text-shadow font-mo">{{ keep.name }}</p>
                    <img class="profile-img rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name"
                        :title="keep.creator.name">
                </div>
            </div>
            <div v-if="account.id == keep.creatorId" class="position-absolute top-0 end-0 move-up">
                <p @click="deleteKeep(keep.id)" class="rounded-circle bg-danger text-light selectable delete"
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

export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {
            account: computed(() => AppState.account),

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
</style>