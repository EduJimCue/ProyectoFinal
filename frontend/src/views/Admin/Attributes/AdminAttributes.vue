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
      <div class="attribute-template">
        <h2>Administrar Atributos</h2>
        <h3>Selecciona el atributo a gestionar</h3>
        <b-form-select v-model="selectedAttribute" :options="attributeOptions" class="form-control" />

        <div v-if="selectedAttribute">
          <h3>Elementos de {{ selectedAttribute }}</h3>
          <router-link :to="`/addAttributes/${selectedAttribute}`">
            <b-button variant="success" class="mb-3">Añadir {{ selectedAttribute }}</b-button>
          </router-link>
          <b-table striped hover :items="selectedElements" :fields="fields">
            <template #cell(name)="data">
              <router-link :to="`/editAttributes/${selectedAttribute}/${data.item.id}`">{{ data.item.name }}</router-link>
            </template>
            <template #cell(Accion)="data">
              <b-button variant="danger" @click="deleteElement(selectedAttribute, data.item.id)">Eliminar</b-button>
            </template>
          </b-table>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      selectedAttribute: null,
      attributeOptions: ['Age', 'Brand', 'Category', 'Colour', 'Gender', 'Sport', 'Size', 'Team'],
      fields: ['name', 'Accion']
    };
  },
  computed: {
    selectedElements() {
      switch (this.selectedAttribute) {
        case 'Age':
          return this.$store.state.ages;
        case 'Brand':
          return this.$store.state.brands;
        case 'Category':
          return this.$store.state.categories;
        case 'Colour':
          return this.$store.state.colours;
        case 'Gender':
          return this.$store.state.genders;
        case 'Sport':
          return this.$store.state.sports;
        case 'Size':
          return this.$store.state.sizes;
        case 'Team':
          return this.$store.state.teams;
        default:
          return [];
      }
    }
  },
  mounted() {
    this.attributeOptions.forEach(option => {
      this.fetchElements(option);
    });
  },
  methods: {
    fetchElements(attribute) {
      this.$store.dispatch(`fetch${attribute}s`);
    },
    deleteElement(attribute, elementId) {
      this.$store.dispatch(`deleteAttribute`, {
        attributeName: attribute,
        attributeId: elementId
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

.attribute-template {
  max-width: 600px;
  margin: 0 auto;
}

.attribute-template h2, .attribute-template h3 {
  text-align: center;
  margin-bottom: 20px;
}

.attribute-template .form-control {
  margin-bottom: 20px;
  width: 100%;
  padding: 10px;
  border: 2px solid #ccc;
  border-radius: 5px;
  font-size: 16px;
}

.attribute-template .form-control:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
  outline: none;
}
</style>