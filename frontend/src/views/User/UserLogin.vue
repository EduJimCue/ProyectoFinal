<template>
    <div class="login">
      <h1>Introduce your log in details</h1>
      <b-form @submit="login" class="form-user">
      <b-form-group
        id="input-group-1"
        label="Username:"
        label-for="input-1"
      >
        <b-form-input
          id="input-1"
          v-model="form.username"
          placeholder="Enter email or name"
          required
        ></b-form-input>
      </b-form-group>

      <b-form-group id="input-group-2" label="Password:" label-for="input-2">
        <b-form-input
          id="input-2"
          v-model="form.password"
          type="password"
          placeholder="Enter password"
          required
        ></b-form-input>
      </b-form-group>
      <b-button type="submit" class="button">Send</b-button>
    </b-form>
    </div>
  </template>
  <script>
    export default {
      data() {
        return {
          form:{
            username:'',
            password:'',
          }
        }
      },
      methods: {
        login() {
          event.preventDefault();
          this.$store.dispatch('fetchUser', { username: this.form.username, password: this.form.password })
        }
      }
    }
  </script>
<style lang="scss" scoped>
    .login{
        height: 76.7vh;
    }

    .form-user {
    width: 400px;
    margin: 0 auto;
    }

    .button {
    background-color: #f7bc1a;
    font-weight: bold;
    margin-top: 20px;
    }


</style>