<template>
  <div class="my-orders">
    <h1>Mis Pedidos</h1>
    <div v-if="orders.length === 0">
      <p>No hay pedidos realizados</p>
    </div>
    <div v-else>
      <div v-for="order in orders" :key="order.orderId" class="order-container">
        <h2>Pedido ID: {{ order.orderId }} - Total: {{ calculateTotal(order.products) }}€</h2>
        <b-table striped hover :items="order.products" :fields="fields" small class="order-table">
          <template #cell(name)="data">
            <div class="product-name">{{ data.item.name }}</div>
          </template>
          <template #cell(quantity)="data">
            <div class="product-quantity">{{ data.item.quantity }}</div>
          </template>
          <template #cell(price)="data">
            <div class="product-price">{{ data.item.price }}€</div>
          </template>
          <template #cell(size)="data">
            <div class="product-size">{{ data.item.size }}</div>
          </template>
        </b-table>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    orders() {
      return this.$store.state.userOrders;
    },
    fields() {
      return [
        { key: 'name', label: 'Producto' },
        { key: 'quantity', label: 'Cantidad' },
        { key: 'price', label: 'Precio Unitario' },
        { key: 'size', label: 'Tamaño' }
      ];
    }
  },
  methods: {
    calculateTotal(products) {
      return products.reduce((total, product) => total + (product.price * product.quantity), 0).toFixed(2);
    },
    fetchUserOrders() {
      this.$store.dispatch('fetchUserOrders');
    }
  },
  created() {
    this.fetchUserOrders();
  }
};
</script>

<style scoped>
.my-orders {
  width: 90%;
  margin: 0 auto;
  padding: 20px;
}

.order-container {
  margin-bottom: 40px;
}

h1 {
  font-size: 2rem;
  margin-bottom: 20px;
  text-align: center;
}

h2 {
  font-size: 1.5rem;
  margin-bottom: 10px;
  background-color: #f7bc1a;
  padding: 10px;
  border-radius: 5px;
  text-align: center;
}

.order-table {
  margin-bottom: 20px;
  border: 1px solid #ddd;
  border-radius: 5px;
  overflow: hidden;
  width: 100%;
  table-layout: fixed; 
}

.order-table th, .order-table td {
  text-align: center;
  vertical-align: middle;
}

.order-table th {
  background-color: #f0f0f0;
  font-weight: bold;
}

.product-name, .product-quantity, .product-price, .product-size {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100%;
}

.product-name {
  text-align: left;
  padding-left: 10px;
  width: 100%;
}

.product-quantity, .product-price, .product-size {
  width: 100%;
  text-align: center;
}

.order-table th:nth-child(1), .order-table td:nth-child(1) {
  width: 40%;
}

.order-table th:nth-child(2), .order-table td:nth-child(2) {
  width: 20%;
}

.order-table th:nth-child(3), .order-table td:nth-child(3) {
  width: 20%;
}

.order-table th:nth-child(4), .order-table td:nth-child(4) {
  width: 20%; 
}
</style>