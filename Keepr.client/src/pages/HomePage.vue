<template>
  <div class="mx-2 mx-md-5 my-3 text-center">
    <section class="bricks">
      <div v-for="k in keeps">
        <KeepsCard :keep="k" />
      </div>
    </section>
  </div>
</template>

<script>
import Pop from '../utils/Pop.js';
import { keepsService } from '../services/KeepsService.js'
import { computed, onMounted } from 'vue';
import { AppState } from '../AppState.js';
import KeepsCard from '../components/KeepsCard.vue';

export default {
  setup() {
    async function getAllKeeps() {
      try {
        await keepsService.getAllKeeps();
      }
      catch (error) {
        Pop.error(error, "[get all keeps]");
      }
    }
    onMounted(() => {
      getAllKeeps();
    });
    return {
      keeps: computed(() => AppState.allKeeps)
    };
  },
  components: { KeepsCard }
}
</script>

<style scoped lang="scss">
.bricks {
  columns: 350px;
  column-gap: .5em;

  &>div {
    margin-top: 1em;
    display: inline-block;
  }
}

@media screen and (max-width: 768px) {

  .bricks {
    columns: 2;
    column-gap: .5em;

    &>div {
      margin-top: 1em;
      display: inline-block;
    }
  }

}
</style>
