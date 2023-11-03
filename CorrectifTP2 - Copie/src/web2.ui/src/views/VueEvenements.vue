<template>
  <div>
    <h1>Évènements</h1>
    <ul>
      <li v-for="event in events" :key="event.id">
        {{ event.title }}
      </li>
    </ul>
  </div>
</template>
  
<script>
  export default {
    name: 'VueEvenements',
    data() {
      return {
        events: [], // Remplissez cette liste avec les données des événements depuis votre API
      };
    },
    created() {
      // Chargez les données des événements depuis votre API ici
      // Vous devrez peut-être effectuer une requête HTTP à votre API avec Axios.
    },
  };
</script>
  