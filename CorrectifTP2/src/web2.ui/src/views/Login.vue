<template>
    <div>
      <h1>Login</h1>
      <button @click="login">Se connecter</button>
    </div>
  </template>
  
  <script>
  export default {
    methods: {
      login() {
        // Utilisez la méthode de connexion OIDC appropriée, par exemple :
        this.$oidc.signIn();
      },
    },
  };
  </script>
  