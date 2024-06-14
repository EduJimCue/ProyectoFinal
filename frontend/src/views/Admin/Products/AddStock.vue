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
      <div class="add-stock">
        <h2>Añadir Stock</h2>
        <form @submit.prevent="addStock" class="add-stock-form">
          <div class="form-group">
            <label for="size">Talla:</label>
            <select v-model="selectedSize" id="size" class="form-control">
              <option v-for="size in sizes" :key="size.id" :value="size.id">{{ size.name }}</option>
            </select>
          </div>
          <div class="form-group">
            <label for="quantity">Cantidad:</label>
            <input type="number" id="quantity" v-model.number="quantity" min="0" class="form-control">
          </div>
          <button type="submit" class="btn btn-primary">Añadir Stock</button>
        </form>

        <h2 class="mt-4">Tallas y Stock</h2>
        <b-table striped hover :items="sizeStockData" :fields="fields" class="mt-3">
          <template #cell(size)="data">
            {{ data.item.size }}
          </template>
          <template #cell(stock)="data">
            {{ data.item.stock }}
          </template>
        </b-table>
        <div v-if="sizeStockData.length === 0" class="no-data">
          <p>No hay datos disponibles.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedSize: null,
      quantity: 1,
      sizeStockData: [],
      fields: ['size', 'stock']
    };
  },
  computed: {
    sizes() {
      return this.$store.state.sizes;
    },
    productId() {
      return this.$route.params.productId;
    }
  },
  created() {
    this.fetchSizeStockData();
  },
  methods: {
    async fetchSizeStockData() {
      try {
        const response = await fetch(`https://localhost:7098/SizeProduct/GetSizesByProduct?productId=${this.productId}`);
        const sizesData = await response.json();

        const promises = sizesData.map(async size => {
          const stockResponse = await fetch(`https://localhost:7098/SizeProduct/GetStock?productId=${this.productId}&sizeId=${size.id}`);
          const stockData = await stockResponse.json();
          return { size: size.name, stock: stockData[0] };
        });

        this.sizeStockData = await Promise.all(promises);
      } catch (error) {
        console.error("Error al obtener tallas y stock:", error);
      }
    },
    addStock() {
      const stockData = {
        productId: this.productId,
        sizeId: this.selectedSize,
        stock: this.quantity,
      };

      this.$store.dispatch("actualizeSizeProduct", stockData)
        .then(() => {
          this.selectedSize = null;
          this.quantity = 1;

          const sizeStock = this.sizeStockData.find(item => item.size === this.selectedSize);

          if (sizeStock) {
            sizeStock.stock.stock = this.quantity;
          }

          this.fetchSizeStockData();
        })
        .catch((error) => {
          console.error("Error al agregar stock desde el componente:", error);
        });
    }
  }
};
</script>

<style scoped>
.admin-page {
  display: flex;
  min-height: 71.5vh;
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

.add-stock {
  max-width: 600px;
  margin: 0 auto;
  background-color: #fff;
  border: 1px solid #ddd;
  border-radius: 5px;
  padding: 20px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.add-stock h2 {
  margin-bottom: 20px;
  font-size: 1.5em;
  color: #333;
}

.add-stock-form .form-group {
  margin-bottom: 15px;
}

.add-stock-form .form-control {
  padding: 10px;
  border-radius: 5px;
  border: 1px solid #ccc;
}

.add-stock-form .btn {
  padding: 10px 20px;
  font-size: 1em;
  border-radius: 5px;
}

.no-data {
  text-align: center;
  color: #888;
  margin-top: 20px;
}
</style>