import { createStore } from 'vuex'
import httpClient from '@/api/event/httpClient'

export default createStore({
  strict: true,
  state: {
    evenements: [],
    villes: [],
    categories: [],
  },
  getters: {  
  },
  mutations: {
    deleteEvents(state, index) {
      state.evenements.splice(index, 1)
    },
    setEvents(state, events) {
      state.events = events
    },
  },
  actions: {
    getEventsApi(context, requestParams) {
      return httpClient.get('/Events', {
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
  },
  modules: {
  }
})