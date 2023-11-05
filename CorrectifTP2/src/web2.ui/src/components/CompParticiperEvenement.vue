<template>
  <div>
    <h1>Participation Evènement ({{ nomEvent }})</h1>
  </div>
  <div>
    <form class="formulaireParticipation">      
      <ul>
        <label>Nom : </label> <input :disabled="true" />
        <label>Prénom : </label> <input :disabled="true" />
      </ul>

      <ul>
        <label>Email : </label> <input :disabled="true" />
        <label>Place(s) : </label> <input />
      </ul>

      <ul>
        <button @click="$router.push(`/evenements`)"><i class="fa-sharp fa-solid fa-angle-left"></i> Retour</button>
        <button @click="validerParticipation"><i class="fa-solid fa-circle-arrow-right"></i> Envoyer</button>
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
    methods: {
      validerParticipation() {

      }
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