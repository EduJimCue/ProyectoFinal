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
      <div class="edit-attribute">
        <h2>Editar {{ $route.params.attribute }}</h2>
        <b-form @submit.prevent="editAttribute">
          <b-form-group label="Nombre del atributo:" label-for="attributeName">
            <b-form-input id="attributeName" v-model="editedAttributeName" required></b-form-input>
          </b-form-group>
          <b-form-group v-if="isTeamAttribute" label="Contraseña:" label-for="attributePassword">
            <b-form-input type="password" id="attributePassword" v-model="editedAttributePassword" required></b-form-input>
          </b-form-group>
          <b-button type="submit" variant="primary">Guardar cambios</b-button>
        </b-form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      editedAttributeName: '',
      editedAttributePassword: ''
    };
  },
  computed: {
    isTeamAttribute() {
      return this.$route.params.attribute === 'Team';
    },
    // Obtener el atributo específico del store según el parámetro :attribute
    attribute() {
      const attributeStore = this.$store.state[this.$route.params.attribute.toLowerCase() + 's'];
      if (!attributeStore) return null;
      return attributeStore.find(attr => attr.id === parseInt(this.$route.params.attributeId));
    }
  },
  mounted() {
    if (this.attribute) {
      this.editedAttributeName = this.attribute.name;
      if (this.isTeamAttribute && this.attribute.password) {
        this.editedAttributePassword = this.attribute.password;
      }
    }
  },
  methods: {
    editAttribute() {
      const attributeName = this.$route.params.attribute;
      const attributeId = this.$route.params.attributeId;
      const attributeData = {
        id: attributeId,
        name: this.editedAttributeName,
        ...(this.isTeamAttribute && { password: this.editedAttributePassword })
      };
  
      this.$store.dispatch('actualizeAttribute', { attributeName, attributeData })
        .then(() => {
          this.$store.dispatch(`fetch${attributeName}s`);
          this.$router.push('/adminAttributes');
        })
        .catch(error => {
          console.error('Error al actualizar el atributo:', error);
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

.edit-attribute {
  max-width: 600px;
  margin: 0 auto;
}

.edit-attribute h2 {
  text-align: center;
  margin-bottom: 20px;
}

.edit-attribute form {
  display: flex;
  flex-direction: column;
}

.edit-attribute .form-group {
  margin-bottom: 15px;
}

.edit-attribute label {
  font-weight: bold;
}

.edit-attribute select,
.edit-attribute input,
.edit-attribute textarea {
  width: 100%;
  padding: 10px;
  border: 2px solid #ccc;
  border-radius: 5px;
  font-size: 16px;
}

.edit-attribute select:focus,
.edit-attribute input:focus,
.edit-attribute textarea:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
  outline: none;
}

.edit-attribute button {
  padding: 10px;
  font-size: 18px;
  color: #fff;
  background-color: #f7bc1a;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.edit-attribute button:hover {
  background-color: #cf9f1a;
}
</style>