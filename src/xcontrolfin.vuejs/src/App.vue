<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'
import AuthLayout from '@/layouts/AuthLayout.vue'
import MainLayout from '@/layouts/MainLayout.vue'

const route = useRoute()

const layoutComponent = computed(() => {
  if (route.meta.layout === 'AuthLayout') return AuthLayout
  if (route.meta.layout === 'MainLayout') return MainLayout
  return 'div' // fallback
})
</script>

<template>
  <component :is="layoutComponent" v-if="route.matched.length">
    <router-view />
  </component>
</template>

<style>
/* Estilos globais adicionais se necessário */
</style>
