<template>
  <div class="container">        
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
    <header>
      <slot name="header">
        <nav>
          <div>
            <router-link to="/"><i class="fas fa-home"></i> Accueil</router-link>
            <router-link to="/evenements">Evènements</router-link>
            <router-link v-if="isAdmin" to="/statistiques">Statistiques</router-link>
          </div>
          <div>
            <span @click="isAuthenticated ? logout() : login()">
              <i class="fas" :class="isAuthenticated ? 'fa-sign-out' : 'fa-key'"></i>
              {{ isAuthenticated ? 'Déconnexion' : 'Login' }}
            </span>
            <!--<span @click="login()"><i class="fas fa-key"></i> Login</span>-->
          </div>
        </nav>
      </slot>
    </header>
    <main>
      <slot></slot>
    </main>
  </div>
</template>

<script>
import mainOidc from '@/api/authClient'

export default {
  data() {
    return {
      isAuthenticated: false,
    };
  },
  computed: {
    
  },
  methods: {
    created() {
      this.isAuthenticated = mainOidc.isAuthenticated();
    },
    login() {
      this.$oidc.signIn();
    },    
    isAdmin() {
      mainOidc.userProfile.role === 'admin'
    },    
    isManager() {
      mainOidc.userProfile.role === 'manager'
    },
    logout() {
      this.$oidc.signOut();
    },
    
  },
}

</script>
  
<style>
  nav {
    padding: 15px;
    display: flex;
    background: #1C2933;
    font-size: x-large;
    justify-content: space-between;
  }
  
  nav a, span {
    font-weight: bold;
    color: #75C5FF;
    margin: 10px;
    text-decoration: none;
  }
  
  nav a.router-link-exact-active {
    color: #EFA925;
  }
  main {
    min-height: 300px;
  }
  footer {
    background: #d2e7e7;
  }
</style>