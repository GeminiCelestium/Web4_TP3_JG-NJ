import { createRouter, createWebHistory } from 'vue-router';
import mainOidc from '../api/authClient.js';

const routes = [
  {
    path: '/',
    name: 'Accueil',
    component: () => import('../views/VueAccueil.vue'),
  },
  {
    path: '/evenements',
    name: 'Évènements',
    component: () => import('../views/VueEvenements.vue'),
    meta: { authName: mainOidc.authName }, // Cette route est protégée
  },
  {
    path: '/statistique',
    name: 'Statistique',
    component: () => import('../views/VueStatistiques.vue'),
    meta: { authName: mainOidc.authName }, // Cette route est protégée
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/VueLogin.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
