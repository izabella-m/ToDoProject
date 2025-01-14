<template>
<div>
  <div class="container">
    <div class="itemLeft">Minha lista de tarefas</div>
    
      <div class="itemRight" @click="openDialog">
        <svg-icon class="iconAdd" type="mdi" :path="path"></svg-icon>
        <p>Adicionar item</p>
      </div>

    <div v-if="isDialogOpen" class="dialog-overlay" @click.self="closeDialog">
      <transition name="dialog-fade">
        <div class="dialog-content">
          <h2>Adicionar tarefa</h2>
          <input v-model="message" placeholder="Nome" />
          <input v-model="message" placeholder="Descrição" />
          <select v-model="selected">
            <option disabled value="">Please select one</option>
            <option>A</option>
            <option>B</option>
            <option>C</option>
          </select>
          <button @click="closeDialog">Fechar</button> <!--adaptar para salvar--> 
        </div>
      </transition>
    </div>
  </div>

  <hr>
</div>
</template>

<script setup>
import SvgIcon from '@jamescoyle/vue-icon';
import { mdiPlusCircleOutline } from '@mdi/js'; // Importação da biblioteca de ícones
import { ref } from 'vue';

const isDialogOpen = ref(false);
const path = mdiPlusCircleOutline; // Caminho do ícone SVG

const openDialog = () => {
  isDialogOpen.value = true;
};

const closeDialog = () => {
  isDialogOpen.value = false;
};
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&family=Roboto+Slab:wght@100..900&display=swap');

* {
  font-family: Inter;
}

/*
body {
  background-color: #FCFCFC;
}
*/
.container {
  display: flex;               
  justify-content: space-between; 
  align-items: center;       
  width: 60%;                 
  margin: 0 auto;            
  max-width: 100vw;           
  overflow-x: hidden;          
  box-sizing: border-box;     
  padding: 10px;             
}

.itemLeft {
  padding: 10px;
  font-weight: bold;
}

.itemRight {
  display: flex;
  align-items: center; 
  cursor: pointer;
  font-weight: bold;
}

hr {
  display: flex;              
  width: 60%;                 
  margin: 0 auto;        
}

.dialog-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

/* Estilos para o conteúdo do diálogo */
.dialog-content {
  background-color: white;
  padding: 20px;
  border-radius: 8px;
  width: 300px;
  text-align: center;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  transform: scale(0.8);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
}

.dialog-content > * {
  display: block;
  margin-bottom: 10px;
}

/* Animação de transição para o dialog */
.dialog-fade-enter-active,
.dialog-fade-leave-active {
  transition: opacity 0.3s ease;
}

.dialog-fade-enter, 
.dialog-fade-leave-to /* .dialog-fade-leave-active em versões anteriores do Vue */ {
  opacity: 0;
}

/* Efeito de relevo na transição do dialog */
.dialog-fade-enter-to {
  transform: scale(1);
  box-shadow: 0 6px 20px rgba(0, 0, 0, 0.2);
}

</style>
  