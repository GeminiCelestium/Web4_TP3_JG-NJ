<template>
  <div>
    <h3>{{ titre }}</h3>
    <input :class="{inputRecherche}" type="text" placeholder="Entrer un titre ou une description" v-model="filter.filterString"><br/>
    <table>
      <thead>
        <th>Titre</th>
        <th>Ville</th>
        <th>Nbr Participation(s)</th>
        <th>Catégories</th>
        <th>Prix</th>
        <th>Date</th>
        <th>Action</th>
      </thead>
      <tbody>
        <tr v-for="(item, index) in events" :key="index">
          <td>{{ item.Titre }}</td>
          <td>
            <span v-for="ville in villesFiltrees" :key="ville.ID">
              {{ ville.Name  }}
            </span>            
          </td>
          <td>
            <span v-for="participation in participationsFiltrees" :key="participation.ID">
              {{ participation.Count() }}
            </span>
          </td>
          <td>
            <span v-for="categorie in categoriesFiltrees" :key="categorie.ID">
              {{ categories.Name }}
            </span>
          </td>
          <td>{{ item.Prix }}</td>
          <td>{{ item.DateDebut }}</td>
          <td>            
            <button @click="$router.push(`/events/${item.id}/details`)"><i class="fas fa-users"></i></button>
            <button @click="$router.push(`/events/${item.id}/participer`)"><i class="fas fa-eye"></i></button>
            <button @click="deleteEventApi({id: item.id, 'index': index})"><i class="fas fa-trash"></i></button>
          </td>
        </tr>
      </tbody>
    </table><br/>
    <div>
      <button type="button" @click="filter.pageIndex--" :disabled="filter.pageIndex <= 1">précédent</button>
        Page {{ filter.pageIndex }} / {{ pageCount }}
      <button type="button" @click="filter.pageIndex++" :disabled="filter.pageIndex === pageCount">suivant</button>
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
        filter:{
         filterString: '',
          pageIndex : 1,
           pageSize : 10,
         },
         pageCount : 5
      };
      
    },
    methods: {
      ...mapActions({
        getCategories: 'getCategoriesApi',
        getEventsApi: 'getEventsApi',
        deleteEventApi: 'deleteEventApi',
      }),
      ...mapMutations({
        setCategories: 'setCategories',
        setEvents: 'setEvents',
        deleteEvent: 'deleteEvent',
      }),
      loadEvents() {
        this.getEventsApi(this.filter)
        .then(data => {
          this.pageCount = data.pageCount;
        })
        .catch(error => {
          console.error("Error loading events:", error);
          this.$toast.error(`Erreur de communication avec le serveur lors du chargement des événements :(`);
        });
      }
    },    
    computed: {
      ...mapState({ events: 'events', categories: 'categories', villes: 'villes', participations: 'participations' }),
      ...mapGetters({  }),
      villesFiltrees() {
        return this.villes.filter(ville => ville.ID === this.item.VilleID);
      },
      participationsFiltrees() {
        if (!this.participations || this.participations.length === 0) {
          return 0;
        }
        else {
          return this.participations.filter(participation => participation.EvenementId === this.item.ID);
        }        
      },
      categoriesFiltrees() {
        return this.categories.filter(categorie => categorie.ID === this.item.CategoryIDs);
      },
    },
    created() {
      this.loadEvents()
    },
    watch:{
      'filter.filterString'(){
        this.filter.pageIndex = 1
        this.loadEvents()
      }
    }
  }

</script>

<style scoped>
.inputRecherche {
  width: 200px;
}

button,
[aria-label] {
  cursor: pointer;
  background-color: #1C2933;
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
</style>
