<template>
  <div class="HomePage container-fluid">
    <div class="row masonry">
      <div class="col-md-3 masonry-item">
        <img class="masonry-item" src="'http://placebeard.it/200x300'" alt="">
      </div>
      <div class="col masonry-item">
        <img class="masonry-content" src="'http://placebeard.it/200x300'" alt="">
      </div>
      <div class="col masonry-item">
        <img class="masonry-content" src="'http://placebeard.it/200x300'" alt="">
      </div>
      <div class="col masonry-item">
        <img class="masonry-content" src="'http://placebeard.it/200x300'" alt="">
      </div>
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
    </div>
  </div>
</template>

<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  name: 'HomePage',
  setup() {
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
    grid-template-columns: repeat(auto-fill, minmax(200px,1fr));
    grid-auto-rows: 0;
  }

  .masonry-item {
    border-radius: 5px;
  }

  .masonry-item {
     background-color: #eee;
     border-radius: 5px;
     overflow: hidden;
  }

  .masonry-item,
  .masonry-item img {
     position: relative;
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
</style>
