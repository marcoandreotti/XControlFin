import { createRouter, createWebHistory } from 'vue-router'
import { useAuthStore } from '@/stores/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/login',
      name: 'login',
      component: () => import('../views/auth/Login.vue'),
      meta: { layout: 'AuthLayout', requiresAuth: false }
    },
    {
      path: '/',
      name: 'dashboard',
      component: () => import('../views/dashboard/Dashboard.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Dashboard' }
    },
    {
      path: '/releases',
      name: 'releases',
      component: () => import('../views/cruds/Releases.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Lançamentos' }
    },
    {
      path: '/planning',
      name: 'planning',
      component: () => import('../views/cruds/Plannings.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Planejamento Financeiro' }
    },
    {
      path: '/institutions',
      name: 'institutions',
      component: () => import('../views/cruds/Institutions.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Instituições Financeiras' }
    },
    {
      path: '/accounts',
      name: 'accounts',
      component: () => import('../views/cruds/UserAccounts.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Minhas Contas' }
    },
    {
      path: '/cost-centers',
      name: 'costCenters',
      component: () => import('../views/cruds/CostCenters.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Centros de Custo' }
    },
    {
      path: '/users',
      name: 'users',
      component: () => import('../views/cruds/Users.vue'),
      meta: { layout: 'MainLayout', requiresAuth: true, title: 'Usuários' }
    }
  ]
})

router.beforeEach((to, from, next) => {
  const authStore = useAuthStore()
  const isAuthenticated = authStore.isAuthenticated

  if (to.meta.requiresAuth && !isAuthenticated) {
    next({ name: 'login' })
  } else if (to.name === 'login' && isAuthenticated) {
    next({ name: 'dashboard' })
  } else {
    next()
  }
})

export default router
