<template>
  <div>
    <h1>Détails Evènement</h1>
    <form>
      <div class="beau-paragraphe">
        <div>
          <label for="titre">Titre </label>
          <input type="text" disabled />
        </div>
        <div>
          <label for="ville">Ville </label>
          <input type="text"  name="prix"  disabled />
          <label for="organisateur">Organisateur </label>
          <input type="text" disabled />                      
        </div>
        <div>
          <label for="dateFin">Categories </label>
          <input type="text"  disabled />
          <label for="prix">Prix </label>
          <input type="text"  disabled />
        </div>
      <div>
        <label for="dateDebut">Date de début</label>
          <input type="text"  disabled />
          <label for="dateFin">Date de fin </label>
          <input type="text" disabled />
      </div>
        <div>
          
          <label for="description">Description </label>
          <textarea  name="description" disabled></textarea>
        </div>
        <div class="validation">
          <button type="button" @click="$router.push(`/evenements`)"><i class="fa-sharp fa-solid fa-angle-left"></i> Retour</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { mapActions, mapMutations } from 'vuex';

export default {
  name: 'CompDetailsEvenement',
  data() {
    return {
      event: {},
    }
  },
  methods: {
    ...mapActions({        
      getEventIDApi: 'getEventIDApi',     
    }),
    ...mapMutations({        
      setEvent: 'setEvent',     
    }),
  },
  created() {
  const eventId = this.$route.params.id;
  console.log(eventId)
  this.getEventIDApi(eventId)
},

  computed: {
 }
};
</script>

<style lang="scss" scoped>
  .beau-paragraphe {
    text-align: justify;
    text-align-last: left;
    font-size: 18px;
    line-height: 1.5;
    margin: 0 auto;
    max-width: 1000px;
    min-width: 800px;
    padding: 20px;
    background-color: #ffffff;
    border: 1px solid #494949;
    border-radius: 5px;
  }

  .sizes {
    .p-inputtext {
      display: block;
      margin-bottom: .5rem;

      &:last-child {
        margin-bottom: 0;
      }
    }
  }

  .p-dropdown {
    width: 14rem;
  }

  .validation {
    text-align: justify;
    text-align-last: right;
  }

  input {
    margin-left: 10px;
    margin-right: 15px;
  }
</style>