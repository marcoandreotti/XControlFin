<template>
  <div class="dashboard">
    <div class="summary-cards">
      <BaseCard title="Saldo Consolidado Estimado" class="stat-card">
        <div :class="totalBalance >= 0 ? 'stat-value text-success' : 'stat-value text-danger'">
          R$ {{ Number(totalBalance).toFixed(2) }}
        </div>
        <div class="stat-desc">Somatório de todos os lançamentos pagos</div>
      </BaseCard>
      
      <BaseCard title="Lançamentos dos Últimos 7 Dias" class="stat-card">
        <div :class="weekReleasesTotal >= 0 ? 'stat-value text-success' : 'stat-value text-danger'">
          R$ {{ Number(weekReleasesTotal).toFixed(2) }}
        </div>
        <div class="stat-desc">Acumulado líquido da última semana</div>
      </BaseCard>
    </div>

    <div class="dashboard-content mt-6">
      <BaseCard title="Últimos Lançamentos (Top 5)">
        <div v-if="loading" class="loading-state">Carregando dados...</div>
        <table v-else class="data-table">
          <thead>
            <tr>
              <th>Data</th>
              <th>Descrição</th>
              <th>Valor</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="release in recentReleases" :key="release.id">
              <td>{{ new Date(release.paymentDate).toLocaleDateString('pt-BR') }}</td>
              <td>{{ release.historic }}</td>
              <td :class="release.value >= 0 ? 'text-success font-bold' : 'text-danger font-bold'">
                 R$ {{ Number(release.value).toFixed(2) }}
              </td>
              <td>
                <span :class="release.realized ? 'badge bg-success' : 'badge bg-warning'">
                  {{ release.realized ? 'Pago' : 'Pendente' }}
                </span>
              </td>
            </tr>
            <tr v-if="recentReleases.length === 0">
              <td colspan="4" class="text-center">Nenhum lançamento no sistema ainda.</td>
            </tr>
          </tbody>
        </table>
      </BaseCard>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import BaseCard from '@/components/ui/BaseCard.vue'
import api from '@/services/api'

const totalBalance = ref(0)
const weekReleasesTotal = ref(0)
const recentReleases = ref([])
const loading = ref(true)

const loadDashboardData = async () => {
  try {
    loading.value = true
    
    // Buscar todos os lançamentos brutos
    const relResp = await api.get('/Financial/crud-releases')
    if (relResp.data && Array.isArray(relResp.data)) {
      const allReleases = relResp.data
      
      // Ordenar por data decrescente
      const sortedReleases = [...allReleases].sort((a, b) => new Date(b.paymentDate) - new Date(a.paymentDate))
      recentReleases.value = sortedReleases.slice(0, 5)
      
      // Calcular saldo de tudo que está Realizado (Pago)
      totalBalance.value = allReleases
        .filter(r => r.realized)
        .reduce((sum, item) => sum + item.value, 0)
        
      // Calcular movimentação dos últimos 7 dias (Realizado)
      const today = new Date()
      const sevenDaysAgo = new Date();
      sevenDaysAgo.setDate(today.getDate() - 7);
      
      weekReleasesTotal.value = allReleases
        .filter(r => r.realized && new Date(r.paymentDate) >= sevenDaysAgo && new Date(r.paymentDate) <= today)
        .reduce((sum, item) => sum + item.value, 0)
    }
  } catch (error) {
    console.error("Erro ao carregar dashboard", error)
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  loadDashboardData()
})
</script>

<style scoped>
.dashboard {
  display: flex;
  flex-direction: column;
}
.summary-cards {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1.5rem;
}
.stat-card .stat-value {
  font-size: 2.2rem;
  font-weight: 700;
  margin-top: 0.5rem;
}
.font-bold { font-weight: 600; }
.stat-desc {
  color: var(--text-muted);
  font-size: 0.9rem;
  margin-top: 0.25rem;
}
.text-success { color: var(--color-success); }
.text-danger { color: var(--color-danger); }
.mt-6 { margin-top: 1.5rem; }

.data-table {
  width: 100%;
  border-collapse: collapse;
}
.data-table th, .data-table td {
  padding: 1rem;
  text-align: left;
  border-bottom: 1px solid var(--border-color);
}
.data-table th {
  color: var(--text-muted);
  font-weight: 500;
}
.loading-state {
  padding: 2rem;
  text-align: center;
  color: var(--text-muted);
}
.text-center { text-align: center; }

.badge {
  padding: 0.25rem 0.5rem;
  border-radius: 9999px;
  font-size: 0.75rem;
  font-weight: 600;
  color: white;
}
.bg-success { background-color: #10b981; }
.bg-warning { background-color: #f59e0b; }
</style>
