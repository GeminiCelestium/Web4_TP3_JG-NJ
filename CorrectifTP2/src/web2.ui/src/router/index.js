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
        name: 'eventList',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompEvenements.vue')
      },
      {
        path: ':id/details',
        name: 'event',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompDetailsEvenement.vue')
      },
      {
        path: ':id/participer',
        name: 'event',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../components/CompParticiperEvenement.vue')
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
    /*meta: {
      authName: mainOidc.authName
    }*/
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

mainOidc.useRouter(router);

export default router