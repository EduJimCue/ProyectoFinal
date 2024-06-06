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
      <div class="image-gallery">
        <div class="upload-section">
          <h2>Galería de Imágenes</h2>
          <b-form @submit.prevent="uploadFile">
            <b-form-file ref="fileInput" placeholder="Elige una imagen..." drop-placeholder="Arrastra la imagen aquí..."></b-form-file>
            <b-button type="submit" variant="primary" class="mt-3">Subir imagen</b-button>
          </b-form>
        </div>
        <b-table striped hover :items="images" :fields="fields" class="mt-4">
          <template #cell(url)="data">
            <div class="image-cell">
              <img :src="data.item.url" alt="Image" class="image-preview">
              {{ data.item.url }}
            </div>
          </template>
          <template #cell(Accion)="data">
            <b-button variant="danger" @click="deleteImage(data.item.id)">Eliminar</b-button>
          </template>
        </b-table>
        <div v-if="images.length === 0">
          <p>No hay imágenes disponibles.</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ImageGallery',
  computed: {
    images() {
      return this.$store.state.images;
    },
    fields() {
      return ['url', 'Accion'];
    }
  },
  methods: {
    async uploadFile() {
      const file = this.$refs.fileInput.files[0];
      if (!file) {
        console.error('No se proporcionó ningún archivo.');
        return;
      }
      await this.$store.dispatch('uploadImage', file);
      this.$refs.fileInput.value = '';
    },
    deleteImage(imageId) {
      this.$store.dispatch('deleteImage', imageId);
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

.image-gallery {
  max-width: 800px;
  margin: 0 auto;
}

.upload-section {
  margin-bottom: 20px;
}

.image-cell {
  display: flex;
  align-items: center;
}

.image-preview {
  width: 100px;
  height: 100px;
  margin-right: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  object-fit: cover;
}
</style>