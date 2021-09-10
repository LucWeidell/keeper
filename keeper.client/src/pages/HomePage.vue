<template>
  <div class="HomePage container-fluid">
    <div class="row masonry">
      <div class="masonry-item col" style="background-image: http://placebeard.it/200x300; background-size: auto;">
        <!-- <img class="masonry-item" src="http://placebeard.it/200x300" style="background-image: http://placebeard.it/200x300; background-size: auto;" alt=""> -->
      </div>
      <div class="masonry-item">
        <img class="masonry-content" src="http://placebeard.it/200x300" style="background-image: http://placebeard.it/200x400; background-size: auto;" alt="">
      </div>
      <div class="masonry-item">
        <img class="masonry-content" src="http://placebeard.it/200x400" style="background-image: http://placebeard.it/200x500; background-size: auto;" alt="">
      </div>
      <div class="masonry-item">
        <img class="masonry-content" src="http://placebeard.it/200x500" style="background-image: http://placebeard.it/200x100; background-size: auto;" alt="">
      </div>
      <div class="masonry-item">
        <!-- <img class="masonry-content" src="http://placebeard.it/400x600" alt=""> -->
      </div>
      <div class="masonry-item">
        <!-- <img class="masonry-content" src="http://placebeard.it/300x500" alt=""> -->
      </div>
      <div class="masonry-item">
        <!-- <img class="masonry-content" src="http://placebeard.it/200x300" alt=""> -->
      </div>
      <div class="masonry-item">
        <!-- <img class="masonry-content" src="http://placebeard.it/200x300" alt=""> -->
      </div>
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
// import waitForImages from '../utils/Masonry'
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import Pop from '../utils/Notifier'
export default {
  name: 'HomePage',
  setup() {
    onMounted(async() => {
      try {
        waitForImages()
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>

<style lang="scss" scoped>
 :root {
    counter-reset: masonry;
  }
  .masonry {
    display: grid;
    grid-gap: 10px;
    // grid-template-columns: min-content min-content;
    grid-template-columns: repeat(auto-fill, minmax(200px,1fr));
    // grid-auto-rows: auto;
    grid-auto-rows: minmax(30px, auto);
  }

  .masonry-item {
    border-radius: 5px;
  }

  .masonry-item {
     background-color: #eee;
     border-radius: 5px;
     overflow: clip;
  }

  .masonry-item,
  .masonry-item img {
     position: relative;
    //  height: 100%;

  }

  .masonry-item:after {
    font-weight: bold;
    background-color: rgba(0, 0, 0, .5);
    content: counter(masonry);
    counter-increment: masonry;
    position: absolute;
    top: 0;
    left: 0;
    height: 100%;
    width: 100%;
    color: white;
    display: flex;
    justify-content: center;
    align-items: center;
    transition: all .1s ease-in;
  }
    .masonry-item:hover:after {
    font-size: 30px;
    background-color: rgba(0, 0, 0, .75);
  }
</style>
