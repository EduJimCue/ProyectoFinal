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
      <div class="add-attribute">
        <h2>Añadir {{ $route.params.attribute }}</h2>
        <b-form @submit.prevent="addAttribute">
          <b-form-group label="Nombre del atributo:" label-for="attributeName">
            <b-form-input id="attributeName" v-model="newAttributeName" required></b-form-input>
          </b-form-group>
          <b-form-group v-if="isTeamAttribute" label="Contraseña:" label-for="password">
            <b-form-input type="password" id="password" v-model="password" required></b-form-input>
          </b-form-group>
          <b-button type="submit" variant="success">Agregar</b-button>
        </b-form>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      newAttributeName: '',
      password: ''
    };
  },
  computed: {
    isTeamAttribute() {
      return this.$route.params.attribute === 'Team';
    }
  },
  methods: {
    addAttribute() {
      const attributeName = this.$route.params.attribute;
      const attributeData = {
        name: this.newAttributeName
      };
      if (this.isTeamAttribute) {
        attributeData.password = this.password;
      }

      this.$store.dispatch('addAttribute', { attributeName, attributeData })
        .then(() => {
          this.newAttributeName = '';
          this.password = '';
          this.$router.push('/adminAttributes');
        })
        .catch(error => {
          console.error('Error al agregar el atributo:', error);
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

.add-attribute {
  max-width: 600px;
  margin: 0 auto;
}

.add-attribute h2 {
  text-align: center;
  margin-bottom: 20px;
}

.add-attribute form {
  display: flex;
  flex-direction: column;
}

.add-attribute .form-group {
  margin-bottom: 15px;
}

.add-attribute label {
  font-weight: bold;
}

.add-attribute select,
.add-attribute input,
.add-attribute textarea {
  width: 100%;
  padding: 10px;
  border: 2px solid #ccc;
  border-radius: 5px;
  font-size: 16px;
}

.add-attribute select:focus,
.add-attribute input:focus,
.add-attribute textarea:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
  outline: none;
}

.add-attribute button {
  padding: 10px;
  font-size: 18px;
  color: #fff;
  background-color: #28a745;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.add-attribute button:hover {
  background-color: #218838;
}
</style>
