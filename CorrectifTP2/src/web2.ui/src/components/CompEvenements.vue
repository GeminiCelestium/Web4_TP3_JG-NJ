<template>
  <div>
    <h3>{{ titre }}</h3>
    <ul>
      <input style="width: 300px;" type="text" placeholder="Entrer un titre ou une description" v-model="texteRecherche">
      |
      <button @click="rechercher">
        <i class="fas fa-search"></i>
      </button>
    </ul>
    <table>
      <thead>
        <th scope="col">Titre</th>
        <th scope="col">Ville</th>
        <th scope="col">Nbr Participation(s)</th>
        <th scope="col">Catégories</th>
        <th scope="col">Prix</th>
        <th scope="col">Date</th>
        <th scope="col">Action</th>
      </thead>
      <tbody>
        <tr v-for="(event, index) in events" :key="index">
          <td>{{ event.titre }}</td>
          <td>
            {{ getCityName(event.villeID) }}
          </td>
          <td>
            <span v-for="participation in participationsFiltrees" :key="participation.ID">
              {{ participation.Count() }}
            </span>
          </td>
          <td>
            {{ getCategoryName(event.categoryIDs) }}
          </td>
          <td>
            <span v-if="event.prix === 0" :class="{ gratuit: event.prix === 0 }">Gratuit</span>
             <span v-else>{{ event.prix }}</span>
            </td>
          <td>{{ event.dateDebut }}</td>
          <td>
            <button @click="$router.push({name: 'detailsEvent', params:{ id : event.id}})"><i class="fas fa-users"></i></button> |
            <button @click="$router.push(`/evenements/${event.id}/details`)"><i class="fas fa-eye"></i></button> |
            <button @click="deleteEventApi({ id: event.id, 'index': index })"><i class="fas fa-trash"></i></button>
          </td>
        </tr>
      </tbody>
    </table><br />
    <div>
      <button type="button" @click="filter.pageIndex--" :disabled="filter.pageIndex <= 1">Précédent</button>
      Page {{ filter.pageIndex }} / {{ pageCount }}
      <button type="button" @click="filter.pageIndex++" :disabled="filter.pageIndex === pageCount">Suivant</button>
    </div>
  </div>
</template>

<script>
import { mapState, mapGetters, mapMutations, mapActions } from 'vuex'

export default {
  name: "CompEvenements",
  data() {
    return {

      event: {},
      categorie: {},
      ville: {},
      filter: {
        filterString: '',
        pageIndex: 1,
        pageSize: 10,
      },
      pageCount: 5,

      texteRecherche: "",

    };

  },
  methods: {
    ...mapActions({
      getCategoriesApi: 'getCategoriesApi',
      getEventsApi: 'getEventsApi',
      getVillesApi: 'getVillesApi',
      deleteEventApi: 'deleteEventApi',
    }),
    ...mapMutations({
      setCategories: 'setCategories',
      setEvents: 'setEvents',
      setVille: 'setVilles',
      deleteEvent: 'deleteEvent',
    }),
    getCategoryName(categoryID) {
      if (this.categories[categoryID]) {
        return this.categories[categoryID].name;
      } else {
        return 'Category Not Found';
      }
    },
    getCityName(villeID) {
      if (this.villes[villeID]) {
        return this.villes[villeID].name;
      } else {
        return 'City Not Found';
      }
    },
    loadEvents() {
      this.getEventsApi(this.filter)
        .then(data => {
          this.pageCount = data.pageCount;
          console.log(data)
        })
        .catch(error => {
          console.error("Error loading events:", error);
          this.$toast.error(`Erreur de communication avec le serveur lors du chargement des événements :(`);
        });
    },
    rechercher() {
      this.filter.filterString = this.texteRecherche
      this.filter.pageIndex = 1
      this.loadEvents()
    },

  },
  computed: {
    ...mapState({ events: 'events', categories: 'categories', villes: 'villes', participations: 'participations' }),
    ...mapGetters({}),
    villesFiltrees() {
      return this.villes.filter(ville => ville.ID === this.event.event.id);
    },
    participationsFiltrees() {
      if (!this.participations || this.participations.length === 0) {
        return 0;
      }
      else {
        return this.participations.filter(participation => participation.evenementId === this.event.ID);
      }
    },
    categoriesFiltrees() {
      return this.categories.filter(categorie => categorie.ID === this.event.id);
    },
  },
  created() {
    this.loadEvents()
    this.getCategoriesApi().catch(() => this.$toast.error("erreur de communication avec le serveur lors du chargement des categories :("))
    this.getVillesApi().catch(() => this.$toast.error("erreur de communication avec le serveur lors du chargement des villes :("))
  },
  watch: {
    'filter.filterString'() {
      this.filter.pageIndex = 1
      this.loadEvents()

    }
  }
}

</script>

<style scoped>
.gratuit {
  color: lightgreen;
}

button,
[aria-label] {
  cursor: pointer;
}

table {
  font-family: Arial, sans-serif;
  border: 1px solid;
  border-collapse: collapse;
  width: 100%;
}

th {
  background-color: #f8f8f8;
  padding: 5px;
}

td {
  border: 1px solid;
  padding: 5px;
}
span {
  color: black;
  font-family: Arial, sans-serif;
  font-weight: normal;
}

</style>