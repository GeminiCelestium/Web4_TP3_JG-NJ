import { createStore } from 'vuex'
import httpClientEvent from '@/api/event/httpClientEvent'

export default createStore({
  strict: true,
  state: {
    events: [],
    villes: [],
    categories: [],
  },
  getters: {  
  },
  mutations: {
    setVilles(state, villes) {
      state.villes = villes
    },
    setCategories(state, categories) {
      state.categories = categories
    },
    setEvents(state, events) {
      state.events = events
    },
    setParticipations(state, participations) {
      state.participations = participations
    },
    deleteEvent(state, index) {
      state.events.splice(index, 1)
    },    
  },
  actions: {
    getCategoriesApi(context) {
      return httpClientEvent.get('/api/Categories')
        .then(response => context.commit('setCategories', response.data))
        .catch(error => {
          console.log(error)
          return Promise.reject(error)
        })
    },
    getVillesApi(context) {
      return httpClientEvent.get('/api/Villes')
        .then(response => context.commit('setVilles', response.data))
        .catch(error => {
          console.log(error)
          return Promise.reject(error)
        })
    },
    getParticipationsApi(context) {
      return httpClientEvent.get('/api/Participations')
        .then(response => context.commit('setParticipations', response.data))
        .catch(error => {
          console.log(error)
          return Promise.reject(error)
        })
    },
    getEventsApi(context, requestParams) {
      return httpClientEvent.get('/api/Evenements', {
        params : {
          pageIndex: requestParams?.pageIndex,
          pageSize: requestParams?.pageSize,
          filterString: requestParams?.filterString
        }
      })
        .then(response => {
          context.commit('setEvents', response.data.data)
          return response.data
          
        })
        .catch(error =>{
          console.log(error)
          return Promise.reject(error)
        })
    },
    deleteEventApi(context, params) {
      console.log(params)
      httpClientEvent.delete(`/api/Evenements/${params.id}`)
        .then(context.commit('deleteEvent', params.index))
        .catch(error => {
          console.log(error)
          return Promise.reject(error)
        })
    },
  },
  modules: {
  }
})