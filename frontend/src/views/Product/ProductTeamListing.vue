<template>
  <div class="product-listing-container">
    <h2>Productos del Equipo</h2>
    <div class="products-container">
      <ul v-if="paginatedProducts && paginatedProducts.length > 0" class="products-list">
        <li v-for="product in paginatedProducts" :key="product.id" class="product-card">
          <router-link :to="`/productDetail/${product.id}`">
            <div class="card-container">
              <img :src="product.principalImage" alt="Imagen del producto" class="product-image" />
              <div class="product-info">
                <h3>{{ product.name }}</h3>
                <p>{{ product.price }}€</p>
              </div>
              <b-button href="#" variant="primary" class="buy-button">Comprar</b-button>
            </div>
          </router-link>
        </li>
      </ul>
      <p v-else>No hay productos disponibles para este equipo.</p>
    </div>

    <div class="custom-pagination">
      <b-pagination
        v-if="filteredProducts && filteredProducts.length > 8"
        v-model="currentPage"
        :total-rows="filteredProducts.length"
        :per-page="perPage"
        aria-controls="products-container"
        class="pagination"
      ></b-pagination>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      currentPage: 1,
      perPage: 8
    };
  },
  computed: {
    filteredProducts() {
      return this.$store.state.filteredProducts;
    },
    teamId() {
      return this.$store.state.verifiedTeam.id;
    },
    paginatedProducts() {
      const start = (this.currentPage - 1) * this.perPage;
      const end = start + this.perPage;
      return this.filteredProducts.slice(start, end);
    }
  },
  created() {
    this.fetchTeamsProducts();
  },
  methods: {
    fetchTeamsProducts() {
      this.$store.dispatch('fetchTeamsProducts', this.teamId);
    }
  },
  watch: {
    teamId() {
      this.fetchTeamsProducts();
      this.currentPage = 1; 
    },
    '$store.state.filters': {
      handler() {
        this.fetchTeamsProducts();
        this.currentPage = 1; 
      },
      deep: true
    }
  }
};
</script>

<style>
.product-listing-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  min-height: 71.5vh;
  padding: 20px;
}

.products-container {
  width: 100%;
  padding: 20px;
}

.products-container h2 {
  text-align: center;
  margin-bottom: 20px;
}

.products-list {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-wrap: wrap;
  justify-content: space-around; /* Esto ayudará a distribuir el espacio de manera uniforme */
}

.product-card {
  flex: 1 1 calc(25% - 20px);
  box-sizing: border-box;
  margin: 10px;
  text-align: center;
  max-width: calc(25% - 20px); /* Esto asegurará que cada tarjeta no exceda el 20% del contenedor */
}

.card-container {
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 10px;
  overflow: hidden;
  transition: box-shadow 0.3s ease;
  padding: 15px;
  display: flex;
  flex-direction: column;
  justify-content: space-between; /* Esto asegurará que el contenido esté distribuido verticalmente */
}

.card-container:hover {
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
}

.product-image {
  width: 100%;
  height: auto;
  border-bottom: 1px solid #ddd;
  margin-bottom: 15px;
}

.product-info {
  margin-bottom: 15px;
}

.product-info h3 {
  margin: 10px 0;
  font-size: 1.2rem;
  color: #333;
}

.product-info p {
  margin: 0;
  font-size: 1rem;
  color: #777;
}

.buy-button {
  width: 100%;
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px;
  cursor: pointer;
}

.buy-button:hover {
  background-color: #0056b3;
}

.custom-pagination {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}

.pagination {
  display: flex;
  justify-content: center;
}
</style>