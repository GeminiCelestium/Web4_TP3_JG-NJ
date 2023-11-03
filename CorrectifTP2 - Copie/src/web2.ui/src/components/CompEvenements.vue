<template>
    <div>
      <h3>{{ titre }}</h3>
      <input type="text" placeholder="entrer une tache" v-model="event.text" />
      <select name="" id="category" v-model="event.category">
        <option v-for="(category, index) in categories" :key="index" :value="category">{{ category }}</option>
      </select>
      <button @click="addEvent">Add</button><br />
      <table>
        <thead>
          <th>Done?</th>
          <th>Name: <input type="text" placeholder="filtre sur le titre" v-model="filter.filterString"></th>
          <th>Action</th>
        </thead>
        <tbody>
          <tr :class="{ done: item.done }" v-for="(item, index) in Events" :key="index">
            <td><input type="checkbox" :checked="item.done" @click="updateEventstatusApi({event: item, 'index': index})"></td>
            <td>{{ item.text }}</td>
            <td><button @click="deleteEventApi({id: item.id, 'index': index})"><i class="fa fa-trash"></i></button>
              <button @click="$router.push(`/Events/${item.id}/edit`)"><i class="fa">Edit</i></button>
              <button @click="$router.push(`/Events/${item.id}/view`)"><i class="fa">View</i></button>
            </td>
          </tr>
        </tbody>
      </table>
      <div v-if="this.Events.length > 0"> % de progression: {{ progress }}</div>
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
            event: {},
        }
    },
    methods: {
    ...mapActions({getEventsApi: 'getEventsApi', postEventApi: 'postEventApi', getCategoriesApi: 'getCategoriesApi', deleteEventApi: 'deleteEventApi', updateEventstatusApi: 'updateEventstatusApi'}),
    ...mapMutations({ setEvents: 'setEvents', createEvent: 'createEvent', deleteEvent: 'deleteEvent', toggleEvent: 'updateStatus', setCategories: 'setCategories' }),
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
