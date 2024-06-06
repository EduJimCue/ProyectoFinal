<template>
  <div class="admin-page">
    <div class="admin-menu">
      <h2>Panel de Administración</h2>
      <ul>
        <li><router-link to="/adminProducts">Productos</router-link></li>
        <li><router-link to="/adminAttributes">Atributos</router-link></li>
        <li><router-link to="/adminImages">Imágenes</router-link></li>
      </ul>
    </div>
    <div class="admin-content">
      <h1>Administrar Productos</h1>
      <router-link to="/addProducts">
        <b-button variant="success" class="mb-3">Añadir</b-button>
      </router-link>
      <b-table striped hover :items="adminProducts" :fields="fields">
        <template #cell(name)="data">
          <router-link :to="`/editProducts/${data.item.id}`">{{ data.item.name }}</router-link>
        </template>
        <template #cell(stock)="data">
          <b-button variant="warning" @click="redirectToAddStock(data.item.id)">Añadir Stock</b-button>
        </template>
        <template #cell(Accion)="data">
          <b-button variant="danger" @click="deleteProduct(data.item.id)">Eliminar</b-button>
        </template>
      </b-table>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      fields: ['name', 'stock', 'Accion']
    };
  },
  computed: {
    adminProducts() {
      return this.$store.state.adminProducts;
    }
  },
  created() {
    this.fetchProducts();
  },
  methods: {
    fetchProducts() {
      this.$store.dispatch('fetchProducts');
    },
    deleteProduct(productId) {
      this.$store.dispatch('deleteProduct', productId);
    },
    redirectToAddStock(productId) {
      this.$router.push(`/addStock/${productId}`);
    }
  }
};
</script>

<style scoped>
.admin-page {
  display: flex;
}

.admin-menu {
  width: 20%;
  border-right: 1px solid #ccc;
  padding: 20px;
  background-color: #f9f9f9;
  overflow-y: auto;
}

.admin-menu h2 {
  margin-bottom: 10px;
}

.admin-menu ul {
  list-style: none;
  padding: 0;
}

.admin-menu li {
  margin-bottom: 10px;
}

.admin-menu a {
  text-decoration: none;
  color: #333;
}

.admin-menu a:hover {
  color: #007bff;
}

.admin-content {
  padding: 20px;
  width: 80%;
}
</style>