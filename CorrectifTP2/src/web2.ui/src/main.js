import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import store from './store'
import mainOidc from './api/authClient.js'

createApp(App).use(store).use(router).mount('#app')

mainOidc.startup().then(ok => {
    if (ok) {
        const app = createApp(App);
        app.config.globalProperties.$oidc = mainOidc;
        app.use(router).mount('#app');
    }
})