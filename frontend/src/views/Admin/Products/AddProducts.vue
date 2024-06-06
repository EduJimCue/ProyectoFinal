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
      <div class="add-product">
        <h1>Agregar Nuevo Producto</h1>
        <b-form @submit.prevent="addProduct">
          <b-form-group label="Nombre:" label-for="name">
            <b-form-input id="name" v-model="formData.name" required></b-form-input>
          </b-form-group>
          <b-form-group label="Precio:" label-for="price">
            <b-form-input id="price" type="number" v-model.number="formData.price" required></b-form-input>
          </b-form-group>
          <b-form-group label="Deporte:" label-for="sport">
            <b-form-select id="sport" v-model="formData.sportId" :options="sportsOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Marca:" label-for="brand">
            <b-form-select id="brand" v-model="formData.brandId" :options="brandsOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Equipo:" label-for="team">
            <b-form-select id="team" v-model="formData.teamId" :options="teamsOptions"></b-form-select>
          </b-form-group>
          <b-form-group label="Color:" label-for="colour">
            <b-form-select id="colour" v-model="formData.colourId" :options="colourOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Categoria:" label-for="category">
            <b-form-select id="category" v-model="formData.categoryId" :options="categoriesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Edad:" label-for="age">
            <b-form-select id="age" v-model="formData.ageId" :options="agesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Género:" label-for="gender">
            <b-form-select id="gender" v-model="formData.genderId" :options="gendersOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Imagen Principal:" label-for="image">
            <b-form-select id="image" v-model="formData.principalImage" :options="imagesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Imagen Secundaria 1:" label-for="secondary1">
            <b-form-select id="secondary1" v-model="formData.secondImage" :options="imagesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Imagen Secundaria 2:" label-for="secondary2">
            <b-form-select id="secondary2" v-model="formData.thirdImage" :options="imagesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Imagen Secundaria 3:" label-for="secondary3">
            <b-form-select id="secondary3" v-model="formData.fourthImage" :options="imagesOptions" required></b-form-select>
          </b-form-group>
          <b-form-group label="Descripción:" label-for="description">
            <b-form-textarea id="description" v-model="formData.description" required></b-form-textarea>
          </b-form-group>
          <b-button type="submit">Agregar Producto</b-button>
        </b-form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      formData: {
        id: 0,
        name: '',
        price: 0,
        sportId: 0,
        brandId: 0,
        teamId: 0,
        colourId: 0,
        categoryId: 0,
        ageId: 0,
        genderId: 0,
        isActive: false,
        principalImage: '',
        secondImage: '',
        thirdImage: '',
        fourthImage: '',
        description: ''
      }
    };
  },
  computed: {
    sports() {
      return this.$store.state.sports;
    },
    brands() {
      return this.$store.state.brands;
    },
    categories() {
      return this.$store.state.categories;
    },
    colours() {
      return this.$store.state.colours;
    },
    teams() {
      return this.$store.state.teams;
    },
    ages() {
      return this.$store.state.ages;
    },
    genders() {
      return this.$store.state.genders;
    },
    images() {
      return this.$store.state.images;
    },
    sportsOptions() {
      return this.sports.map(sport => ({ value: sport.id, text: sport.name }));
    },
    brandsOptions() {
      return this.brands.map(brand => ({ value: brand.id, text: brand.name }));
    },
    categoriesOptions() {
      return this.categories.map(category => ({ value: category.id, text: category.name }));
    },
    colourOptions() {
      return this.colours.map(colour => ({ value: colour.id, text: colour.name }));
    },
    teamsOptions() {
      return this.teams.map(team => ({ value: team.id, text: team.name }));
    },
    agesOptions() {
      return this.ages.map(age => ({ value: age.id, text: age.name }));
    },
    gendersOptions() {
      return this.genders.map(gender => ({ value: gender.id, text: gender.name }));
    },
    imagesOptions() {
      return this.images.map(image => ({ value: image.url, text: image.name }));
    }
  },
  methods: {
    addProduct() {
      this.$store.dispatch('addProduct', this.formData)
        .then(
          this.clearForm(),
          this.$router.push('/adminProducts')
        )
        .catch(error => {
          console.error('Error al agregar el producto:', error);
        });
    },
    clearForm() {
      this.formData = {
        name: '',
        price: 0,
        sportId: 0,
        brandId: 0,
        teamId: 0,
        colourId: 0,
        categoryId: 0,
        ageId: 0,
        genderId: 0,
        isActive: false,
        principalImage: '',
        secondImage: '',
        thirdImage: '',
        fourthImage: '',
        description: ''
      };
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

.add-product {
  max-width: 600px;
  margin: 0 auto;
}

.add-product h1 {
  text-align: center;
  margin-bottom: 20px;
}

.add-product form {
  display: flex;
  flex-direction: column;
}

.add-product .form-group {
  margin-bottom: 15px;
}

.add-product label {
  font-weight: bold;
}

.add-product select,
.add-product input,
.add-product textarea {
  width: 100%;
  padding: 10px;
  border: 2px solid #ccc;
  border-radius: 5px;
  font-size: 16px;
}

.add-product select:focus,
.add-product input:focus,
.add-product textarea:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
  outline: none;
}

.add-product button {
  padding: 10px;
  font-size: 18px;
  color: #fff;
  background-color: #f7bc1a;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.add-product button:hover {
  background-color: #cf9f1a;
}
</style>
