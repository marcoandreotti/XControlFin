<template>
  <div class="crud-page">
    <!-- Notice we don't have "Nova Instituição" here, this is Users -->
    <div class="actions-header mb-4">
      <BaseButton @click="openCreateModal">
        Novo Usuário
      </BaseButton>
    </div>

    <BaseCard>
      <div v-if="loading" class="loading-state">Carregando dados...</div>
      <table v-else class="data-table">
        <thead>
          <tr>
            <th>Nome</th>
            <th>Email</th>
            <th>Status</th>
            <th width="120">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ item.name }}</td>
            <td>{{ item.email }}</td>
            <td>
              <span :class="item.active ? 'badge bg-success' : 'badge bg-danger'">
                {{ item.active ? 'Ativo' : 'Inativo' }}
              </span>
            </td>
            <td>
              <div class="action-buttons">
                 <button class="text-btn text-primary" @click="editItem(item)">Editar</button>
                 <!-- <button class="text-btn text-danger" @click="deleteItem(item.id)">Excluir</button> -->
              </div>
            </td>
          </tr>
          <tr v-if="items.length === 0">
            <td colspan="4" class="text-center">Nenhum registro encontrado.</td>
          </tr>
        </tbody>
      </table>
    </BaseCard>

    <BaseModal v-model="isModalOpen" :title="isEditing ? 'Editar Usuário' : 'Novo Usuário'">
      <form @submit.prevent="saveItem" id="userForm">
        
        <BaseInput 
          v-model="form.name" 
          label="Nome Completo" 
          placeholder="Ex: João Silva" 
          required 
        />
        
        <BaseInput 
          v-model="form.email" 
          label="Endereço de Email"
          type="email" 
          placeholder="Ex: joao@email.com" 
          required 
        />

        <BaseInput 
          v-if="!isEditing"
          v-model="form.password" 
          label="Senha de Acesso" 
          type="password"
          placeholder="*******" 
          required 
        />

        <div class="form-group mb-3 checkbox-group">
          <label class="form-label">
            <input type="checkbox" v-model="form.active" /> 
            Usuário Ativo no Sistema
          </label>
        </div>

      </form>
      <template #footer>
        <BaseButton variant="ghost" @click="isModalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="userForm" :isLoading="saving">Salvar</BaseButton>
      </template>
    </BaseModal>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import BaseCard from '@/components/ui/BaseCard.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import api from '@/services/api'

const items = ref([])
const loading = ref(true)
const saving = ref(false)
const isModalOpen = ref(false)
const isEditing = ref(false)

const form = reactive({
  id: null,
  name: '',
  email: '',
  password: '',
  active: true
})

const loadData = async () => {
  try {
    loading.value = true
    const resp = await api.get('/User')
    items.value = resp.data || []
  } catch (error) {
    console.error("Erro", error)
  } finally {
    loading.value = false
  }
}

const openCreateModal = () => {
  isEditing.value = false
  form.id = null
  form.name = ''
  form.email = ''
  form.password = ''
  form.active = true
  isModalOpen.value = true
}

const editItem = (item) => {
  isEditing.value = true
  form.id = item.id
  form.name = item.name
  form.email = item.email
  form.password = '' // No password update in basic put
  form.active = item.active !== false
  isModalOpen.value = true
}

const saveItem = async () => {
  if (!form.name || !form.email) return
  if (!isEditing.value && !form.password) return

  try {
    saving.value = true
    if (isEditing.value) {
      await api.put(`/User/${form.id}`, {
        name: form.name,
        email: form.email,
        active: form.active
      })
    } else {
      await api.post('/User', form)
    }
    isModalOpen.value = false
    loadData()
  } catch (e) {
    alert(e.response?.data?.message || 'Erro ao salvar usuário')
  } finally {
    saving.value = false
  }
}

const deleteItem = async (id) => {
  if (confirm('Deseja realmente excluir este registro?')) {
    try {
      await api.delete(`/User/${id}`)
      loadData()
    } catch (e) {
      alert('Erro ao excluir')
    }
  }
}

onMounted(() => loadData())
</script>

<style scoped>
.actions-header { display: flex; justify-content: flex-end; }
.mb-4 { margin-bottom: 1rem; }
.mb-3 { margin-bottom: 1rem; }
.data-table { width: 100%; border-collapse: collapse; }
.data-table th, .data-table td { padding: 1rem; text-align: left; border-bottom: 1px solid var(--border-color); }
.data-table th { color: var(--text-muted); font-weight: 500; }
.loading-state { padding: 2rem; text-align: center; color: var(--text-muted); }
.text-center { text-align: center; }
.action-buttons { display: flex; gap: 0.75rem; }
.text-btn { background: none; border: none; cursor: pointer; font-weight: 500; font-size: 0.9rem; transition: opacity 0.2s; }
.text-primary { color: var(--color-primary); }
.text-danger { color: var(--color-danger); }
.text-btn:hover { opacity: 0.7; }

/* Custom Form Styles */
.checkbox-group label {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
  font-size: 0.95rem;
  color: var(--text-main);
  padding: 0.5rem 0;
}
.checkbox-group input {
  width: 1.25rem;
  height: 1.25rem;
  accent-color: var(--color-primary);
  cursor: pointer;
}
.badge {
  padding: 0.25rem 0.5rem;
  border-radius: 9999px;
  font-size: 0.75rem;
  font-weight: 600;
  color: white;
}
.bg-success { background-color: #10b981; }
.bg-danger { background-color: var(--color-danger); }
</style>
