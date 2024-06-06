<template>
  <div>
    <div class="product-listing-container">
      <div class="filters-container">
        <h2>Filtros activos:</h2>
        <p v-for="(value, filterKey) in filters" :key="filterKey" @click="isCategory(filterKey) ? null : removeFilter(filterKey, value)" class="filter-item">
          <span class="filter-value">{{ value }}</span>
          <span class="close-button">X</span>
        </p>
        <h2>Filtros disponibles:</h2>
        <template v-for="(filterValue, filterKey) in filteredFiltersWithValues">
          <div :key="filterKey" class="filter-group">
            <h3>{{ getFilterName(filterKey) }}</h3>
            <ul>
              <p v-for="(value, index) in filterValue" :key="index" @click="applyFilter(filterKey, value)" class="filter-option">
                <span class="circle"></span>{{ value }}
              </p>
            </ul>
          </div>
        </template>
      </div>

      <div class="products-container">
        <ul v-if="paginatedProducts && paginatedProducts.length > 0" style="display: flex; flex-wrap: wrap;">
          <div v-for="product in paginatedProducts" :key="product.id">
            <router-link :key="product.id" :to="`/productDetail/${product.id}`">
              <div class="card-container">
                <b-card
                  :title="product.name"
                  :img-src="product.principalImage"
                  img-top
                  tag="article"
                  style="max-width: 20rem; min-height: 450px;"
                  class="mb-2"
                >
                  <b-button href="#" variant="primary" style="width: 100px;">{{ product.price }}€</b-button>
                </b-card>
              </div>
            </router-link>
          </div>
        </ul>
        <p v-else>No hay productos con esos filtros.</p>
      </div>
    </div>
    <div class="custom-pagination">
      <b-pagination
        v-if="filteredProducts && filteredProducts.length > 8"
        v-model="currentPage"
        :total-rows="filteredProducts.length"
        :per-page="perPage"
        aria-controls="products-container"
        style="display: flex; justify-content: end; margin-right: 30px;"
      ></b-pagination>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      categoryId: null,
      currentPage: 1,
      perPage: 8
    };
  },
  computed: {
    filters() {
      const filters = this.$store.state.filters;
      const categories = this.$store.state.categories;
      const colours = this.$store.state.colours;
      const genders = this.$store.state.genders;
      const ages = this.$store.state.ages;
      const brands = this.$store.state.brands;
      const sizes = this.$store.state.sizes;
      const sports = this.$store.state.sports;
      const teams = this.$store.state.teams;

      const filtered = {};
      for (const [key, value] of Object.entries(filters)) {
        if (value !== null && value !== undefined) {
          let filterName = '';
          switch (key) {
            case 'categoryId':
              filterName = categories.find(category => category.id === value)?.name;
              break;
            case 'colourId':
              filterName = colours.find(colour => colour.id === value)?.name;
              break;
            case 'genderId':
              filterName = genders.find(gender => gender.id === value)?.name;
              break;
            case 'ageId':
              filterName = ages.find(age => age.id === value)?.name;
              break;
            case 'brandId':
              filterName = brands.find(brand => brand.id === value)?.name;
              break;
            case 'sizeId':
              filterName = sizes.find(size => size.id === value)?.name;
              break;
            case 'sportId':
              filterName = sports.find(sport => sport.id === value)?.name;
              break;
            case 'teamId':
              filterName = teams.find(team => team.id === value)?.name;
              break;
            default:
              filterName = value;
          }
          filtered[key] = filterName;
        }
      }
      return filtered;
    },
    filteredFiltersWithValues() {
      const filters = this.$store.state.filteredFilters;

      if (!filters) {
        return {};
      }

      const categories = this.$store.state.categories;
      const colours = this.$store.state.colours;
      const genders = this.$store.state.genders;
      const ages = this.$store.state.ages;
      const brands = this.$store.state.brands;
      const sizes = this.$store.state.sizes;
      const sports = this.$store.state.sports;
      const teams = this.$store.state.teams;

      const filtered = {};
      for (const [key, value] of Object.entries(filters)) {
        if (Array.isArray(value) && value.length > 0) {
          const filterNames = value.map(val => {
            switch (key) {
              case 'categoriesIds':
                return categories.find(category => category.id === val)?.name || '';
              case 'colourIds':
                return colours.find(colour => colour.id === val)?.name || '';
              case 'gendersIds':
                return genders.find(gender => gender.id === val)?.name || '';
              case 'agesIds':
                return ages.find(age => age.id === val)?.name || '';
              case 'brandsIds':
                return brands.find(brand => brand.id === val)?.name || '';
              case 'sizesIds':
                return sizes.find(size => size.id === val)?.name || '';
              case 'sportsIds':
                return sports.find(sport => sport.id === val)?.name || '';
              case 'teamsIds':
                return teams.find(team => team.id === val)?.name || '';
              default:
                return val;
            }
          });
          filtered[key.substring(0, key.length - 3)] = filterNames;
        }
      }
      return filtered;
    },
    filteredProducts() {
      return this.$store.state.filteredProducts;
    },
    paginatedProducts() {
      const start = (this.currentPage - 1) * this.perPage;
      const end = start + this.perPage;
      return this.filteredProducts.slice(start, end);
    }
  },
  watch: {
    '$route.params.category'(newCategory) {
      this.categoryId = newCategory;
      this.fetchFilteredProducts();
      this.resetPagination(); // Reset pagination when category changes
    },
    '$store.state.filters': {
      handler() {
        this.fetchFilteredProducts();
        this.resetPagination(); // Reset pagination when filters change
      },
      deep: true
    }
  },
  created() {
    this.categoryId = this.$route.params.category;
    this.fetchFilteredProducts();
  },
  methods: {
    getFilterName(filterKey) {
      switch (filterKey) {
        case 'categoryId':
          return 'Categoría';
        case 'colour':
          return 'Color';
        case 'genders':
          return 'Género';
        case 'ages':
          return 'Edad';
        case 'brands':
          return 'Marca';
        case 'sizes':
          return 'Talla';
        case 'sports':
          return 'Deporte';
        case 'teams':
          return 'Equipo';
        default:
          return filterKey;
      }
    },
    fetchFilteredProducts() {
      this.$store.dispatch('fetchFilteredProducts', { categoryId: this.categoryId });
    },
    applyFilter(key, value) {
      if (key === 'brands' && value) {
        const brands = this.$store.state.brands;
        for (const brand of brands) {
          if (brand.name === value || brand.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'brandId', value: brand.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'sports' && value) {
        const sports = this.$store.state.sports;
        for (const sport of sports) {
          if (sport.name === value || sport.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'sportId', value: sport.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'colour' && value) {
        const colours = this.$store.state.colours;
        for (const colour of colours) {
          if (colour.name === value || colour.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'colourId', value: colour.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'teams' && value) {
        const teams = this.$store.state.teams;
        for (const team of teams) {
          if (team.name === value || team.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'teamId', value: team.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'sizes' && value) {
        const sizes = this.$store.state.sizes;
        for (const size of sizes) {
          if (size.name === value || size.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'sizeId', value: size.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'genders' && value) {
        const genders = this.$store.state.genders;
        for (const gender of genders) {
          if (gender.name === value || gender.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'genderId', value: gender.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
      if (key === 'ages' && value) {
        const ages = this.$store.state.ages;
        for (const age of ages) {
          if (age.name === value || age.name.includes(value)) {
            this.$store.dispatch('updateFilter', { filterName: 'ageId', value: age.id });
            this.resetPagination(); // Reset pagination when a filter is applied
            return;
          }
        }
        console.error('No se encontró un ID válido para la marca seleccionada');
      }
    },
    removeFilter(filterKey, value) {
      if (filterKey === 'categoryId') {
        this.$store.dispatch('updateFilter', { filterName: filterKey, value: null });
      } else {
        this.$store.dispatch('removeFilter', { filterName: filterKey, value });
      }
      this.resetPagination(); // Reset pagination when a filter is removed
    },
    isCategory(filterKey) {
      return filterKey === 'categoryId';
    },
    resetPagination() {
      this.currentPage = 1;
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
.filter-item {
  display: flex;
  align-items: center;
  background-color: #f7bc1a;
  border-radius: 5px;
  padding: 5px 10px;
  margin: 5px;
  cursor: pointer;
}
.filter-value {
  flex: 1;
}
.close-button {
  margin-left: auto;
  cursor: pointer;
}
.filter-item:hover {
  background-color: #dda91a;
}
.filter-group {
  border: 1px solid #ccc;
  border-radius: 5px;
  padding: 10px;
  margin-bottom: 10px;
}
.filter-option {
  display: flex;
  align-items: center;
  font-size: 0.9rem;
  margin-bottom: 5px;
  cursor: pointer;
}
.filter-option .circle {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  border: 1px solid #333;
  margin-right: 5px;
}
.products-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}
.products-header h2 {
  font-size: 2rem;
}
.products-container ul {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-wrap: wrap;
}
.card-container {
  padding: 10px;
}
.card-container img {
  width: 100%;
  min-width: 318px;
  height: 300px;
  object-fit: cover;
}
.card-container h3 {
  margin: 10px;
  font-size: 1.2rem;
}
.card-container p {
  margin: 10px;
  font-size: 1rem;
}
.card-container button {
  margin: 10px;
}
.custom-pagination {
  display: flex;
  justify-content: flex-end;
  margin-top: 20px;
}
</style>