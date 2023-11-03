<template>
    <div>
      <h3>{{ titre }}</h3>
      <input type="text" placeholder="entrer une tache" v-model="todo.text" />
      <select name="" id="category" v-model="todo.category">
        <option v-for="(category, index) in categories" :key="index" :value="category">{{ category }}</option>
      </select>
      <button @click="addTodo">Add</button><br />
      <table>
        <thead>
          <th>Done?</th>
          <th>Name: <input type="text" placeholder="filtre sur le titre" v-model="filter.filterString"></th>
          <th>Action</th>
        </thead>
        <tbody>
          <tr :class="{ done: item.done }" v-for="(item, index) in todos" :key="index">
            <td><input type="checkbox" :checked="item.done" @click="updateTodoStatusApi({todo: item, 'index': index})"></td>
            <td>{{ item.text }}</td>
            <td><button @click="deleteTodoApi({id: item.id, 'index': index})"><i class="fa fa-trash"></i></button>
              <button @click="$router.push(`/todos/${item.id}/edit`)"><i class="fa">Edit</i></button>
              <button @click="$router.push(`/todos/${item.id}/view`)"><i class="fa">View</i></button>
            </td>
          </tr>
        </tbody>
      </table>
      <div v-if="this.todos.length > 0"> % de progression: {{ progress }}</div>
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
    ...mapActions({getTodosApi: 'getTodosApi', postTodoApi: 'postTodoApi', getCategoriesApi: 'getCategoriesApi', deleteTodoApi: 'deleteTodoApi', updateTodoStatusApi: 'updateTodoStatusApi'}),
    ...mapMutations({ setTodos: 'setTodos', createTodo: 'createTodo', deleteTodo: 'deleteTodo', toggleTodo: 'updateStatus', setCategories: 'setCategories' }),
    addTodo() {
      this.postTodoApi(this.event).then((data) => {
        this.todo = {}
        this.$toast.success(`la tache { id: ${data.id}, text: ${data.text}} a ete ajouté avec success :)`)
      }).catch(() => this.$toast.error(`erreur de communication avec le serveur lors de l'ajout de la tache :(`))
    },
    loadTodos(){
      this.getTodosApi(this.filter).then(data => this.pageCount = data.pageCount).catch( () => this.$toast.error(`erreur de communication avec le serveur lors du chargement des taches :(`))
    }
  },
  computed: {
    ...mapState({ todos: 'todos', categories: 'categories' }),
    ...mapGetters({ progress: 'getProgress' })
  },
  created() {
    this.loadTodos()
    this.getCategoriesApi().catch( () => this.$toast.error(`erreur de communication avec le serveur lors du chargement des categories :(`))
  },
  watch:{
    'filter.filterString'(){
      this.filter.pageIndex = 1
      this.loadTodos()
    }
  }
};


</script>
