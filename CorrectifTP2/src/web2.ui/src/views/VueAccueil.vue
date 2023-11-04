<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <Accueil msg="Bienvenue dans notre projet Vue.js"/>
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
