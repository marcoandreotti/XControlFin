<template>
  <div class="input-wrapper">
    <label v-if="label" class="label" :for="computedId">{{ label }}</label>
    <div class="input-container">
      <component :is="icon" v-if="icon" class="icon" :size="18" />
      <input
        :id="computedId"
        :type="type"
        :value="modelValue"
        @input="$emit('update:modelValue', $event.target.value)"
        :placeholder="placeholder"
        :class="['base-input', { 'with-icon': icon, 'error': error }]"
        v-bind="$attrs"
      />
    </div>
    <span v-if="error" class="error-message">{{ error }}</span>
  </div>
</template>

<script setup>
import { computed } from 'vue'
const props = defineProps({
  modelValue: [String, Number],
  label: String,
  id: String,
  type: {
    type: String,
    default: 'text'
  },
  placeholder: String,
  error: String,
  icon: Object
})
defineEmits(['update:modelValue'])

const computedId = computed(() => props.id || Math.random().toString(36).substring(2, 9))
</script>

<style scoped>
.input-wrapper {
  display: flex;
  flex-direction: column;
  gap: 0.35rem;
  margin-bottom: 1rem;
  width: 100%;
}
.label {
  font-size: 0.85rem;
  font-weight: 500;
  color: var(--text-main);
}
.input-container {
  position: relative;
  display: flex;
  align-items: center;
  width: 100%;
}
.icon {
  position: absolute;
  left: 12px;
  color: var(--text-muted);
}
.base-input {
  width: 100%;
  padding: 0.65rem 1rem;
  border-radius: 8px;
  border: 1px solid var(--border-color);
  background: var(--bg-card);
  color: var(--text-main);
  font-family: inherit;
  font-size: 0.95rem;
  outline: none;
  transition: all 0.2s;
}
.with-icon {
  padding-left: 2.5rem;
}
.base-input:focus {
  border-color: var(--color-primary);
  box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.15);
}
.base-input.error {
  border-color: var(--color-danger);
}
.base-input.error:focus {
  box-shadow: 0 0 0 3px rgba(239, 68, 68, 0.15);
}
.error-message {
  font-size: 0.75rem;
  color: var(--color-danger);
  margin-top: 2px;
}
</style>
