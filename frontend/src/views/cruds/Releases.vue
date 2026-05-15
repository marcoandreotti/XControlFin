<template>
  <div class="crud-page">
    <div class="actions-header mb-4">
      <BaseButton @click="openCreateModal">
        Novo Lançamento
      </BaseButton>
    </div>

    <!-- Filtros de Busca -->
    <BaseCard class="mb-4 filters-card">
      <h4 class="filters-title">Filtros de Busca</h4>
      <div class="filters-grid">
        <div class="form-group">
          <label class="form-label">Instituição Financeira</label>
          <select v-model="filters.institutionId" class="form-select">
            <option value="">Todas as Instituições</option>
            <option v-for="inst in institutions" :key="inst.id" :value="inst.id">
              {{ inst.name }}
            </option>
          </select>
        </div>
        <div class="form-group">
          <label class="form-label">Período Letivo (Mês)</label>
          <input type="month" v-model="filters.period" class="form-select" />
        </div>
      </div>
    </BaseCard>

    <BaseCard>
      <div v-if="loading" class="loading-state">Carregando dados...</div>
      <table v-else class="data-table">
        <thead>
          <tr>
            <th>Descrição (Histórico)</th>
            <th>Valor</th>
            <th>Centro de Custo</th>
            <th>Data de Pagamento</th>
            <th>Status</th>
            <th width="120">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in filteredItems" :key="item.id">
            <td>{{ item.historic }}</td>
            <td :class="item.value >= 0 ? 'text-success font-bold' : 'text-danger font-bold'">
              R$ {{ Number(item.value).toFixed(2) }}
            </td>
            <td>{{ getCostCenterName(item.costCenterId) }}</td>
            <td>{{ new Date(item.paymentDate).toLocaleDateString() }}</td>
            <td>
              <span :class="item.realized ? 'badge bg-success' : 'badge bg-warning'">
                {{ item.realized ? 'Realizado' : 'Pendente' }}
              </span>
            </td>
            <td>
              <div class="action-buttons">
                 <button class="text-btn text-primary" @click="editItem(item)">Editar</button>
                 <button class="text-btn text-danger" @click="deleteItem(item.id)">Excluir</button>
              </div>
            </td>
          </tr>
          <tr v-if="filteredItems.length === 0">
            <td colspan="6" class="text-center">Nenhum lançamento encontrado para estes filtros.</td>
          </tr>
        </tbody>
      </table>
    </BaseCard>

    <BaseModal v-model="isModalOpen" :title="isEditing ? 'Editar Lançamento' : 'Novo Lançamento'">
      <form @submit.prevent="saveItem" id="relForm">
        
        <BaseInput 
          v-model="form.historic" 
          label="Título (Histórico)" 
          placeholder="Ex: Supermercado" 
          required 
        />
        
        <BaseInput 
          v-model="form.value" 
          label="Valor (R$ - Use sinal negativo para despesas)" 
          type="number"
          step="0.01"
          placeholder="Ex: -150.00" 
          required 
        />

        <div class="form-group mb-3">
          <label class="form-label">Instituição Financeira</label>
          <select v-model="form.financialInstitutionId" class="form-select" required>
            <option value="" disabled>Selecione de onde saiu o valor</option>
            <option v-for="inst in institutions" :key="inst.id" :value="inst.id">
              {{ inst.name }}
            </option>
          </select>
        </div>

        <div class="form-group mb-3">
          <label class="form-label">Centro de Custo</label>
          <select v-model="form.costCenterId" class="form-select" required>
            <option value="" disabled>Selecione a categoria</option>
            <option v-for="cc in costCenters" :key="cc.id" :value="cc.id">
              {{ cc.name }}
            </option>
          </select>
        </div>

        <BaseInput 
          v-model="form.paymentDate" 
          label="Data de Pagamento" 
          type="date"
          required 
        />

        <div class="form-group mb-3 checkbox-group">
          <label class="form-label">
            <input type="checkbox" v-model="form.realized" /> 
            Marcar como Pago / Realizado
          </label>
        </div>

      </form>
      <template #footer>
        <BaseButton variant="ghost" @click="isModalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="relForm" :isLoading="saving">Salvar</BaseButton>
      </template>
    </BaseModal>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed } from 'vue'
import BaseCard from '@/components/ui/BaseCard.vue'
import BaseButton from '@/components/ui/BaseButton.vue'
import BaseModal from '@/components/ui/BaseModal.vue'
import BaseInput from '@/components/ui/BaseInput.vue'
import api from '@/services/api'

const items = ref([])
const costCenters = ref([])
const institutions = ref([])
const loading = ref(true)
const saving = ref(false)
const isModalOpen = ref(false)
const isEditing = ref(false)

// Data inicial padrão: Mês Atual
const getDefaultMonth = () => {
    const d = new Date();
    return `${d.getFullYear()}-${String(d.getMonth() + 1).padStart(2, '0')}`;
}

const filters = reactive({
    institutionId: '',
    period: getDefaultMonth()
})

const form = reactive({
  id: null,
  historic: '',
  value: '',
  costCenterId: '',
  financialInstitutionId: '',
  paymentDate: '',
  realized: false
})

const getCostCenterName = (id) => {
  const cc = costCenters.value.find(c => c.id === id)
  return cc ? cc.name : 'Desconhecido'
}

const loadData = async () => {
  try {
    loading.value = true
    const [relResp, ccResp, instResp] = await Promise.all([
      api.get('/Financial/crud-releases'),
      api.get('/CostCenter'),
      api.get('/FinancialInstitution')
    ])
    items.value = relResp.data || []
    costCenters.value = ccResp.data || []
    institutions.value = instResp.data || []
  } catch (error) {
    console.error("Erro", error)
  } finally {
    loading.value = false
  }
}

const filteredItems = computed(() => {
    return items.value.filter(item => {
        // Filtro por Instituição
        if (filters.institutionId && item.financialInstitutionId !== filters.institutionId) {
            return false;
        }
        // Filtro por Período (Ano/Mês)
        if (filters.period) {
            const itemDate = new Date(item.paymentDate);
            const itemMonth = `${itemDate.getFullYear()}-${String(itemDate.getMonth() + 1).padStart(2, '0')}`;
            if (itemMonth !== filters.period) return false;
        }
        return true;
    }).sort((a,b) => new Date(b.paymentDate) - new Date(a.paymentDate))
})

const openCreateModal = () => {
  isEditing.value = false
  form.id = null
  form.historic = ''
  form.value = ''
  form.costCenterId = ''
  form.financialInstitutionId = filters.institutionId || ''
  form.realized = true
  form.paymentDate = new Date().toISOString().split('T')[0]
  isModalOpen.value = true
}

const editItem = (item) => {
  isEditing.value = true
  form.id = item.id
  form.historic = item.historic
  form.value = item.value
  form.costCenterId = item.costCenterId
  form.financialInstitutionId = item.financialInstitutionId
  form.realized = item.realized
  form.paymentDate = item.paymentDate ? item.paymentDate.split('T')[0] : ''
  isModalOpen.value = true
}

const saveItem = async () => {
  if (!form.historic || !form.value) return
  try {
    saving.value = true
    if (isEditing.value) {
      await api.put(`/Financial/releases/${form.id}`, form)
    } else {
      await api.post('/Financial/releases', form)
    }
    isModalOpen.value = false
    loadData()
  } catch (e) {
    alert(e.response?.data?.message || 'Erro ao salvar lançamento')
  } finally {
    saving.value = false
  }
}

const deleteItem = async (id) => {
  if (confirm('Deseja realmente excluir este lançamento?')) {
    try {
      await api.delete(`/Financial/releases/${id}`)
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
.font-bold { font-weight: 600; }
.text-success { color: #10b981; }
.text-danger { color: var(--color-danger); }
.data-table { width: 100%; border-collapse: collapse; }
.data-table th, .data-table td { padding: 1rem; text-align: left; border-bottom: 1px solid var(--border-color); }
.data-table th { color: var(--text-muted); font-weight: 500; }
.loading-state { padding: 2rem; text-align: center; color: var(--text-muted); }
.text-center { text-align: center; }
.action-buttons { display: flex; gap: 0.75rem; }
.text-btn { background: none; border: none; cursor: pointer; font-weight: 500; font-size: 0.9rem; transition: opacity 0.2s; }
.text-primary { color: var(--color-primary); }
.text-btn:hover { opacity: 0.7; }

/* Custom Form Select Styling */
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
.checkbox-group label {
  display: flex;
  align-items: center;
  gap: 0.5rem;
  cursor: pointer;
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
.bg-warning { background-color: #f59e0b; }

/* Filters Styles */
.filters-card {
  padding: 1rem;
  background: rgba(255, 255, 255, 0.02);
}
.filters-title {
  margin-top: 0;
  margin-bottom: 1rem;
  font-size: 1rem;
  color: var(--text-main);
}
.filters-grid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}
</style>
