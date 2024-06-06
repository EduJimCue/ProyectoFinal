<template>
  <div style="min-height: 71.5vh;">
  <div v-if="isAdmin" class="admin-menu">
    <h2>Panel de Administración</h2>
    <ul>
      <li><router-link to="/adminProducts">Productos</router-link></li>
      <li><router-link to="/adminAttributes">Atributos</router-link></li>
      <li><router-link to="/adminImages">Imágenes</router-link></li>
    </ul>
  </div>
  <div v-else>
    <p>No tienes permiso para acceder a esta sección.</p>
    <router-link to="/">Ir al inicio</router-link>
  </div>
</div>
</template>

<script>
export default {
  computed: {
    isAdmin() {
      return this.$store.state.auth === 1;
    }
  },
  created() {
    if (!this.isAdmin) {
      this.$router.push('/');
    }
  }
};
</script>

<style>
.admin-menu {
  border: 1px solid #ccc;
  padding: 20px;
  border-radius: 5px;
  background-color: #f9f9f9;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
  max-width: 300px;
  margin: 20px auto;
}

.admin-menu h2 {
  margin-bottom: 20px;
  color: #333;
  text-align: center;
  font-size: 1.5em;
}

.admin-menu ul {
  list-style: none;
  padding: 0;
}

.admin-menu li {
  margin-bottom: 15px;
  transition: all 0.3s ease;
}

.admin-menu li:hover {
  transform: translateX(10px);
}

.admin-menu a {
  text-decoration: none;
  color: #333;
  padding: 10px;
  display: block;
  border-radius: 3px;
  transition: all 0.3s ease;
}

.admin-menu a:hover {
  color: #007bff;
  background-color: #f0f8ff;
}

.admin-menu p {
  text-align: center;
  font-size: 1.2em;
}

.admin-menu .router-link-active {
  font-weight: bold;
  color: #007bff;
}
</style>