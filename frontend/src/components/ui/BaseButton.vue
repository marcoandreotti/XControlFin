<template>
  <button
    :class="['base-button', variant, { loading: isLoading }]"
    :disabled="disabled || isLoading"
    @click="$emit('click', $event)"
  >
    <span v-if="isLoading" class="loader"></span>
    <slot v-else></slot>
  </button>
</template>

<script setup>
defineProps({
  variant: {
    type: String,
    default: 'primary' // primary, secondary, danger, ghost
  },
  disabled: {
    type: Boolean,
    default: false
  },
  isLoading: {
    type: Boolean,
    default: false
  }
})
defineEmits(['click'])
</script>

<style scoped>
.base-button {
  padding: 0.65rem 1.25rem;
  border-radius: 8px;
  font-weight: 500;
  border: none;
  cursor: pointer;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  transition: all 0.2s ease;
  font-family: inherit;
  font-size: 0.95rem;
}
.base-button:disabled {
  opacity: 0.6;
  cursor: not-allowed;
  transform: none !important;
}
.base-button:active:not(:disabled) {
  transform: scale(0.98);
}
.primary {
  background-color: var(--color-primary);
  color: #fff;
  box-shadow: 0 4px 14px 0 rgba(99, 102, 241, 0.39);
}
.primary:hover:not(:disabled) {
  background-color: var(--color-primary-hover);
  transform: translateY(-1px);
}
.secondary {
  background-color: var(--bg-element);
  color: var(--text-main);
  border: 1px solid var(--border-color);
}
.secondary:hover:not(:disabled) {
  filter: brightness(0.95);
}
.ghost {
  background: transparent;
  color: var(--text-muted);
}
.ghost:hover:not(:disabled) {
  background: var(--bg-element);
  color: var(--text-main);
}
.danger {
  background-color: var(--color-danger);
  color: #fff;
}
.danger:hover:not(:disabled) {
  filter: brightness(0.9);
}

.loader {
  width: 18px;
  height: 18px;
  border: 2px solid currentColor;
  border-bottom-color: transparent;
  border-radius: 50%;
  display: inline-block;
  box-sizing: border-box;
  animation: rotation 1s linear infinite;
}
@keyframes rotation {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
</style>
