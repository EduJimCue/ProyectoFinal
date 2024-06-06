<template>
  <header class="header">
    <div class="search-container">
      <router-link to="/">
        <img alt="Mae logo" src="../assets/logo.png" class="logo">
      </router-link>
      <input type="text" placeholder="Buscar productos..." v-model="searchQuery" @input="searchProducts" class="search-input" />
      <div class="links">
        <span v-if="!user">
          <router-link to="/login">Iniciar sesión</router-link> |
          <router-link to="/signup">Crear cuenta</router-link> |
        </span>
        <span v-else>
          <router-link :to="{ path: '/', query: { logout: 'true' } }" @click.native="logout">Cerrar sesión</router-link> |
          <router-link to="/myAccount">Mi Cuenta</router-link> |
        </span>
        <router-link to="/myCart">Mi Cesta {{ $store.state.cart.length > 0 ? `(${$store.state.cart.length})` : '' }}</router-link>
      </div>
    </div>

    <ul v-if="filteredProducts.length > 0" class="search-results">
      <li v-for="product in filteredProducts" :key="product.id">
        <router-link :to="`/productDetail/${product.id}`" @click.native="clearSearchQuery">{{ product.name }}</router-link>
      </li>
    </ul>

    <div v-if="categories.length > 0" class="categories">
      <a
        v-for="category in categories"
        :key="category.id"
        @click.prevent="navigateToCategory(category.id)"
        class="category-link"
      >
        {{ category.name }}
      </a>
      <a href="/Teams">Equipos</a>
    </div>
  </header>
</template>

<script>
export default {
  data() {
    return {
      searchQuery: ''
    };
  },
  computed: {
    filteredProducts() {
      return this.$store.state.products.filter(product =>
        product.name.toLowerCase().includes(this.searchQuery.toLowerCase())
      );
    },
    categories() {
      return this.$store.state.categories;
    },
    user() {
      return this.$store.state.user;
    },
    cartCount() {
      return this.$store.state.cart.length;
    }
  },
  methods: {
    logout() {
      this.$store.dispatch('logout');
    },
    searchProducts() {
      this.$store.dispatch('searchProducts', this.searchQuery);
    },
    navigateToCategory(categoryId) {
      const currentRoute = this.$router.currentRoute;
      const targetRoute = { name: 'ProductListing', params: { category: categoryId } };
      if (currentRoute.name !== targetRoute.name || currentRoute.params.category !== categoryId) {
        this.updateFilter('categoryId', categoryId);
        this.$router.push(targetRoute);
      }
    },
    updateFilter(filterName, value) {
      this.$store.dispatch('updateFilter', { filterName, value });
    },
    clearSearchQuery() {
      this.$store.state.products=[];
      this.searchQuery = '';
    }
  }
};
</script>

<style scoped>
.header {
  background-color: white;
}

.search-container {
  display: flex;
  align-items: center;
}

.logo {
  width: 100px;
  height: auto;
  margin-right: 20px;
}

.search-input {
  flex: 1;
  padding: 10px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 20px;
  background-color: #ccc;
}

.search-results {
  list-style-type: none;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 0;
  margin-top: 10px;
}

.search-results li {
  padding: 5px;
  background-color: #fff;
  border: 1px solid #ccc;
  width: 300px;
  border-radius: 5px;
  margin-bottom: 5px;
}

.search-results li:hover {
  background-color: #f0f0f0;
}

.links {
  padding-right: 60px;
  padding-left: 30px;
}

.categories {
  margin-top: 10px;
  text-align: left;
  padding-left: 60px;
}

.categories span {
  margin-right: 5px;
}

.category-link {
  text-decoration: underline;
  cursor: pointer;
}
</style>