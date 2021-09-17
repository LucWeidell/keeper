<template>
  <div class="VaultCard">
    <div class="card p-0 action">
      <div class="card-body card-img p-0">
        <img class="w-100 h-100" src="http://placebeard.it/187x187" alt="" srcset="">
        <div class="card-img-overlay text-light d-flex justify-content-start align-items-end mb-1">
          <h4 class="shadower m-0">
            {{ vault.name }}
          </h4>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'VaultCard',
  props: {
    vault: {
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

</style>
