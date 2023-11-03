import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/VueAccueil.vue'
import mainOidc from '../api/authClient.js'

const routes = [
  {
    path: '/',
    name: 'accueil',
    component: HomeView
  },
  {
    path: '/evenements',
    name: 'evenements',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/VueEvenements.vue'),    
  },
  {
    path: '/statistiques',
    name: 'statistiques',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/VueStatistiques.vue'),
  },
  {
    path: '/login:email',
    name: 'login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '../views/VueLogin.vue'),
    meta: {
      authName: mainOidc.authName
    }
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

mainOidc.useRouter(router);

export default router