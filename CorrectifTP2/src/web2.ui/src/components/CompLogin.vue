<template>
    <span @click="login" />
</template>
  
<script>
  
    export default {
        name: 'CompLogin',
        methods: {
            login() {
                this.$oidc.signIn();
            },
        },
    }

</script>