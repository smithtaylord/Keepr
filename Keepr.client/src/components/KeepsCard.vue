<template>
    <div class="component">
        <div class="position-relative">
            <img class="keep-img  rounded keeps-box-shadow" :src="keep.img" :alt="keep.name">
            <div class=" position-absolute bottom-0 start-0 w-100 d-flex align-items-center">
                <div class="d-flex justify-content-between flex-grow-1 p-2 mx-2">
                    <p class="fs-5 text-light text-shadow font-mo">{{ keep.name }}</p>
                    <img class="profile-img rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name"
                        :title="keep.creator.name">
                </div>
            </div>
        </div>

    </div>
</template>


<script>
export default {
    props: {
        keep: {
            type: Object,
            required: true
        }
    },
    setup() {
        return {}
    }
}
</script>


<style lang="scss" scoped>
.keep-img {
    width: 300px;
}

.profile-img {
    height: 40px;
    width: 40px;
    object-fit: cover;
}
</style>