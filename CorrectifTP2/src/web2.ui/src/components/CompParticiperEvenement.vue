<template>
  <div>
    <h1>Participation Evènement ({{ nomEvent }})</h1>
  </div>
  <div>
    <form>      
      <ul>
        <label>Nom : </label> <input />
        <label>Prénom : </label> <input />
      </ul>

      <ul>
        <label>Email : </label> <input />
        <label>Places : </label> <input />
      </ul>

      <ul>
        <button><i class="fa-sharp fa-solid fa-angle-left"></i> Retour</button>
        <button><i class="fa-solid fa-circle-arrow-right"></i> Envoyer</button>
      </ul>           
    </form>
  </div>
</template>
      
<script>
  
  
  export default {
    name: 'CompParticiperEvenement',
    props: {
      nomEvent: String,
    },
  };
</script>

<style>
  form {
    border-radius: 10px;
    border-color: black;
  }
  form > ul > input {
    margin-right: 10px;
  }
  form > ul >button {
    margin-right: 20px;
  }
</style>