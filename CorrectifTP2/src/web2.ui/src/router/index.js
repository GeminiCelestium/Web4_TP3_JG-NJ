import { createRouter, createWebHistory } from 'vue-router';
import mainOidc from '../api/authClient.js';

const routes = [
  {
    path: '/',
    name: 'Accueil',
    component: () => import('../views/Accueil.vue'),
  },
  {
    path: '/evenements',
    name: 'Évènements',
    component: () => import('../views/Evenements.vue'),
    meta: { authName: mainOidc.authName }, // Cette route est protégée
  },
  {
    path: '/statistique',
    name: 'Statistique',
    component: () => import('../views/Statistique.vue'),
    meta: { authName: mainOidc.authName }, // Cette route est protégée
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/Login.vue'),
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
