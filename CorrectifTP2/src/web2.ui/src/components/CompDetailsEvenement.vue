<template>
  <div>
    <h1>Détail événement</h1>
    <form>
      <div class="beau-paragraphe">
        <div>
          <label for="titre">Titre </label>
          <input type="text" id="titre" name="titre" :value="selectedEvent.titre" disabled />
        </div>
        <div>
          <label for="prix">Ville </label>
          <input type="text" id="prix" name="prix" :value="selectedEvent.ville" disabled />
          <label for="organisateur">Organisateur </label>
          <input type="text" id="organisateur" name="organisateur" :value="selectedEvent.organisateur" disabled />                      
        </div>
        <div>
          <label for="dateFin">Categories </label>
          <input type="text" id="dateFin" name="dateFin" :value="selectedEvent.dateFin" disabled />
          <label for="prix">Prix </label>
          <input type="text" id="prix" name="prix" :value="selectedEvent.prix" disabled />
        </div>
      <div>
        <label for="dateDebut">Date de début</label>
          <input type="text" id="dateDebut" name="dateDebut" :value="selectedEvent.dateDebut" disabled />
          <label for="dateFin">Date de fin </label>
          <input type="text" id="dateFin" name="dateFin" :value="selectedEvent.dateFin" disabled />
      </div>
        <div>
          
          <label for="description">Description </label>
          <textarea id="description" name="description" :value="selectedEvent.description" disabled></textarea>
        </div>
        <div class="validation">
          <button type="button" @click="$router.push(`/evenements`)">Retour</button>
        </div>
      </div>
    </form>
  </div>
</template>

<script>
import { mapActions } from 'vuex'

export default {
  name: 'CompDetailsEvenement',
  data() {
    return {
      selectedEvent: {},
    }
  },
  methods: {
    ...mapActions({        
      getEventsApi: 'getEventsApi',
      deleteEventApi: 'deleteEventApi',
      loadEventDetails: 'loadEventDetails',
    }),
  },
  loadEventDetails(eventId) {
    this.getEventDetailsApi(eventId)
      .then(eventDetails => {
        this.selectedEvent = eventDetails;
      })
      .catch(error => {
        console.error("Error loading event details:", error);
        this.$toast.error(`Erreur de communication avec le serveur lors du chargement des détails de l'événement :(`);
      });
  },
  created() {
  const eventId = this.$route.params.eventId;
  this.loadEventDetails(eventId);
},
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