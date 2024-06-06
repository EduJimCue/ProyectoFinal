<template>
  <div class="product-listing-container">
    <div class="products-container">
      <h2>Productos del Equipo</h2>
      <ul v-if="paginatedProducts && paginatedProducts.length > 0" class="products-list">
        <div v-for="product in paginatedProducts" :key="product.id" class="product-card">
          <router-link :key="product.id" :to="`/productDetail/${product.id}`">
            <div class="card-container">
              <b-card
                :title="product.name"
                :img-src="product.principalImage"
                img-top
                tag="article"
                style="max-width: 20rem;"
                class="mb-2"
              >
                <b-button href="#" variant="primary" style="width: 100px;">{{ product.price }}€</b-button>
              </b-card>
            </div>
          </router-link>
        </div>
      </ul>
      <p v-else>No hay productos disponibles para este equipo.</p>

      <div class="custom-pagination">
        <b-pagination
          v-if="filteredProducts && filteredProducts.length > 8"
          v-model="currentPage"
          :total-rows="filteredProducts.length"
          :per-page="perPage"
          aria-controls="products-container"
          style="display: flex;"
        ></b-pagination>
      </div>
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
      this.currentPage = 1; // Reset pagination when teamId changes
    },
    '$store.state.filters': {
      handler() {
        this.fetchTeamsProducts();
        this.currentPage = 1; // Reset pagination when filters change
      },
      deep: true
    }
  }
};
</script>

<style>
.product-listing-container {
  display: flex;
  min-height: 71.5vh;
}

.filters-container {
  flex: 0 0 20%;
  padding: 20px;
  border-right: 1px solid #ccc;
}

.products-container {
  flex: 1;
  padding: 20px;
}

.products-container h2 {
  margin-bottom: 20px;
}

.products-list {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-wrap: wrap;
}

.product-card {
  flex: 1 1 calc(25% - 20px);
  box-sizing: border-box;
  margin: 10px;
}

.card-container {
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
  overflow: hidden;
  transition: box-shadow 0.3s ease;
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  text-align: center;
}

.card-container:hover {
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

.product-image {
  width: 100%;
  height: 200px;
  object-fit: cover;
  border-bottom: 1px solid #ddd;
  margin-bottom: 10px;
}

.product-info h3 {
  margin: 10px 0 5px;
  font-size: 1.2rem;
}

.product-info p {
  margin: 0;
  font-size: 1rem;
  color: #555;
}
</style>