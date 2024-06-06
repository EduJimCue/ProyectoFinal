<template>
  <div style="min-height: 71.5vh;">
  <div class="edit-account">
    <h1>Edita tus datos</h1>
    <b-form @submit.prevent="submitForm">
      <b-form-group label="Nombre:" label-for="name">
        <b-form-input id="name" v-model="editedUser.name" required></b-form-input>
      </b-form-group>
      <b-form-group label="Email:" label-for="email">
        <b-form-input id="email" type="email" v-model="editedUser.email" required></b-form-input>
      </b-form-group>
      <b-form-group label="Dirección de envío:" label-for="adress">
        <b-form-input id="address" v-model="editedUser.adress" required></b-form-input>
      </b-form-group>
      <b-form-group label="Nueva Contraseña:" label-for="password">
        <b-form-input id="password" type="password" v-model="editedUser.password"></b-form-input>
      </b-form-group>
      <b-form-group label="Para validar cambios introducir contraseña actual:" label-for="currentPassword">
        <b-form-input id="currentPassword" type="password" v-model="currentPassword" required></b-form-input>
      </b-form-group>
      <b-button type="submit" variant="primary">Guardar Cambios</b-button>
    </b-form>
  </div>
</div>
</template>

<script>
export default {
  data() {
    return {
      editedUser: {
        name: '',
        email: '',
        adress: '',
        role: null,
        signUpDate: null,
        password: null
      },
      currentPassword: ''
    };
  },
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  created() {
    this.editedUser = { ...this.user };
  },
  methods: {
    submitForm() {
      if (this.currentPassword !== this.user.password) {
        window.alert('La contraseña actual es incorrecta');
        return;
      }
      this.$store.dispatch('actualizeUser', this.editedUser);
      this.$router.push('/');
    }
  }
};
</script>

<style scoped>
.edit-account {
  max-width: 600px;
  margin: 0 auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.edit-account h1 {
  text-align: center;
  margin-bottom: 20px;
}

.edit-account form {
  display: flex;
  flex-direction: column;
}

.edit-account form > div {
  margin-bottom: 15px;
}

.edit-account label {
  font-weight: bold;
}

.edit-account input {
  width: 100%;
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.edit-account button {
  padding: 10px;
  font-size: 16px;
  color: #fff;
  background-color: #007bff;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

.edit-account button:hover {
  background-color: #0056b3;
}
</style>