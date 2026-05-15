<template>
  <div class="crud-page">
    <div class="actions-header mb-4">
      <BaseButton @click="openCreateModal">Nova Instituição</BaseButton>
    </div>

    <BaseCard>
      <div v-if="loading" class="loading-state">Carregando dados...</div>
      <table v-else class="data-table">
        <thead>
          <tr>
            <th>Nome da Instituição</th>
            <th width="120">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in items" :key="item.id">
            <td>{{ item.name }}</td>
            <td>
              <div class="action-buttons">
                 <button class="text-btn text-primary" @click="editItem(item)">Editar</button>
                 <button class="text-btn text-danger" @click="deleteItem(item.id)">Excluir</button>
              </div>
            </td>
          </tr>
          <tr v-if="items.length === 0">
            <td colspan="2" class="text-center">Nenhum registro encontrado.</td>
          </tr>
        </tbody>
      </table>
    </BaseCard>

    <BaseModal v-model="isModalOpen" :title="isEditing ? 'Editar Instituição' : 'Nova Instituição'">
      <form @submit.prevent="saveItem" id="instForm">
        <BaseInput 
          v-model="form.name" 
          label="Nome da Instituição" 
          placeholder="Ex: Nubank, Itaú..." 
          required 
        />
      </form>
      <template #footer>
        <BaseButton variant="ghost" @click="isModalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="instForm" :isLoading="saving">Salvar</BaseButton>
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
  name: ''
})

const loadData = async () => {
  try {
    loading.value = true
    const resp = await api.get('/FinancialInstitution')
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
  isModalOpen.value = true
}

const editItem = (item) => {
  isEditing.value = true
  form.id = item.id
  form.name = item.name
  isModalOpen.value = true
}

const saveItem = async () => {
  if (!form.name) return
  try {
    saving.value = true
    if (isEditing.value) {
      await api.put(`/FinancialInstitution/${form.id}`, form)
    } else {
      await api.post('/FinancialInstitution', form)
    }
    isModalOpen.value = false
    loadData()
  } catch (e) {
    alert(e.response?.data?.message || 'Erro ao salvar instituição')
  } finally {
    saving.value = false
  }
}

const deleteItem = async (id) => {
  if (confirm('Deseja realmente excluir este registro?')) {
    try {
      await api.delete(`/FinancialInstitution/${id}`)
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
</style>
