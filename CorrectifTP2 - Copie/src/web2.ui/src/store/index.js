import { createStore } from 'vuex'

export default createStore({
  strict: true,
  state: {
    evenements: []
  },
  getters: {
    getProgress(state) {
      return (state.evenements.filter(t => t.done).length / state.evenements.length) * 100
    }
  },
  mutations: {
    setEvenements(state, evenements) {
      state.evenements = evenements
    },
    createEvenement(state, evenement) {
      state.evenements.push(evenement)
    },
    deleteEvenement(state, index) {
      state.evenements.splice(index, 1)
    },
    updateStatus(state, index) {
      const evenement = state.evenements[index]
      evenement.done = !evenement.done
    }
  },
  actions: {
  },
  modules: {
  }
})