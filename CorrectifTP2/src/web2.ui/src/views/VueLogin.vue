<template>
  <span login()></span>
</template>

<script>
  import Login from '@/components/CompLogin.vue'

  export default {
    name: 'VueLogin',
    components: {
      Login
    },
    methods: {
    login() {
      this.$oidc.signIn();
    },
  },
  }
</script>