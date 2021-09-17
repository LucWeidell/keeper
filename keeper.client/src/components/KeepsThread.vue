<template>
  <div class="KeepsThread my-3">
    <div class="d-flex">
      <h2>Keeps:</h2>
      <i class="mdi mdi-plus-thick mdi-24px action" style="color: green;"></i>
    </div>
    <div class="mt-3 card-columns flex-grow-1">
      <KeepCard v-for="k in keeps" :key="k.id" :keep="k" :showimg="false" />
    </div>
  </div>
</template>

<script>
export default {
  name: 'KeepsThread',
  props: {
    keeps: {
      type: Array,
      required: true
    }
  },
  setup(props) {
    return {}
  }
}
</script>

<style lang="scss" scoped>

</style>
