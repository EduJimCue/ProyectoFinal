<template>
  <div style="min-height: 71.5vh;">
  <div class="user-account-container">
    <div class="user-header">
      <div class="profile-image">
        <svg xmlns="http://www.w3.org/2000/svg" width="4rem" height="4rem" fill="currentColor" class="bi bi-person-circle" viewBox="0 0 16 16">
          <path d="M13.468 12.37C12.758 11.226 11.373 10.5 8 10.5s-4.758.726-5.468 1.87A6.996 6.996 0 0 1 1 8a7 7 0 1 1 12.468 4.37zM8 15A7 7 0 1 0 8 1a7 7 0 0 0 0 14zm0-7a3 3 0 1 0 0-6 3 3 0 0 0 0 6z"/>
        </svg>
      </div>
      <div class="user-info">
        <h1>{{ user.name }}</h1>
        <p class="member-since">Member since: {{ formatMemberSince(user.signUpDate) }}</p>
      </div>
    </div>
    <div class="user-actions">
      <router-link to="/myOrders" class="user-action">
        <div class="action-icon">
          <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-cart-check" viewBox="0 0 16 16">
           <path d="M11.354 6.354a.5.5 0 0 0-.708-.708L8 8.293 6.854 7.146a.5.5 0 1 0-.708.708l1.5 1.5a.5.5 0 0 0 .708 0z"/>
           <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
          </svg>
        </div>
        <div class="action-info">
          <h3>Mis pedidos y devoluciones</h3>
          <p>Consulta el estado de tus pedidos y gestiona tus anulaciones, devoluciones o cambios de talla.</p>
        </div>
      </router-link>
      <router-link to="/editMyData" class="user-action">
        <div class="action-icon">
          <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-person-bounding-box" viewBox="0 0 16 16">
            <path d="M1.5 1a.5.5 0 0 0-.5.5v3a.5.5 0 0 1-1 0v-3A1.5 1.5 0 0 1 1.5 0h3a.5.5 0 0 1 0 1zM11 .5a.5.5 0 0 1 .5-.5h3A1.5 1.5 0 0 1 16 1.5v3a.5.5 0 0 1-1 0v-3a.5.5 0 0 0-.5-.5h-3a.5.5 0 0 1-.5-.5M.5 11a.5.5 0 0 1 .5.5v3a.5.5 0 0 0 .5.5h3a.5.5 0 0 1 0 1h-3A1.5 1.5 0 0 1 0 14.5v-3a.5.5 0 0 1 .5-.5m15 0a.5.5 0 0 1 .5.5v3a1.5 1.5 0 0 1-1.5 1.5h-3a.5.5 0 0 1 0-1h3a.5.5 0 0 0 .5-.5v-3a.5.5 0 0 1 .5-.5"/>
            <path d="M3 14s-1 0-1-1 1-4 6-4 6 3 6 4-1 1-1 1zm8-9a3 3 0 1 1-6 0 3 3 0 0 1 6 0"/>
          </svg>
        </div>
        <div class="action-info">
          <h3>Mis datos personales</h3>
          <p>Modifica toda tu información personal.</p>
        </div>
      </router-link>
      <div v-if="user.role" class="user-action">
        <router-link to="/adminMenu" class="admin-link">
          <div class="action-icon">
            <svg xmlns="http://www.w3.org/2000/svg" width="40" height="40" fill="currentColor" class="bi bi-lock" viewBox="0 0 16 16">
              <path d="M8 1a2 2 0 0 1 2 2v4H6V3a2 2 0 0 1 2-2m3 6V3a3 3 0 0 0-6 0v4a2 2 0 0 0-2 2v5a2 2 0 0 0 2 2h6a2 2 0 0 0 2-2V9a2 2 0 0 0-2-2M5 8h6a1 1 0 0 1 1 1v5a1 1 0 0 1-1 1H5a1 1 0 0 1-1-1V9a1 1 0 0 1 1-1"/>
            </svg>
          </div>
          <div class="action-info">
            <h3>Panel de Admin</h3>
            <p>Accede al panel de administración.</p>
          </div>
        </router-link>
      </div>
    </div>
  </div>
</div>
</template>


<script>
export default {
  computed: {
    user() {
      return this.$store.state.user;
    }
  },
  methods:{
    formatMemberSince(dateString) {
      const date = new Date(dateString);
      return date.toLocaleDateString('es-ES');
    },
    logout() {
      this.$store.dispatch('logout');
    }
  }
};
</script>

<style scoped>
.user-account-container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 20px;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
}

.user-header {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.profile-image {
  font-size: 4rem;
  margin-right: 20px;
}

.user-info {
  display: flex;
  flex-direction: column;
}

.user-info h1 {
  margin: 0;
  font-size: 24px;
}

.member-since {
  font-size: 14px;
  color: #777;
}

.logout-link {
  margin-top: 10px;
  color: #007bff;
  text-decoration: none;
}

.logout-link:hover {
  text-decoration: underline;
}

.user-actions {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.user-action {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  width: 30%;
  padding: 20px;
  background-color: #f9f9f9;
  border: 1px solid #e7e7e7;
  border-radius: 8px;
  text-align: center;
  text-decoration: none;
  color: #333;
  transition: background-color 0.3s, box-shadow 0.3s;
}

.user-action:hover {
  background-color: #f1f1f1;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.action-info h3 {
  margin: 10px 0 5px;
  font-size: 18px;
}

.action-info p {
  margin: 0;
  font-size: 14px;
  color: #777;
}
</style>