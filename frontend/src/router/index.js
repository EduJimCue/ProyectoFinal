import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
  },
  {
    path: '/ProductListing/:category',
    name: 'ProductListing',
    component: () => import('../views/Product/ProductListing.vue')
  },
  {
    path: '/ProductTeamListing',
    name: 'ProductTeamListing',
    component: () => import('../views/Product/ProductTeamListing.vue')
  },
  {
    path: '/ProductDetail/:product',
    name: 'ProductDetail',
    component: () => import('../views/Product/ProductDetail.vue')
  },
  {
    path: '/Teams',
    name: 'Teams',
    component: () => import('../views/Teams/TeamsLogin.vue')
  },
  {
    path: '/myCart',
    name: 'myCart',
    component: () => import('../views/Cart/myCart.vue')
  },
  {
    path: '/myOrders',
    name: 'myOrders',
    component: () => import('../views/User/myOrders.vue')
  },
  {
    path: '/myAccount',
    name: 'myAccount',
    component: () => import('../views/User/MyAccount.vue')
  },
  {
    path: '/editMyData',
    name: 'editMyData',
    component: () => import('../views/User/EditUser.vue')
  },
  {
    path: '/adminMenu',
    name: 'adminMenu',
    component: () => import('../views/Admin/AdminMenu.vue')
  },
  {
    path: '/adminProducts',
    name: 'adminProducts',
    component: () => import('../views/Admin/Products/AdminProducts.vue')
  },
  {
    path: '/addProducts',
    name: 'addProducts',
    component: () => import('../views/Admin/Products/AddProducts.vue')
  },
  {
    path: '/addStock/:productId',
    name: 'addStock',
    component: () => import('../views/Admin/Products/AddStock.vue')
  },
  {
    path: '/editProducts/:productId',
    name: 'editProducts',
    component: () => import('../views/Admin/Products/EditProducts.vue')
  },
  {
    path: '/adminAttributes',
    name: 'adminAttributes',
    component: () => import('../views/Admin/Attributes/AdminAttributes.vue')
  },
  {
    path: '/addAttributes/:attribute',
    name: 'addAttributes',
    component: () => import('../views/Admin/Attributes/AddAttributes.vue')
  },
  {
    path: '/editAttributes/:attribute/:attributeId',
    name: 'editAttributes',
    component: () => import('../views/Admin/Attributes/EditAttributes.vue')
  },
  {
    path: '/adminImages',
    name: 'adminImages',
    component: () => import('../views/Admin/Images/AdminImages.vue')
  },
  {
    path: '/login',
    name: 'login',
    component: () => import('../views/User/UserLogin.vue')
  },
  {
    path: '/signup',
    name: 'signup',
    component: () => import('../views/User/UserSignUp.vue')
  }
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
