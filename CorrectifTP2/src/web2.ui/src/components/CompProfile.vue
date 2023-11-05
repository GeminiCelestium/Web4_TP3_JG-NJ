<template>
  <div>
    <h1>Profile Utilisateur</h1>
  </div>
  <div>
    <form class="formulaireParticipation">      
      <ul>
        <label>Username : </label> <input :disabled="true" />
      </ul>

      <ul>
        <label>Role(s) : </label> <input :disabled="true" />
      </ul>

      <ul>
        <button @click="logout"><i class="fa-solid fa-right-from-bracket"></i> Logout</button>
      </ul>           
    </form>
  </div>
</template>
      
<script>
  
  
  export default {
    name: 'CompProfile',
    methods: {
      logout() {
        this.$oidc.signOut();
      },
    }
  };
</script>

<style>
  .formulaireParticipation {
    text-align: center;    
    font-size: 18px;
    line-height: 1.5;
    margin: 0 auto;
    max-width: 700px;
    min-width: 500px;
    padding: 20px;
    background-color: #ffffff;
    border: 1px solid #494949;
    border-radius: 5px;
  }
  .formulaireParticipation ul {
    list-style: none;
  }
  .formulaireParticipation ul > li {
    margin-bottom: 10px;
  }
  .formulaireParticipation input {
    margin-right: 10px;
  }
  .formulaireParticipation button {
    margin-top: 15px;
    margin-right: 20px;
    text-align: right;
  }
</style>