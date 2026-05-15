<template>
  <div class="login-wrapper">
    <div class="text-center mb-6">
      <h1 class="brand">XControlFin</h1>
      <p class="subtitle">Faça login para gerenciar suas finanças</p>
    </div>
    <form @submit.prevent="handleLogin" class="login-form">
      <BaseInput
        v-model="form.email"
        label="Email"
        placeholder="Seu email cadastrado"
        :icon="User"
        id="email"
        type="email"
        required
      />
      <BaseInput
        v-model="form.password"
        label="Senha"
        type="password"
        placeholder="Sua senha"
        :icon="Lock"
        id="password"
        required
      />
      
      <div v-if="authStore.error" class="error-alert">
        {{ authStore.error }}
      </div>

      <BaseButton type="submit" :isLoading="authStore.loading" class="mt-4 w-full">
        Entrar na plataforma
      </BaseButton>
    </form>
  </div>
</template>

<script setup>
import { reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import BaseInput from '@/components/ui/BaseInput.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import { User, Lock } from 'lucide-vue-next'

const router = useRouter()
const authStore = useAuthStore()

const form = reactive({
  email: '',
  password: ''
})

const handleLogin = async () => {
  if (!form.email || !form.password) return
  const success = await authStore.login(form)
  if (success) {
    router.push('/')
  }
}
</script>

<style scoped>
.brand {
  font-size: 1.8rem;
  font-weight: 700;
  color: var(--color-primary);
  margin-bottom: 0.5rem;
}
.subtitle {
  color: var(--text-muted);
  font-size: 0.95rem;
}
.mt-4 { margin-top: 1rem; }
.mb-6 { margin-bottom: 1.5rem; }
.w-full { width: 100%; display: flex; }
.text-center { text-align: center; }

.login-form {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.error-alert {
  padding: 0.75rem;
  border-radius: 8px;
  background-color: rgba(239, 68, 68, 0.1);
  color: var(--color-danger);
  font-size: 0.85rem;
  margin-top: 0.5rem;
  border: 1px solid rgba(239, 68, 68, 0.2);
}
</style>
