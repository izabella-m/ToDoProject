import { createApp } from 'vue';
import App from './App.vue';

// Importando a biblioteca FontAwesome Vue
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import { fas } from '@fortawesome/free-solid-svg-icons'; // Importa todos os ícones sólidos

// Adiciona todos os ícones da biblioteca 'fas' à biblioteca global
library.add(fas);

const app = createApp(App);

// Registra o componente FontAwesomeIcon globalmente
app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#app');
