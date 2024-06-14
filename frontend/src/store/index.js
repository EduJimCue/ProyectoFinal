// store/index.js
import Vue from 'vue';
import Vuex from 'vuex';
import router from '@/router'

Vue.use(Vuex);
const savedState = localStorage.getItem('vuexState');
const initialState = savedState ? JSON.parse(savedState) : {
  user: null,
  userOrders: [],
  auth: 0,
  products: [],
  adminProducts:[],
  productDetail:{
    id: null,
    name: null,
    price: null,
    colourId: null,
    description: null,
    principalImage: null,
    secondImage:null,
    thirdImage:null,
    fourthImage:null
  },
  filteredProducts:[],
  filters: {
    brandId: null,
    sportId: null,
    categoryId: null,
    colourId: null,
    teamId: null,
    ageId: null,
    genderId: null,
    sizeId: null,
    minPrice: null,
    maxPrice: null
  },
  similarProducts:[],
  lastFour:[],
  randomFour:[],
  filteredFilters: [],
  ages: [],
  sports: [],
  categories: [],
  colours: [],
  otherColours:[],
  teams: [],
  verifiedTeam:null,
  genders: [],
  sizes: [],
  availableSizes: [],
  availableStock:[],
  images: [],
  otherImages:[],
  brands: [],
  cart: []
};

const store = new Vuex.Store({
  state: initialState,
  mutations: {
    setUser(state, payload) {
      if (payload!=null) {
     router.push("/")
      }
      state.user = payload
      state.auth = (payload && payload.role) ? 1 : 0;
    },
    setUserOrders(state, payload) {
      state.userOrders = payload;
    },
    addUser(state, newUser) {
      state.user = newUser; 
    },
    setProducts(state, payload) {
      state.products = payload;
    },
    setAdminProducts(state, payload) {
      state.adminProducts = payload;
    },
    setLastFour(state, payload) {
      state.lastFour = payload;
    },
    setRandomFour(state, payload) {
      state.randomFour = payload;
    },
    addProduct(state, newProduct) {
      state.adminProducts.push(newProduct);
    },
    setProductDetail(state, payload) {
      state.productDetail.id = payload.id;
      state.productDetail.name = payload.name;
      state.productDetail.colourId = payload.colourId;
      state.productDetail.description = payload.description;
      state.productDetail.principalImage = payload.principalImage;
      state.productDetail.secondImage = payload.secondImage;
      state.productDetail.thirdImage = payload.thirdImage;
      state.productDetail.fourthImage = payload.fourthImage;
      state.productDetail.price = payload.price;
    },
    setSimilarProducts(state, payload) {
      state.similarProducts = payload;
    },
    clearProducts(state) {
      state.products = [];
    },
    setCategories(state, payload) {
      state.categories = payload;
    },
    addCategory(state, newCategory) {
      state.categories.push(newCategory);
    },
    setAges(state, payload) {
      state.ages = payload;
    },
    addAge(state, newAge) {
      state.ages.push(newAge);
    },
    setColours(state, payload) {
      state.colours = payload;
    },
    addColour(state, newColour) {
      state.colours.push(newColour);
    },
    setOtherColours(state, payload) {
      state.otherColours = payload;
    },
    setSports(state, payload) {
      state.sports = payload;
    },
    addSport(state, newSport) {
      state.sports.push(newSport);
    },
    setSizes(state, payload) {
      state.sizes = payload;
    },
    addSize(state, newSize) {
      state.sizes.push(newSize);
    },
    setAvailableSizes(state, payload) {
      state.availableSizes = payload;
    },
    setAvailableStock(state, payload) {
      state.availableStock = payload;
    },
    setGenders(state, payload) {
      state.genders = payload;
    },
    addGender(state, newGender) {
      state.genders.push(newGender);
    },
    setImages(state, payload) {
      state.images = payload;
    },
    addImage(state, newImage) {
      state.images.push(newImage);
    },
    setBrands(state, payload) {
      state.brands = payload;
    },
    addBrand(state, newBrand) {
      state.brands.push(newBrand);
    },
    setTeams(state, payload) {
      state.teams = payload;
    },
    addTeam(state, newTeam) {
      state.teams.push(newTeam);
    },
    setVerifiedTeam(state, payload) {
      state.verifiedTeam = payload;
    },
    setOtherImages(state, payload){
      state.otherImages=payload
    },
    setFilteredProducts(state, payload) {
      state.filteredProducts = payload.filteredProducts;
    },
    setFilteredFilters(state, payload) {
      state.filteredFilters = payload.filteredFilters;
    },
    updateFilter(state, { filterName, value }) {
      state.filters[filterName] = value;
    },
    removeFilter(state, filterKey) {
      if (filterKey.filterName in state.filters) {
        state.filters[filterKey.filterName] = null;
      }
    },
    cleanCart(state) {
      state.cart = [];
    },
    addToCart(state, productData) {
      state.cart.push(productData);
    },
    removeFromCart(state, index) {
      state.cart.splice(index, 1);
    }
  },
  actions: {
    fetchUser({commit}, { username, password }){
        fetch(`http://localhost/User/${username}/${password}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({
          username: username,
          password: password
        })
      })
      .then(response => {
          if (!response.ok) {
            window.alert("User not found")
          }
          return response.json();
        })
        .then(response => {commit('setUser',response);
      })
      .catch (error => {
        console.error(error);
      })
    },
    logout({ commit }) {
    commit('setUser', null )
    },
    addUser({ commit }, userData) {
      fetch("http://localhost/User", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(userData)
      })
      .then(response => {
        if (!response.ok) {
          return response.text().then(errorMessage => {
            throw new Error(errorMessage);
          });
        }
        return response.json();
      })
      .then(response => {
        commit('addUser', response);
      })
      .catch(error => {
      
        window.alert(error.message);
        console.error(error);
      });
    },
    actualizeUser({ commit }, userData) {
      fetch(`http://localhost/User`, {
      method: 'PUT',
      headers: {
        'Content-type': 'application/json'
      },
      body: JSON.stringify(userData)
    })
      commit('setUser', null)
    }, 
    fetchProducts({ commit }) {
      fetch("http://localhost/Product")
        .then(response => response.json())
        .then(response => commit('setAdminProducts', response));
    },
    async fetchProductDetail({ commit }, productId) {
      try {
        const response = await fetch(`http://localhost/Product/${productId}`);
        const data = await response.json();
        commit('setProductDetail', data);
      } catch (error) {
        console.log(error);
      }
    },
    fetchLastProducts({ commit }) {
      fetch("http://localhost/Product/LastFour")
        .then(response => response.json())
        .then(response => commit('setLastFour', response));
    },
    fetchRecommendedProducts({ commit }) {
      fetch("http://localhost/Product/RandomFour")
        .then(response => response.json())
        .then(response => commit('setRandomFour', response));
    },
    searchProducts({ commit }, partialName) {
      if (!partialName.trim()) {
        commit('clearProducts');
        return;
      }
      const url = `http://localhost/Product/partialName?partialName=${encodeURIComponent(partialName)}`;

      fetch(url)
        .then(response => response.json())
        .then(products => {
          commit('setProducts', products);
        })
        .catch(error => {
          console.error('Error fetching products:', error);
        });
    },
    addProduct({ commit }, productData) {
      return fetch("http://localhost/Product", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(productData)
      })
      .then(response => response.json())
      .then(product => {
        commit('addProduct', product);
        return product; 
      });
    },
    actualizeProduct({ commit }, productData) {
      fetch("http://localhost/Product", {
          method: `PUT`,
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify(productData),
        })
        .then(() => {
          console.log(commit)
          this.dispatch('fetchProducts');
        })
    },
    deleteProduct({ commit }, productId) {
      fetch(`http://localhost/Product?Id=${productId}`, {
        method: `DELETE`,
        headers: {
          "Content-type": "application/json"
        }
      })
        .then(() => {
          console.log(commit)
          this.dispatch('fetchProducts');
        })
    },
    fetchCategorys({ commit }) {
      fetch("http://localhost/Category/GetAllCategories")
        .then(response => response.json())
        .then(response => commit('setCategories', response));
    },
    deleteAttribute({ commit },{ attributeName, attributeId }) {
      fetch(`http://localhost/${attributeName}/Delete${attributeName}?Id=${attributeId}`, {
        method: `DELETE`,
        headers: {
          "Content-type": "application/json"
        }
      })
        .then(() => {
          console.log(commit)
          this.dispatch(`fetch${attributeName}s`);
        })
    },
    addAttribute({ commit }, { attributeName, attributeData }) {
      return fetch(`http://localhost/${attributeName}/Post${attributeName}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(attributeData)
      })
      .then(response => response.json())
      .then(attribute => {
        commit(`add${attributeName}`, attribute);
        return attribute; 
      });
    },
    actualizeAttribute({ commit }, { attributeName, attributeData }) {
      fetch(`http://localhost/${attributeName}/Put${attributeName}`, {
          method: `PUT`,
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify(attributeData),
        })
        .then(() => {
          console.log(commit)
          this.dispatch(`fetch${attributeName}s`);
        })
    },
    fetchImages({ commit }) {
      fetch("http://localhost/S3Images/GetAllImages")
        .then(response => response.json())
        .then(response => commit('setImages', response));
    },
    async uploadImage({ commit }, file) {
      try {
        const formData = new FormData();
        formData.append('file', file);
        const response = await fetch('http://localhost/S3Images/Upload', {
          method: 'POST',
          body: formData
        });
        if (!response.ok) {
          throw new Error('Error al subir la imagen.');
        }
        const data = await response.json();
        console.log('Imagen subida con éxito:', data.imageUrl);
        commit('addImage', { id: data.imageId, url: data.imageUrl });
      } catch (error) {
        alert('Error al subir la imagen. Por favor, inténtelo de nuevo.');
      }
    },
    deleteImage({ commit }, imageId) {
      fetch(`http://localhost/S3Images/Delete?imageId=${imageId}`, {
        method: `DELETE`,
        headers: {
          "Content-type": "application/json"
        }
      })
        .then(() => {
          console.log(commit)
          this.dispatch('fetchImages');
        })
    },
    fetchAges({ commit }) {
      fetch("http://localhost/Age/GetAllAges")
        .then(response => response.json())
        .then(response => commit('setAges', response));
    },
    fetchColours({ commit }) {
      fetch("http://localhost/Colour/GetAllColours")
        .then(response => response.json())
        .then(response => commit('setColours', response));
    },
    fetchOtherColours({ commit },{ productName, colourId }) {
      fetch(`http://localhost/Product/GetSimilarProductsByFullNameAndDifferentColors?name=${productName}&ColourId=${colourId}`)
          .then(response => {
              if (!response.ok) {
                  if (response.status === 404) {
                      throw new Error('Otros colores no encontrados.');
                  } else {
                      throw new Error('Error al obtener otros colores.');
                  }
              }
              return response.json();
          })
          .then(response => commit('setOtherColours', response))
          .catch(error => {
              console.error('Error en la solicitud de colores:', error);
              commit('setOtherColours', []);
          });
    },
    fetchSimilarProducts({ commit }, productId ) {
      fetch(`http://localhost/Product/GetSimilarProducts?productId=${productId}`)
          .then(response => {
              if (!response.ok) {
                  if (response.status === 404) {
                      throw new Error('Otros productos no encontrados.');
                  } else {
                      throw new Error('Error al obtener otros productoss.');
                  }
              }
              return response.json();
          })
          .then(response => commit('setSimilarProducts', response))
          .catch(error => {
              console.error('Error en la solicitud de productss:', error);
              commit('setSimilarProducts', []);
          });
    },
    fetchSports({ commit }) {
      fetch("http://localhost/Sport/GetAllSports")
        .then(response => response.json())
        .then(response => commit('setSports', response));
    },
    fetchSizes({ commit }) {
      fetch("http://localhost/Size/GetAllSizes")
        .then(response => response.json())
        .then(response => commit('setSizes', response));
    },
    async fetchAvailableSizes({ commit }, productId) {
      try {
        const response = await fetch(`http://localhost/SizeProduct/GetSizesByProduct?productId=${productId}`);
        const data = await response.json();
        commit('setAvailableSizes', data);
      } catch (error) {
        console.log(error);
      }
    },
    async fetchAvailableStock({ commit }, { productId, sizeId }) {
      try {
        const response = await fetch(`http://localhost/SizeProduct/GetStock?productId=${productId}&sizeId=${sizeId}`);
        const data = await response.json();
        commit('setAvailableStock', data);
        return data;
      } catch (error) {
        console.log(error);
      }
    },
    actualizeSizeProduct({ commit }, sizeProductData) {
      fetch("http://localhost/SizeProduct/PostSizeProduct", {
          method: `POST`,
          headers: {
            "Content-type": "application/json",
          },
          body: JSON.stringify(sizeProductData),
        })
        .then(() => {
          console.log(commit)
        })
    },
    fetchGenders({ commit }) {
      fetch("http://localhost/Gender/GetAllGenders")
        .then(response => response.json())
        .then(response => commit('setGenders', response));
    },
    fetchBrands({ commit }) {
      fetch("http://localhost/Brand/GetAllBrands")
        .then(response => response.json())
        .then(response => commit('setBrands', response));
    },
    fetchTeams({ commit }) {
      fetch("http://localhost/Team/GetAllTeams")
        .then(response => response.json())
        .then(response => commit('setTeams', response));
    },
    verifyTeam({ commit }, { teamId, password }) {
      return new Promise((resolve, reject) => {
        fetch(`http://localhost/Team/VerifyTeamCredentials?teamId=${teamId}&password=${password}`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json'
          }
        })
        .then(response => {
          if (!response.ok) {
            throw new Error('Equipo no encontrado o contraseña incorrecta');
          }
          return response.json();
        })
        .then(team => {
          commit('setVerifiedTeam', team);
          resolve();
        })
        .catch(error => {
          console.error('Error during team verification:', error);
          reject(error);
        });
      });
    },
    fetchOtherImages({ commit }, productId) {
      fetch(`http://localhost/ImageProduct/GetImagesByProduct?productId=${productId}`)
          .then(response => {
              if (!response.ok) {
                  if (response.status === 404) {
                      throw new Error('Imagen no encontrada.');
                  } else {
                      throw new Error('Error al obtener las imágenes del producto.');
                  }
              }
              return response.json();
          })
          .then(response => commit('setOtherImages', response))
          .catch(error => {
              console.error('Error en la solicitud de imágenes:', error);
              commit('setOtherImages', []);
          });
    },

    addSecondaryImage(_, productData) {
      fetch("http://localhost/ImageProduct/PostImageProduct", {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(productData)
      })
    },

    actualizeSecondaryImage({ commit },{productId, oldId, newId} ) {
      fetch(`http://localhost/ImageProduct/UpdateImage?productId=${productId}&oldImageId=${oldId}&newImageId=${newId}`, {
          method: `PUT`,
          headers: {
            "Content-type": "application/json",
          },
        })
        .then(() => {
          console.log(commit)
          this.dispatch('fetchOtherImages', productId)
        })
    },

    fetchFilteredProducts({ commit, state }) {
      const filters = state.filters;

      const filteredFilters = {};
      for (const key in filters) {
        if (filters[key] !== null) {
          filteredFilters[key] = filters[key];
        }
      }
      const queryString = new URLSearchParams(filteredFilters).toString();

      fetch(`http://localhost/Product/Filter?${queryString}`)
        .then(response => response.json())
        .then(data => {
          commit('setFilteredProducts',{ filteredProducts: data.products });
          commit('setFilteredFilters', { filteredFilters: data.filters });
        })
        .catch(error => {
          console.error('Error fetching filtered products:', error);
        });
    },

    fetchTeamsProducts({ commit}, teamId) {
    
      fetch(`http://localhost/Product/Filter?TeamId=${teamId}`)
        .then(response => response.json())
        .then(data => {
          commit('setFilteredProducts',{ filteredProducts: data.products });
        })
        .catch(error => {
          console.error('Error fetching filtered products:', error);
        });
    },

    updateFilter({ commit }, { filterName, value }) {
      commit('updateFilter', { filterName, value });
    },

    removeFilter({ commit }, filterKey) {
      commit('removeFilter', filterKey);
    },
    addToCart({ commit }, productData) {
      commit('addToCart', productData);
    },
    removeFromCart({ commit }, index) {
      commit('removeFromCart', index);
    },
    cleanCart({ commit }, index) {
      commit('removeFromCart', index);
    },
    async createOrder({ commit, state }) {
      try {
        const currentDate = new Date().toISOString();
        const userId = state.user.id; 
        const orderResponse = await fetch(`http://localhost/Order/PostOrder`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            userId: userId,
            creationDate: currentDate,
            paid: false
          })
        });
        const orderData = await orderResponse.json();
        const orderId = orderData.id; 
  
        const orderProducts = state.cart.map(product => {
          return {
            productId: product.productId,
            sizeId: product.sizeId,
            quantity: product.quantity
          };
        });
  
        const sizeProductIds = await Promise.all(orderProducts.map(async orderProduct => {
          const response = await fetch(`http://localhost/SizeProduct/GetOneOrder?productId=${orderProduct.productId}&sizeId=${orderProduct.sizeId}`);
          const data = await response.json();
          return data.id;
        }));
  
        const orderProductsData = sizeProductIds.map((sizeProductId, index) => {
          return {
            orderId: orderId,
            sizeProductId: sizeProductId,
            quantity: orderProducts[index].quantity
          };
        });
  
        await Promise.all(orderProductsData.map(async orderProductData => {
          await fetch(`http://localhost/OrderProduct/PostOrderProduct`, {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderProductData)
          });
        }));
  
        console.log('Pedido creado exitosamente');
        commit('cleanCart'); 
      } catch (error) {
        console.error('Error al crear el pedido:', error);
      }
    },
    async fetchUserOrders({ commit, state }) {
      try {
        const userId = state.user.id; 
        const ordersResponse = await fetch(`http://localhost/Order/GetOrdersByUser?userId=${userId}`);
        const ordersData = await ordersResponse.json();
    
        const userOrders = [];
    
        for (const order of ordersData) {
          const orderProductsResponse = await fetch(`http://localhost/OrderProduct/GetProductsByOrder?orderId=${order}`);
          const orderProductsData = await orderProductsResponse.json();
    
          const orderProducts = [];
    
          for (const orderProduct of orderProductsData) {
            const sizeProductId = orderProduct.sizeProductId;
    
            const sizeProductResponse = await fetch(`http://localhost/SizeProduct/GetOneSizeProduct?Id=${sizeProductId}`);
            const sizeProductData = await sizeProductResponse.json();
            const productId = sizeProductData.productId;
            const sizeId = sizeProductData.sizeId;
  
            const productResponse = await fetch(`http://localhost/Product/${productId}`);
            const productData = await productResponse.json();
    
            const sizeResponse = await fetch(`http://localhost/Size/GetOneSize?Id=${sizeId}`);
            const sizeData = await sizeResponse.json();
    
            orderProducts.push({
              id: productId,
              name: productData.name,
              price: productData.price,
              size: sizeData.name,
              quantity: orderProduct.quantity
            });
          }
    
          userOrders.push({
            orderId: order,
            products: orderProducts
          });
        }
    
        commit('setUserOrders', userOrders);
      } catch (error) {
        console.error('Error al obtener los pedidos del usuario:', error);
      }
    }    
  },
  modules: {}
});
export default store;
store.subscribe((_, state) => {
  localStorage.setItem('vuexState', JSON.stringify(state));
});