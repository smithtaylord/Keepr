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
                                <p class="fw-bold fs-4 selectable px-3 py-1 mt-3 home-btn" data-bs-toggle="dropdown"
                                    aria-expanded="false">
                                    Create <i class="mdi mdi-menu-down"></i></p>
                                <ul class="dropdown-menu bg-secondary fw-bold font-o fs-4 drop-border">
                                    <li class="">new keep</li>
                                    <li class="">new vault</li>
                                </ul>
                            </div>
                            <div class="">
                                <img class="prof-pic rounded-circle selectable" title="go to profile"
                                    :src="keep?.creator.picture" :alt="keep?.creator.name">
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

export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            myVaults: computed(() => AppState.myVaults)
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
</style>