<template>
  <div>
    <h1>Introduce tus datos</h1>
    <b-form @submit="onSubmit" @reset="onReset" class="form-user">
      <b-form-group id="input-group-1" label="Nombre:" label-for="input-1">
        <b-form-input
          id="input-1"
          v-model="form.name"
          placeholder="Introduce tu nombre"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-2" label="Email:" label-for="input-2">
        <b-form-input
          id="input-2"
          v-model="form.email"
          placeholder="Introduce tu e-mail"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-3" label="Contraseña:" label-for="input-3">
        <b-form-input
          id="input-3"
          v-model="form.password"
          type="password"
          placeholder="Introduce tu contraseña"
          required
        ></b-form-input>
      </b-form-group>
      <b-form-group id="input-group-4" label="Dirección:" label-for="input-4">
        <b-form-input
          id="input-4"
          v-model="form.address"
          placeholder="Introduce tu dirección"
          required
        ></b-form-input>
      </b-form-group>
      <b-button type="submit" variant="primary" class="button submit">Submit</b-button>
      <b-button type="reset" variant="danger" class="button reset">Reset</b-button>
    </b-form>
    <b-alert v-if="error" variant="danger" show dismissible @dismissed="error = null" class="mt-3">
      {{ error }}
    </b-alert>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        name: '',
        email: '',
        password: '',
        address: '',
        role: 0,
        signUpDate: new Date().toISOString()
      },
      error: null
    }
  },
  methods: {
    onSubmit(event) {
      event.preventDefault();
      this.error = null;  // Reset error message
      const formToSend = {
        name: this.form.name,
        email: this.form.email,
        password: this.form.password,
        address: this.form.address,
        role: !!this.form.role,
        signUpDate: new Date().toISOString()
      };
      this.$store.dispatch('addUser', formToSend)
        .then(() => {
          this.$router.push('/');
        })
        .catch(error => {
          this.error = error.message;
        });
    },
    onReset(event) {
      event.preventDefault();
      this.form.name = '';
      this.form.email = '';
      this.form.password = '';
      this.form.address = '';
      this.error = null;
    }
  }
}
</script>

<style lang="scss" scoped>
.form-user {
  width: 500px;
  margin: 0 auto;
}

.button {
  font-weight: bold;
  margin: 20px 10px;
}

.reset {
  background-color: #3E4646;
  border-color: #3E4646;
}

.submit {
  background-color: #f7bc1a;
  border-color: #f7bc1a;
}

.mt-3 {
  margin-top: 20px;
}
</style>