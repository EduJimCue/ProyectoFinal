<template>
  <div class="cart-container" style="min-height: 41vh;">
    <h1>Cesta de la compra ({{ cartLength }})</h1>
    <div v-if="cart.length === 0">
      <p>El carrito está vacío</p>
    </div>
    <b-table v-else :items="cart" :fields="fields" class="cart-table">
      <template #cell(actions)="row">
        <button @click="removeFromCart(row.index)" class="remove-button">Eliminar</button>
      </template>
      <template #row-details="row">
        <b-card>
          <ul>
            <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
          </ul>
        </b-card>
      </template>
    </b-table>
    <div v-if="this.$store.state.user && cart.length > 0" class="order-button-container">
      <button @click="lanzarPedido" class="order-button">Lanzar Pedido</button>
    </div>
    <div v-if="!this.$store.state.user">
      <p>Necesitas estar logueado para lanzar el pedido</p>
    </div>
  </div>
</template>

<script>
export default {
  computed: {
    cart() {
      return this.$store.state.cart;
    },
    cartLength() {
      return this.$store.state.cart.length;
    },
    fields() {
      return [
        { key: 'productName', label: 'Producto' },
        { key: 'sizeName', label: 'Talla' },
        { key: 'quantity', label: 'Cantidad' },
        { key: 'price', label: 'Precio' },
        { key: 'actions', label: 'Acciones' }
      ];
    }
  },
  methods: {
    removeFromCart(index) {
      this.$store.dispatch('removeFromCart', index);
    },
    async lanzarPedido() {
      await this.$store.dispatch('createOrder');
    }
  }
};
</script>

<style scoped>
.cart-container {
  width: 90%;
  margin: 0 auto;
  padding: 20px;
}

h1 {
  font-size: 2rem;
  margin-bottom: 20px;
  text-align: center;
}

.cart-table {
  margin: 20px auto;
  border: 1px solid #ddd;
  border-radius: 5px;
  overflow: hidden;
  width: 100%;
  table-layout: fixed;
}

.cart-table th, .cart-table td {
  text-align: center;
  vertical-align: middle;
  padding: 10px;
}

.cart-table th {
  background-color: #f0f0f0;
  font-weight: bold;
}

.remove-button {
  background-color: #dc3545;
  color: white;
  border: none;
  padding: 5px 10px;
  cursor: pointer;
  border-radius: 5px;
}

.remove-button:hover {
  background-color: #c82333;
}

.order-button-container {
  text-align: center;
  margin-top: 20px;
}

.order-button {
  background-color: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  cursor: pointer;
  border-radius: 5px;
  font-size: 1rem;
}

.order-button:hover {
  background-color: #0056b3;
}
</style>