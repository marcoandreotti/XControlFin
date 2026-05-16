<template>
  <div class="crud-page">
    <div class="actions-header mb-4">
      <BaseButton @click="openCreateModal">
        Novo Planejamento
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
          <label class="form-label">Período de Início (Mês)</label>
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
            <th>Valor Estimado</th>
            <th>Centro de Custo</th>
            <th>Frequência</th>
            <th>Início</th>
            <th width="120">Ações</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="item in filteredItems" :key="item.id">
            <td>{{ item.historic }}</td>
            <td class="text-primary font-bold">R$ {{ Number(item.value).toFixed(2) }}</td>
            <td>{{ getCostCenterName(item.costCenterId) }}</td>
            <td>{{ getIntervalName(item.timeInterval) }}</td>
            <td>{{ new Date(item.startDate).toLocaleDateString() }}</td>
            <td>
              <div class="action-buttons">
                 <button class="text-btn text-primary" @click="editItem(item)">Editar</button>
                 <button class="text-btn text-danger" @click="deleteItem(item.id)">Excluir</button>
              </div>
            </td>
          </tr>
          <tr v-if="filteredItems.length === 0">
            <td colspan="6" class="text-center">Nenhum planejamento encontrado para estes filtros.</td>
          </tr>
        </tbody>
      </table>
    </BaseCard>

    <BaseModal v-model="isModalOpen" :title="isEditing ? 'Editar Planejamento' : 'Novo Planejamento'">
      <form @submit.prevent="saveItem" id="planForm">
        
        <BaseInput 
          v-model="form.historic" 
          label="Título (Histórico)" 
          placeholder="Ex: Pagamento de Aluguel" 
          required 
        />
        
        <BaseInput 
          v-model="form.value" 
          label="Valor Projetado (R$)" 
          type="number"
          step="0.01"
          placeholder="Ex: 1500.00" 
          required 
        />

        <div class="form-group mb-3">
          <label class="form-label">Instituição Financeira</label>
          <select v-model="form.financialInstitutionId" class="form-select" required>
            <option value="" disabled>Selecione uma instituição</option>
            <option v-for="inst in institutions" :key="inst.id" :value="inst.id">
              {{ inst.name }}
            </option>
          </select>
        </div>

        <div class="form-group mb-3">
          <label class="form-label">Centro de Custo</label>
          <select v-model="form.costCenterId" class="form-select" required>
            <option value="" disabled>Selecione um centro de custo</option>
            <option v-for="cc in costCenters" :key="cc.id" :value="cc.id">
              {{ cc.name }}
            </option>
          </select>
        </div>

        <div class="form-group mb-3">
          <label class="form-label">Frequência</label>
          <select v-model="form.timeInterval" class="form-select" required>
            <option :value="1">Diário</option>
            <option :value="2">Semanal</option>
            <option :value="3">Mensal</option>
            <option :value="4">Anual</option>
          </select>
        </div>

        <BaseInput 
          v-model="form.startDate" 
          label="Data de Início" 
          type="date"
          required 
        />

      </form>
      <template #footer>
        <BaseButton variant="ghost" @click="isModalOpen = false">Cancelar</BaseButton>
        <BaseButton type="submit" form="planForm" :isLoading="saving">Salvar</BaseButton>
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

const filters = reactive({
    institutionId: '',
    period: '' // Para planejamento deixaremos vazio o padrao
})

const form = reactive({
  id: null,
  historic: '',
  value: '',
  costCenterId: '',
  financialInstitutionId: '',
  timeInterval: 3,
  startDate: ''
})

const getIntervalName = (val) => {
  const map = { 1: 'Diário', 2: 'Semanal', 3: 'Mensal', 4: 'Anual' }
  return map[val] || 'Único'
}

const getCostCenterName = (id) => {
  const cc = costCenters.value.find(c => c.id === id)
  return cc ? cc.name : 'Desconhecido'
}

const loadData = async () => {
  try {
    loading.value = true
    const [planResp, ccResp, instResp] = await Promise.all([
      api.get('/Financial/planning'),
      api.get('/CostCenter'),
      api.get('/FinancialInstitution')
    ])
    items.value = planResp.data || []
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
        // Filtro por Período (Ano/Mês) no Início
        if (filters.period) {
            const itemDate = new Date(item.startDate);
            const itemMonth = `${itemDate.getFullYear()}-${String(itemDate.getMonth() + 1).padStart(2, '0')}`;
            if (itemMonth !== filters.period) return false;
        }
        return true;
    }).sort((a,b) => new Date(b.startDate) - new Date(a.startDate))
})

const openCreateModal = () => {
  isEditing.value = false
  form.id = null
  form.historic = ''
  form.value = ''
  form.costCenterId = ''
  form.financialInstitutionId = filters.institutionId || ''
  form.timeInterval = 3
  form.startDate = new Date().toISOString().split('T')[0]
  isModalOpen.value = true
}

const editItem = (item) => {
  isEditing.value = true
  form.id = item.id
  form.historic = item.historic
  form.value = item.value
  form.costCenterId = item.costCenterId
  form.financialInstitutionId = item.financialInstitutionId
  form.timeInterval = item.timeInterval
  form.startDate = item.startDate ? item.startDate.split('T')[0] : ''
  isModalOpen.value = true
}

const saveItem = async () => {
  if (!form.historic || !form.value) return
  try {
    saving.value = true
    if (isEditing.value) {
      await api.put(`/Financial/planning/${form.id}`, form)
    } else {
      await api.post('/Financial/planning', form)
    }
    isModalOpen.value = false
    loadData()
  } catch (e) {
    alert(e.response?.data?.message || 'Erro ao salvar planejamento')
  } finally {
    saving.value = false
  }
}

const deleteItem = async (id) => {
  if (confirm('Deseja realmente excluir este planejamento?')) {
    try {
      await api.delete(`/Financial/planning/${id}`)
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
