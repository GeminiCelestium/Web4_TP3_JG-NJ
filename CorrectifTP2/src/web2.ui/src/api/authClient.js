import { createOidcAuth, SignInType, LogLevel } from 'vue-oidc-client/vue3';
import VueRouter from 'vue-router';

const appRootUrl = 'http://localhost:8080/';

// Créez l'instance OIDC
const mainOidc = createOidcAuth('vuejs', SignInType.Popup, appRootUrl, {
  authority: 'https://localhost:5001/',
  client_id: 'web2_ui',
  response_type: 'code',
  scope: 'openid profile web2ApiScope',
},
console,
LogLevel.Debug);

// Créez votre routeur Vue
const router = new VueRouter({
  // Configurez votre routeur ici
});

// Enregistrez le routeur dans l'instance OIDC
mainOidc.useRouter(router);

export default mainOidc;
