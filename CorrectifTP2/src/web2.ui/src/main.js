import { createApp } from 'vue'
import App from './App.vue'
import BaseLayout from './layouts/BaseLayout.vue'
import router from './router'
import store from './store'
import mainOidc from './api/authClient.js'

mainOidc.startup().then(ok => {
    if (ok) {
        const app = createApp(App);
        app.config.globalProperties.$oidc = mainOidc;
        app.use(store).use(router).component('BaseLayout', BaseLayout).mount('#app')
    }
})
