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
    name: 'evenement',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '@/views/VueEvenements.vue'),
    children: [
      {
        path: '',
        name: 'listeEvent',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompEvenements.vue')
      },
      {
        path: ':id/participer',
        name: 'participerEvent',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompParticiperEvenement.vue')
      },
      {
        path: ':id/details',
        name: 'detailsEvent',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompDetailsEvenement.vue')
      },
      
    ]    
  },
  {
    path: '/statistiques',
    name: 'statistiques',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '@/components/CompStatistiques.vue'),
  },
  {
    path: '/login',
    name: 'login',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '@/components/CompLogin'),
    children: [
      {
        path: '/profile',
        name: 'profile',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompProfile.vue')
      },      
    ] 
  },
  {
    path: '/profile',
    name: 'profile',
    // route level code-splitting
    // this generates a separate chunk (about.[hash].js) for this route
    // which is lazy-loaded when the route is visited.
    component: () => import(/* webpackChunkName: "about" */ '@/components/CompProfile.vue'),
  },// Existe seulement pour pouvoir voir la page malgré le fait que Login ne fonctionne pas totalement.
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

mainOidc.useRouter(router);

export default router