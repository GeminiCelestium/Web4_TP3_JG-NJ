<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <p>
      Voici notre TP3. Nous vous prévenons tout de suite
    </p>
    <h3>Présentation du TP3</h3>
    <p>
      Texte Inutile
    </p>
    <h3>Participants</h3>
    <ul>
      <li><h4>Jason Gravel</h4></li>
      <li><h4>Nathan Joseph</h4></li>
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