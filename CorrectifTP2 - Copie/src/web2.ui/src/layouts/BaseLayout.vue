<template>
    <div class="container">        
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0-beta3/css/all.min.css">
      <header>
        <slot name="header">
          <nav>
            <div>
              <router-link to="/"><i class="fas fa-home"></i> Accueil</router-link>
              <router-link to="/todos">Evènements</router-link>
            </div>
            <div>
              <router-link to="/login/coubalde@email.com"><i class="fas fa-key"></i> Login</router-link>
            </div>
          </nav>
        </slot>
      </header>
      <main>
        <slot></slot>
      </main>
    </div>
  </template>
  
  <style>
  nav {
    padding: 15px;
    display: flex;
    background: #bbc5c9;
    font-size: x-large;
    justify-content: space-between;
  }
  
  nav a {
    font-weight: bold;
    color: #2c3e50;
    margin: 10px;
    text-decoration: none;
  }
  
  nav a.router-link-exact-active {
    color: #099455;
  }
  main {
    min-height: 300px;
  }
  footer {
    background: #d2e7e7;
  }
  </style>