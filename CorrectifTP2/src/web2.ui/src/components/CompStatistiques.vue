<template>
  <div>
    <h1>Statistique de la plateforme</h1>
    <p>
      Contenu de la page de statistiques.
      Normalement, seulement disponible en tant qu'Admin mais l'OIDC ne fonctione pas.
    </p>
  </div>
</template>
    
<script>


  export default {
    name: 'CompStatistiques',
    /*mounted() {
      if (!this.$oidc.isAuthenticated) {
        this.$oidc.signIn(); 
      }
    },*/
  };
</script>
    