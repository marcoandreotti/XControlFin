<template>
  <div class="crud-page">
    <div class="actions-header mb-4">
      <BaseButton @click="openCreateModal">
        Vincular Instituição
      </BaseButton>
    </div>

    <BaseCard>
      <div v-if="loading" class="loading-state">Carregando dados...</div>
      <table v-else class="data-table">
        <thead>
          <tr>
            <th>Instituição Financeira Vinculada</th>
            <th width="120">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in linkedItems" :key="item.linkId || item.financialInstitutionId">
            <td>{{ getInstitutionName(item.financialInstitutionId) }}</td>
            <td>
              <div class="action-buttons">
                 <!-- The API only has Delete for the link -->
                 <button class="text-btn text-danger" @click="deleteItem(item.linkId)">Desvincular</button>
              </div>
            </td>
          </tr>
          <tr v-if="linkedItems.length === 0">
            <td colspan="2" class="text-center">Nenhuma instituição vinculada encontrada.</td>
          </tr>
        </tbody>
      </table>
    </BaseCard>

    <BaseModal v-model="isModalOpen" title="Vincular Instituição">
      <form @submit.prevent="saveItem" id="accountForm">
        
        <div class="form-group mb-3">
          <label class="form-label">Instituição Financeira</label>
          <select v-model="form.financialInstitutionId" class="form-select" required>
            <option value="" disabled>Selecione um banco</option>
            <option v-for="inst in availableInstitutions" :key="inst.id" :value="inst.id">
              {{ inst.name }}
            </option>
          </select>
        </div>
        
      </form>
      <template #footer>
        <BaseButton variant="ghost" @click="isModalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="accountForm" :isLoading="saving">Vincular</BaseButton>
      </template>
    </BaseModal>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed } from 'vue'
import BaseCard from '@/components/ui/BaseCard.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import api from '@/services/api'
import { useAuthStore } from '@/stores/auth'

const authStore = useAuthStore()

const institutions = ref([])
// We will store objects: { linkId (we might not know it easily unless API returns it), financialInstitutionId }
// Actually, wait! The API returns List<long> for UserFinancialInstitution/user/{userId}. 
// We don't have the link ID! If we don't have the link ID, we can't call DELETE /UserFinancialInstitution/{id}.
// BUT wait. If `GetFinancialInstitutionsByUserIdQuery` only returns `List<long>`, how do we delete it?
// Let's check if we can fetch all and filter by UserId to get the real Links.
const allLinks = ref([]) // To hold the actual link IDs

const loading = ref(true)
const saving = ref(false)
const isModalOpen = ref(false)

const form = reactive({
  financialInstitutionId: '',
  userId: null
})

const getInstitutionName = (id) => {
  const inst = institutions.value.find(i => i.id === id)
  return inst ? inst.name : 'Desconhecido'
}

const loadData = async () => {
  try {
    loading.value = true
    const userId = authStore.user?.id
    
    // We fetch all institutions
    const instResp = await api.get(`/FinancialInstitution`)
    institutions.value = instResp.data || []

    // Here we need to know the IDs to delete. Since endpoint /UserFinancialInstitution doesn't have getAll,
    // wait, we changed something? No, UserFinancialInstitution only has GetByUserId which returns List<long> (Institution IDs!).
    // This is a flaw in the backend (can't delete if we don't know the LinkId!).
    // For now, we will just display them. If they want to delete, it might fail if we don't have LinkId.
    // Fortunately, since we can't do GetAll, let's just make Delete call an endpoint that doesn't exist or just hide delete for now.
    // Wait, `DeleteUserFinancialInstitutionCommand` expects `Id`. So the user MUST know the link Id.
    
    // Let's assume we can fetch them via a different way or we'll mock the ID.
    // Actually, `GetFinancialInstitutionsByUserIdQuery` returns `List<long>`. We can't delete. 
    // We will just disable Delete for now until backend is fixed.
    const userInstResp = await api.get(`/UserFinancialInstitution/user/${userId}`)
    allLinks.value = userInstResp.data.map(instId => ({
        financialInstitutionId: instId,
        linkId: null // unknown
    }))
    
  } catch (error) {
    console.error("Erro", error)
  } finally {
    loading.value = false
  }
}

const availableInstitutions = computed(() => {
  const linkedIds = allLinks.value.map(l => l.financialInstitutionId)
  return institutions.value.filter(i => !linkedIds.includes(i.id))
})

const linkedItems = computed(() => {
  return allLinks.value
})

const openCreateModal = () => {
  form.financialInstitutionId = ''
  form.userId = authStore.user?.id
  isModalOpen.value = true
}

const saveItem = async () => {
  if (!form.financialInstitutionId) return
  try {
    saving.value = true
    await api.post('/UserFinancialInstitution', form)
    isModalOpen.value = false
    loadData()
  } catch (e) {
    alert(e.response?.data?.message || 'Erro ao vincular')
  } finally {
    saving.value = false
  }
}

const deleteItem = async (id) => {
    alert('A deleção exige o ID do vinculo, que a API não informa no momento. Atualização do Backend necessária.')
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

/* Custom Form Select Styling matching BaseInput */
.form-label {
  display: block;
  font-size: 0.875rem;
  font-weight: 500;
  color: var(--text-main);
  margin-bottom: 0.5rem;
}
.form-select {
  width: 100%;
  padding: 0.75rem 1rem;
  border: 1px solid var(--border-color);
  border-radius: 8px;
  background-color: var(--bg-surface);
  color: var(--text-main);
  font-family: inherit;
  font-size: 0.95rem;
  outline: none;
  transition: all 0.2s;
}
.form-select:focus {
  border-color: var(--color-primary);
  box-shadow: 0 0 0 3px rgba(var(--color-primary-rgb), 0.15);
}
</style>
