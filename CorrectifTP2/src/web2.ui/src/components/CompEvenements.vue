<template>
  <div>
    <h3>{{ titre }}</h3>
    <input type="text" placeholder="Entrer un titre ou une description" v-model="filter.filterString">
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
        <tr :class="{ done: item.done }" v-for="(item, index) in events" :key="index">
          <td>{{ item.Titre }}</td>
          <td>{{ villes.Name  }}</td>
          <td>{{ item.Titre }}</td>
          <td>{{ item.Titre }}</td>
          <td>{{ item.Titre }}</td>
          <td>{{ item.Titre }}</td>
          <td>{{ item.Titre }}</td>
          <td>{{ item.text }}</td>
          <td>            
            <button @click="$router.push(`/events/${item.id}/details`)"><i class="fas fa-users"></i></button>
            <button @click="$router.push(`/events/${item.id}/participer`)"><i class="fas fa-eye"></i></button>
            <button @click="deleteEventApi({id: item.id, 'index': index})"><i class="fas fa-trash"></i></button>
          </td>
        </tr>
      </tbody>
    </table>
    <div>
      <button type="button" @click="filter.pageIndex--" :disabled="filter.pageIndex <= 1">précédent</button>
        page {{ filter.pageIndex }} / {{ pageCount }}
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
        event: "",
        ville: "",
      }
    },
    methods: {
      ...mapActions({
        getEventsApi: 'getEventsApi',
        postEventApi: 'postEventApi',
        getCategoriesApi: 'getCategoriesApi',
        deleteEventApi: 'deleteEventApi',
        updateEventstatusApi: 'updateEventstatusApi'
      }),
      ...mapMutations({
        setEvents: 'setEvents',
        createEvent: 'createEvent',
        deleteEvent: 'deleteEvent',
        toggleEvent: 'updateStatus',
        setCategories: 'setCategories'
      }),
      addEvent() {
        this.postEventApi(this.event).then((data) => {
          this.event = {}
          this.$toast.success(`la tache { id: ${data.id}, text: ${data.text}} a ete ajouté avec success :)`)
        }).catch(() => this.$toast.error(`erreur de communication avec le serveur lors de l'ajout de la tache :(`))
      },
      loadEvents(){
        this.getEventsApi(this.filter).then(data => this.pageCount = data.pageCount).catch( () => this.$toast.error(`erreur de communication avec le serveur lors du chargement des taches :(`))
      }
    },
    computed: {
      ...mapState({ Events: 'Events', categories: 'categories' }),
      ...mapGetters({ progress: 'getProgress' })
    },
    created() {
      this.loadEvents()
      this.getCategoriesApi().catch( () => this.$toast.error(`erreur de communication avec le serveur lors du chargement des categories :(`))
    },
    watch:{
      'filter.filterString'(){
        this.filter.pageIndex = 1
        this.loadEvents()
      }
    }
  };

</script>

<style scoped>
.done {
  background: rgb(7, 240, 7);
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
</style>
