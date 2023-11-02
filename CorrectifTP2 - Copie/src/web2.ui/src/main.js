import { createApp } from 'vue'
import App from './App.vue'
import BaseLayout from './layouts/BaseLayout.vue'
import router from './router'
import store from './store'

createApp(App).use(store).use(router).component('BaseLayout', BaseLayout).mount('#app')