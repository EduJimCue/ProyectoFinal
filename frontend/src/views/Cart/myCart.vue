<template>
  <div>
    <h1>Cesta de la compra ({{ cartLength }})</h1>
    <div v-if="cart.length === 0">
      <p>El carrito está vacío</p>
    </div>
    <b-table v-else :items="cart" :fields="fields" style="margin: auto; border-collapse: collapse;">
      <template #cell(actions)="row">
        <button @click="removeFromCart(row.index)">Eliminar</button>
      </template>
      <template #row-details="row">
        <b-card>
          <ul>
            <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
          </ul>
        </b-card>
      </template>
    </b-table>
    <div v-if="this.$store.state.user && cart.length > 0">
      <button @click="lanzarPedido">Lanzar Pedido</button>
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