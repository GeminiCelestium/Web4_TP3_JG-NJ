<template>
  <div>
    <h1>Login</h1>
  </div>
  <div class="login">
      <div>
          <label for="usename">Username : </label>
          <input type="email" name="username" id="username">
      </div><br/>
      <div>
          <label for="password">Password : </label>
          <input type="password" name="password" id="password">
      </div><br/>
      <div><button @click="login">Se connecter</button></div>
  </div>
</template>
    
<script>  
  
  export default {
    name: 'VueLogin',
    methods: {
      login() {
        this.$oidc.signIn();
      },
    },
  };
</script>
    