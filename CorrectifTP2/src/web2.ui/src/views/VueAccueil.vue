<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <Accueil msg="Welcome to Your Vue.js App"/>
  </div>
</template>

<script>
  import Accueil from '@/components/CompAccueil.vue'

  export default {
    name: 'HomeView',
    components: {
      Accueil
    }
  }
</script>
