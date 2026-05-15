import { defineStore } from 'pinia'
import api from '@/services/api'

export const useAuthStore = defineStore('auth', {
  state: () => ({
    user: JSON.parse(localStorage.getItem('user')) || null,
    token: localStorage.getItem('token') || null,
    loading: false,
    error: null
  }),
  getters: {
    isAuthenticated: (state) => !!state.token
  },
  actions: {
    async login(loginData) {
      this.loading = true
      this.error = null
      try {
        const response = await api.post('/auth/login', loginData)
        if (response.data && response.data.accessToken) {
          this.token = response.data.accessToken
          this.user = { 
            name: loginData.email, 
            id: null
          }
          localStorage.setItem('token', this.token)
          localStorage.setItem('user', JSON.stringify(this.user))
          return true
        }
      } catch (err) {
        this.error = err.response?.data?.message || 'Erro ao realizar login.'
        return false
      } finally {
        this.loading = false
      }
    },
    logout() {
      this.token = null
      this.user = null
      localStorage.removeItem('token')
      localStorage.removeItem('user')
    }
  }
})
