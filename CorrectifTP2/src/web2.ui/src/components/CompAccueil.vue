<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    
    <h3>Présentation du TP3</h3><br/>
    <p>
      Ce projet permet d'afficher une liste d'évènements.
      Il y a une optionde recherche qui permet de directement trouvé un certain évènement selon le titre ou la description de celui-ci.
      L'utilisateur peut se connecter et s'inscrire aux différents évènements en cliquant sur le deuxième symbole de la colonne Action.
      De plus, il peut voir les détails complet de ceux-ci en cliquant sur le premier symbole de Action.
      Enfin, si l'utilisateur a un rôle particulier (Admin ou Manager), il a accès à plus d'options.
      Comme par exemple, de visualiser les statistiques de chaque évènement ou de supprimer un évènement sélectionné dans la liste.
    </p>
    <h3>Participants</h3>
    <ul>
      <li><h4 style="text-decoration: underline;">Jason Gravel</h4></li>
      <li><h4 style="text-decoration: underline;">Nathan Joseph</h4></li>
    </ul>
    <ul>
      <li>        
        <img alt="Image Jason" src="../assets/jason.gif">
      </li>
      <li>        
        <img alt="Portrait Nathan" src="../assets/nathan.avif">
      </li>
    </ul>        
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
p {
  text-align: justify;
  padding: 5px;
  margin: 0 auto;
  width: 1300px;
  height: 100px;
  border: 1px solid black;
}
h3 {
  margin: 40px 0 0;
}
h4 {
  margin-right: 290px;
  margin-left: 190px;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>