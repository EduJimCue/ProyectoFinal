<template>
  <div id="app">
    <div class="black">
      <div class="text-slider">
        <span class="slider-text">
          <svg v-html="currentSVG" style="height: 17; width: 17;"></svg>
          {{ currentText }}
        </span>
      </div>
    </div>
    <nav class="header">
      <AppHeader />
    </nav>
    <router-view/>
    <AppFooter />
  </div>
</template>

<script>
import AppHeader from './components/AppHeader.vue';
import AppFooter from './components/AppFooter.vue'; 

export default {
  data(){
    return{
      sliderTexts: [
        "Envios gratuitos en compras superiores a 50€",
        "Devoluciones gratuitas",
        "Pedido listo en 1-2 dias laborales"
      ],
      sliderSVGs: [
        '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-truck" viewBox="0 0 16 16"><path d="M0 3.5A1.5 1.5 0 0 1 1.5 2h9A1.5 1.5 0 0 1 12 3.5V5h1.02a1.5 1.5 0 0 1 1.17.563l1.481 1.85a1.5 1.5 0 0 1 .329.938V10.5a1.5 1.5 0 0 1-1.5 1.5H14a2 2 0 1 1-4 0H5a2 2 0 1 1-3.998-.085A1.5 1.5 0 0 1 0 10.5zm1.294 7.456A2 2 0 0 1 4.732 11h5.536a2 2 0 0 1 .732-.732V3.5a.5.5 0 0 0-.5-.5h-9a.5.5 0 0 0-.5.5v7a.5.5 0 0 0 .294.456M12 10a2 2 0 0 1 1.732 1h.768a.5.5 0 0 0 .5-.5V8.35a.5.5 0 0 0-.11-.312l-1.48-1.85A.5.5 0 0 0 13.02 6H12zm-9 1a1 1 0 1 0 0 2 1 1 0 0 0 0-2m9 0a1 1 0 1 0 0 2 1 1 0 0 0 0-2"/></svg>',
        '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-arrow-clockwise" viewBox="0 0 16 16"><path fill-rule="evenodd" d="M8 3a5 5 0 1 0 4.546 2.914.5.5 0 0 1 .908-.417A6 6 0 1 1 8 2z"/><path d="M8 4.466V.534a.25.25 0 0 1 .41-.192l2.36 1.966c.12.1.12.284 0 .384L8.41 4.658A.25.25 0 0 1 8 4.466"/></svg>',
        '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-stopwatch-fill" viewBox="0 0 16 16"><path d="M6.5 0a.5.5 0 0 0 0 1H7v1.07A7.001 7.001 0 0 0 8 16a7 7 0 0 0 5.29-11.584l.013-.012.354-.354.353.354a.5.5 0 1 0 .707-.707l-1.414-1.415a.5.5 0 1 0-.707.707l.354.354-.354.354-.012.012A6.97 6.97 0 0 0 9 2.071V1h.5a.5.5 0 0 0 0-1zm2 5.6V9a.5.5 0 0 1-.5.5H4.5a.5.5 0 0 1 0-1h3V5.6a.5.5 0 1 1 1 0"/></svg>'
      ],
      currentSlide: 0,
      showLogin: false,
    }
  },
  components: {
    AppHeader,
    AppFooter 
  },
  created() {
    setInterval(() => {
      this.currentSlide = (this.currentSlide + 1) % this.sliderTexts.length;
    }, 10000)
    this.$store.dispatch('fetchLastProducts');
    this.$store.dispatch('fetchRecommendedProducts');
    this.$store.dispatch('fetchCategorys');
    this.$store.dispatch('fetchImages');
    this.$store.dispatch('fetchAges');
    this.$store.dispatch('fetchColours');
    this.$store.dispatch('fetchSports');
    this.$store.dispatch('fetchGenders');
    this.$store.dispatch('fetchSizes');
    this.$store.dispatch('fetchBrands');
    this.$store.dispatch('fetchTeams');
  },
  computed: {
    currentText() {
      return this.sliderTexts[this.currentSlide];
    },
    currentSVG() {
      return this.sliderSVGs[this.currentSlide];
    }
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
.black {
  background-color: black;
  color: white;
  overflow: hidden;
  min-height: 15px;
  padding: 5px;
}

.text-slider {
  display: inline-block;
  overflow: hidden;
  width: 100%; 
  animation: slide-text 10s linear infinite;
}

.slider-text {
  flex: 0 0 auto;
  margin-right: 20px;
  white-space: nowrap; 
}

@keyframes slide-text {
  0% {
    transform: translateX(100%);
  }
  30% {
    transform: translateX(0%);
  }
  70% {
    transform: translateX(0%);
  }
  100% {
    transform: translateX(-100%);
  }
}
.header {
  position: sticky;
  top: 0;
  z-index: 1000;
}
nav a {
  font-weight: bold;
  color: #2c3e50;
  padding-left: 10px;
}

nav a.router-link-exact-active {
  color: #42b983;
}

body {
  margin: 0;
}
</style>
