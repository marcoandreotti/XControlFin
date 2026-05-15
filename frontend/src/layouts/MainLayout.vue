<template>
  <div class="main-layout">
    <aside :class="['sidebar glass', { 'collapsed': isCollapsed }]">
      <div class="sidebar-header">
        <h2 class="brand-text" v-if="!isCollapsed">XControlFin</h2>
        <h2 class="brand-text" v-else>XC</h2>
        <button class="toggle-btn" @click="isCollapsed = !isCollapsed">
          <Menu v-if="isCollapsed" :size="20"/>
          <X v-else :size="20" />
        </button>
      </div>
      <nav class="sidebar-nav">
        <router-link to="/" class="nav-item">
          <LayoutDashboard :size="20" />
          <span v-if="!isCollapsed">Dashboard</span>
        </router-link>
        <router-link to="/releases" class="nav-item">
          <CreditCard :size="20" />
          <span v-if="!isCollapsed">Lançamentos</span>
        </router-link>
        <router-link to="/planning" class="nav-item">
          <Target :size="20" />
          <span v-if="!isCollapsed">Planejamento</span>
        </router-link>
        <router-link to="/institutions" class="nav-item">
          <Landmark :size="20" />
          <span v-if="!isCollapsed">Instituições</span>
        </router-link>
        <router-link to="/accounts" class="nav-item">
          <Wallet :size="20" />
          <span v-if="!isCollapsed">Minhas Contas</span>
        </router-link>
        <router-link to="/cost-centers" class="nav-item">
          <FolderTree :size="20" />
          <span v-if="!isCollapsed">Centro de Custo</span>
        </router-link>
        <router-link to="/users" class="nav-item">
          <Users :size="20" />
          <span v-if="!isCollapsed">Usuários</span>
        </router-link>
      </nav>
    </aside>
    
    <div class="main-content">
      <header class="topbar glass">
        <div class="header-left">
          <h2 class="page-title">{{ $route.meta.title || '' }}</h2>
        </div>
        <div class="header-right">
           <button @click="toggleTheme" class="icon-btn" title="Alternar Tema">
             <Moon v-if="!isDark" :size="20" />
             <Sun v-else :size="20" />
           </button>
           <button @click="handleLogout" class="icon-btn logout-btn" title="Sair">
             <LogOut :size="20"/>
           </button>
        </div>
      </header>
      <main class="page-container fade-in">
        <slot></slot>
      </main>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { useAuthStore } from '@/stores/auth'
import { 
  Menu, X, LayoutDashboard, CreditCard, Target, 
  Landmark, Wallet, FolderTree, Users, 
  Moon, Sun, LogOut 
} from 'lucide-vue-next'

const isCollapsed = ref(false)
const isDark = ref(false)
const router = useRouter()
const authStore = useAuthStore()

onMounted(() => {
  const theme = localStorage.getItem('theme') || 'light'
  if (theme === 'dark') {
    isDark.value = true
    document.documentElement.setAttribute('data-theme', 'dark')
  }
})

const toggleTheme = () => {
  isDark.value = !isDark.value
  const theme = isDark.value ? 'dark' : 'light'
  document.documentElement.setAttribute('data-theme', theme)
  localStorage.setItem('theme', theme)
}

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}
</script>

<style scoped>
.main-layout {
  display: flex;
  height: 100vh;
  overflow: hidden;
  background-color: var(--bg-body);
}

.sidebar {
  width: var(--sidebar-w);
  height: 100%;
  display: flex;
  flex-direction: column;
  transition: width 0.3s ease;
  border-radius: 0;
  border-right: 1px solid var(--border-color);
  z-index: 10;
}
.sidebar.collapsed {
  width: 80px;
}

.sidebar-header {
  height: var(--header-h);
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1.25rem;
  border-bottom: 1px solid var(--border-color);
}
.brand-text {
  font-size: 1.2rem;
  font-weight: 700;
  color: var(--color-primary);
  margin: 0;
}
.toggle-btn {
  background: transparent;
  border: none;
  cursor: pointer;
  color: var(--text-muted);
  display: flex;
  align-items: center;
  padding: 0.25rem;
}
.toggle-btn:hover { color: var(--color-primary); }

.sidebar-nav {
  padding: 1rem 0;
  flex: 1;
  overflow-y: auto;
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}
.nav-item {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 0.75rem 1.5rem;
  color: var(--text-muted);
  font-weight: 500;
  transition: all 0.2s;
  white-space: nowrap;
}
.sidebar.collapsed .nav-item {
  justify-content: center;
  padding: 0.75rem 0;
}
.nav-item:hover, .nav-item.router-link-active {
  color: var(--color-primary);
  background-color: rgba(99, 102, 241, 0.1);
  border-right: 3px solid var(--color-primary);
}

.main-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}
.topbar {
  height: var(--header-h);
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 2rem;
  border-radius: 0;
  border-bottom: 1px solid var(--border-color);
  z-index: 5;
}
.page-title {
  margin: 0;
  font-size: 1.25rem;
  font-weight: 600;
}
.header-right {
  display: flex;
  align-items: center;
  gap: 1rem;
}
.icon-btn {
  background: var(--bg-element);
  border: 1px solid var(--border-color);
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  color: var(--text-main);
  transition: all 0.2s;
}
.icon-btn:hover {
  background: var(--color-primary);
  color: #fff;
}
.logout-btn:hover {
  background: var(--color-danger);
  color: #fff;
}

.page-container {
  flex: 1;
  overflow-y: auto;
  padding: 2rem;
}
</style>
