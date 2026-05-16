<template>
  <div class="base-card glass">
    <div v-if="$slots.header || title" class="card-header">
      <slot name="header">
        <h3 class="card-title">{{ title }}</h3>
      </slot>
    </div>
    <div class="card-body" :style="{ padding: noPadding ? '0' : '1.5rem' }">
      <slot></slot>
    </div>
    <div v-if="$slots.footer" class="card-footer">
      <slot name="footer"></slot>
    </div>
  </div>
</template>

<script setup>
defineProps({
  title: String,
  noPadding: {
    type: Boolean,
    default: false
  }
})
</script>

<style scoped>
.base-card {
  display: flex;
  flex-direction: column;
  overflow: hidden;
  width: 100%;
}
.card-header {
  padding: 1.25rem 1.5rem;
  border-bottom: 1px solid var(--border-color);
  display: flex;
  align-items: center;
  justify-content: space-between;
}
.card-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: var(--text-main);
  margin: 0;
}
.card-body {
  flex: 1;
}
.card-footer {
  padding: 1rem 1.5rem;
  border-top: 1px solid var(--border-color);
  background-color: rgba(0,0,0,0.02);
}
</style>
