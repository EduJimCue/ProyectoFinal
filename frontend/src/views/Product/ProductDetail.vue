<template>
  <div class="product-page">
    <h1>Detalles del Producto</h1>
    <div v-if="product" class="product-container">
      <div class="product-thumbnails">
        <img
          :src="product.principalImage"
          alt="Otras imágenes"
          @click="mainImage = product.principalImage"
          class="product-thumbnail"
        >
        <img
          :src="product.secondImage"
          alt="Otras imágenes"
          @click="mainImage = product.secondImage"
          class="product-thumbnail"
        >
        <img
          :src="product.thirdImage"
          alt="Otras imágenes"
          @click="mainImage = product.thirdImage"
          class="product-thumbnail"
        >
        <img
          :src="product.fourthImage"
          alt="Otras imágenes"
          @click="mainImage = product.fourthImage"
          class="product-thumbnail"
        >
      </div>
      <img :src="mainImage" alt="Imagen principal" class="product-main-image">
      <div class="product-info">
        <h2>{{ product.name }}</h2>
        <p class="product-price">Precio: {{ product.price }}€</p>
        <p class="product-description">Descripción: {{ product.description }}</p>
        <form @submit.prevent="submitForm" class="product-form">
          <div class="form-group">
            <label for="size">Talla:</label>
            <select id="size" v-model="selectedSize" @change="updateAvailableStock(selectedSize.id)">
              <option v-for="size in availableSizes" :key="size.id" :value="{ id: size.id, name: size.name }">{{ size.name }}</option>
            </select>
          </div>
          <div class="form-group">
            <label for="quantity">Cantidad:</label>
            <select id="quantity" v-model="selectedQuantity" :disabled="!selectedSize">
              <option v-for="n in Math.min(maxQuantity, 10)" :key="n" :value="n">{{ n }}</option>
            </select>
          </div>
          <button type="submit" class="add-to-cart-button">Agregar a la cesta</button>
        </form>
        <b-alert v-if="errorMessage" variant="danger" show>{{ errorMessage }}</b-alert>
        <b-alert v-if="successMessage" variant="success" show>{{ successMessage }}</b-alert>
        <div v-if="otherColours && otherColours.length" class="other-colours">
          <h3>Otros colores</h3>
          <div v-for="(color, index) in otherColours" :key="index" class="other-colour">
            <router-link :key="color.id" :to="`/productDetail/${color.id}`">
              <img :src="color.principalImage" alt="Imagen de otro color" @click="reloadPage(color.id)" class="other-colour-image">
            </router-link>
          </div>
        </div>
        <h5>Envío gratis a España Peninsular a partir de 20 €</h5>
        <h5>Recoge tu pedido gratis en tienda</h5>
        <h5>La 1ª devolución o cambio de talla es gratis</h5>
      </div>
    </div>
    <div v-if="similarProducts && similarProducts.length" class="similar-products">
      <h2>Productos similares</h2>
      <div class="similar-products-container">
        <div v-for="(similar, index) in similarProducts" :key="index" class="similar-product">
          <router-link :key="similar.id" :to="`/productDetail/${similar.id}`">
            <img :src="similar.principalImage" alt="Producto similar" @click="reloadPage(similar.id)" class="similar-product-image">
            <p @click="reloadPage(similar.id)">{{ similar.name }}</p>
          </router-link>
        </div>
      </div>
    </div>
    <p v-else>No se encontraron detalles para este producto.</p>
  </div>
</template>

<script>
export default {
  props: ['id'],
  computed: {
    product() {
      return this.$store.state.productDetail;
    },
    availableSizes() {
      return this.$store.state.availableSizes;
    },
    maxQuantity() {
      return this.selectedSize ? this.getAvailableStock() : 0; 
    },
    otherColours() {
      return this.$store.state.otherColours;
    },
    similarProducts() {
      return this.$store.state.similarProducts;
    },
  },
  data() {
    return {
      selectedSize: null,
      selectedQuantity: 0,
      mainImage: null,
      errorMessage: null,
      successMessage: null,
    };
  },
  created() {
    const productId = this.$route.params.product;
    this.$store.dispatch('fetchProductDetail', productId)
      .then(() => {
        this.mainImage = this.$store.state.productDetail.principalImage;
      })
      .then(() => {
        const productDetail = this.$store.state.productDetail;
        if (productDetail && productDetail.name && productDetail.colourId) {
          this.$store.dispatch('fetchOtherColours', {
            productName: productDetail.name,
            colourId: productDetail.colourId
          });
        } else {
          console.error('Los detalles del producto son nulos o incompletos.');
        }
      })
      .catch(error => {
        console.error('Error al obtener los detalles del producto:', error);
      });
    this.$store.dispatch('fetchAvailableSizes', productId)
      .catch(error => {
        console.error('Error al obtener las tallas:', error);
      });
    this.$store.dispatch('fetchSimilarProducts', productId)
      .catch(error => {
        console.error('Error al obtener otros productos:', error);
      });
  },
  methods: {
    reloadPage(productId) {
      window.location.href = `/productDetail/${productId}`;
    },
    updateAvailableStock(sizeId) {
      this.$store.dispatch('fetchAvailableStock', {productId: this.$route.params.product, sizeId});
    },
    submitForm() {
      this.errorMessage = null;
      this.successMessage = null;
      if (!this.selectedSize || this.selectedQuantity === 0) {
        this.errorMessage = "Debe seleccionar una talla y una cantidad mayor que 0.";
        return;
      }
      const productData = {
        productId: this.$route.params.product,
        productName: this.$store.state.productDetail.name,
        sizeId: this.selectedSize.id,
        sizeName: this.selectedSize.name,
        price: this.$store.state.productDetail.price,
        quantity: this.selectedQuantity
      };
      
      this.$store.dispatch('addToCart', productData)
        .then(() => {
          this.successMessage = "Producto añadido correctamente a la cesta.";
        })
        .catch(error => {
          this.errorMessage = "Error al añadir el producto a la cesta.";
          console.log(error)
        });
    },
    getAvailableStock() {
      var stock = this.$store.state.availableStock;
      if (stock > 10) {
        return 10;
      } else {
        return stock;
      }
    }
  }
};
</script>
<style>
.product-page {
  font-family: 'Arial', sans-serif;
  padding: 20px;
  min-height: 71.5vh;
  max-width: 1500px;
  margin: 0 auto;
}

.product-container {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  gap: 40px; 
}

.product-thumbnails {
  display: flex;
  flex-direction: column;
  width: 120px; 
  gap: 20px; 
}

.product-thumbnail {
  width: 100%;
  height: auto;
  cursor: pointer;
  border: 1px solid #ccc;
  border-radius: 5px;
  transition: border-color 0.3s;
}

.product-thumbnail:hover {
  border-color: #000;
}

.product-main-image {
  width: 600px;
  height: auto;
  border: 1px solid #ccc;
  border-radius: 5px;
  padding: 10px;
  margin-right: 80px; 
}

.product-info {
  flex: 1;
  margin-left: 80px; 
}

.product-info h2 {
  font-size: 2em;
  margin-bottom: 10px;
}

.product-price {
  font-size: 1.5em;
  color: #d9534f;
  margin-bottom: 20px;
}

.product-description {
  font-size: 1.2em;
  margin-bottom: 20px;
}

.product-form {
  display: flex;
  flex-direction: column;
  gap: 15px;
}

.product-form .form-group {
  display: flex;
  flex-direction: column;
}

.product-form label {
  font-weight: bold;
}

.product-form select {
  padding: 10px;
  font-size: 1em;
  border: 1px solid #ccc;
  border-radius: 5px;
}

.add-to-cart-button {
  padding: 15px;
  font-size: 1.2em;
  color: #fff;
  background-color: #f7bc1a;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.add-to-cart-button:hover {
  background-color: #cf9f1a;
}

.other-colours {
  margin-top: 20px;
}

.other-colour {
  display: inline-block;
  margin-right: 10px;
}

.other-colour-image {
  width: 60px;
  height: 60px;
  cursor: pointer;
  border: 1px solid #ccc;
  border-radius: 5px;
  transition: border-color 0.3s;
}

.other-colour-image:hover {
  border-color: #000;
}

.similar-products {
  margin-top: 40px;
}

.similar-products-container {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.similar-product {
  width: 200px;
  text-align: center;
}

.similar-product img {
  width: 100%;
  height: auto;
  cursor: pointer;
  border: 1px solid #ccc;
  border-radius: 5px;
  margin-bottom: 10px;
  transition: border-color 0.3s;
}

.similar-product img:hover {
  border-color: #000;
}
</style>