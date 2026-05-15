<template>
  <div class="auth-layout">
    <div class="auth-box glass fade-in">
      <slot></slot>
    </div>
  </div>
</template>

<style scoped>
.auth-layout {
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, var(--bg-body) 0%, rgba(99, 102, 241, 0.1) 100%);
  padding: 1rem;
}
.auth-box {
  width: 100%;
  max-width: 420px;
  padding: 2.5rem 2rem;
  box-shadow: 0 10px 40px rgba(0,0,0,0.1);
}
</style>
