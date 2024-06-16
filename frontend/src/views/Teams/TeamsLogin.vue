<template>
  <div class="login">
    <div class="login-container">
      <h1>Introduce los datos de tu equipo</h1>
      <b-form @submit.prevent="login" class="form-team">
        <b-form-group id="input-group-1" label="Equipo:" label-for="input-1">
          <b-form-select v-model="form.team" :options="teamOptions" required class="custom-select"></b-form-select>
        </b-form-group>

        <b-form-group id="input-group-2" label="Password:" label-for="input-2">
          <b-form-input
            id="input-2"
            v-model="form.password"
            type="password"
            placeholder="Introduce la contraseña"
            required
          ></b-form-input>
        </b-form-group>
        <b-button type="submit" class="button">Enviar</b-button>
      </b-form>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      form: {
        team: '',
        password: '',
      },
    };
  },
  computed: {
    teamOptions() {
      return this.$store.state.teams.map(team => ({
        value: team.id,
        text: team.name,
      }));
    },
  },
  methods: {
    login() {
      this.$store.dispatch('verifyTeam', {
        teamId: this.form.team,
        password: this.form.password,
      }).then(() => {
        this.$router.push({ name: 'ProductTeamListing' });
      }).catch(error => {
        window.alert(error.message);
      });
    },
  },
};
</script>

<style lang="scss" scoped>
.login {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 71.5vh;
  background-color: #f5f5f5;
  padding: 20px;
}

.login-container {
  background-color: #fff;
  padding: 40px;
  border-radius: 10px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  text-align: center;
  max-width: 500px;
  width: 100%;
}

.login-container h1 {
  margin-bottom: 30px;
  font-size: 24px;
  color: #333;
}

.form-team .form-group {
  margin-bottom: 20px;
}

.custom-select {
  border: 1px solid #ccc;
  border-radius: 5px;
  padding: 10px;
  font-size: 16px;
  width: 100%;
  height: auto;
  -webkit-appearance: none;
  -moz-appearance: none;
  appearance: none;
  background-color: #fff;
  padding-right: 30px;
  box-shadow: none;
}

.custom-select:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.25);
}

.b-form-input {
  border: 1px solid #ccc;
  border-radius: 5px;
  padding: 10px;
  font-size: 16px;
  width: 100%;
  height: auto;
}

.b-form-input:focus {
  border-color: #007bff;
  box-shadow: 0 0 5px rgba(0, 123, 255, 0.25);
}

.button {
  background-color: #f7bc1a;
  font-weight: bold;
  margin-top: 20px;
  width: 100%;
  padding: 10px;
  border: none;
  border-radius: 5px;
  font-size: 16px;
  color: #fff;
  transition: background-color 0.3s ease;
}

.button:hover {
  background-color: #e0a800;
}
</style>