<template>
  <div class="home">
    <div class="black">
      <p>Echa un vistazo a nuestras novedades</p>
    </div>
    <div class="carousel-container">
      <b-carousel
        id="carousel-fade"
        style="text-shadow: 0px 0px 2px #000"
        fade
        :interval="3000"
      >
        <b-carousel-slide
          img-src="https://tiendakarate.s3.amazonaws.com/imagenes/novedadesKarate1.webp"
        ></b-carousel-slide>
        <b-carousel-slide
          img-src="https://tiendakarate.s3.amazonaws.com/imagenes/novedadesKobudo .webp"
        ></b-carousel-slide>
        <b-carousel-slide
          img-src="https://tiendakarate.s3.amazonaws.com/imagenes/novedadesMMA.webp"
        ></b-carousel-slide>
      </b-carousel>
    </div>
    <div class="products-section">
      <h2>Nuestras novedades</h2>
      <div class="products-container">
        <div v-for="product in lastFour" :key="product.id" class="product-card">
          <router-link :to="`/productDetail/${product.id}`">
            <b-card
              :title="product.name"
              :img-src="product.principalImage"
              img-top
              tag="article"
              class="product-card-content"
            >
              <b-button href="#" variant="primary" class="product-price">{{ product.price }}€</b-button>
            </b-card>
          </router-link>
        </div>
      </div>
    </div>
    <div class="products-section">
      <h2>Productos Recomendados</h2>
      <div class="products-container">
        <div v-for="product in randomFour" :key="product.id" class="product-card">
          <router-link :to="`/productDetail/${product.id}`">
            <b-card
              :title="product.name"
              :img-src="product.principalImage"
              img-top
              tag="article"
              class="product-card-content"
            >
              <b-button href="#" variant="primary" class="product-price">{{ product.price }}€</b-button>
            </b-card>
          </router-link>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HomeView',
  computed: {
    lastFour() {
      return this.$store.state.lastFour;
    },
    randomFour() {
      return this.$store.state.randomFour;
    }
  },
  components: {}
}
</script>

<style>
.black {
  background-color: black;
  color: white;
  min-height: 20px;
  padding: 10px;
}

.carousel-container {
  display: inline-block;
  width: 75%;
  padding: 20px;
}

.products-section {
  margin: 20px 0;
}

.products-container {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}

.product-card {
  flex: 0 0 20%;
  max-width: 20%;
  padding: 10px;
  box-sizing: border-box;
}

.product-card-content {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  height: 100%; 
  min-height: 350px; 
}

.product-card-content img {
  object-fit: cover;
  height: 200px; 
}

.product-price {
  width: 100px;
  margin-top: auto; 
}
</style>