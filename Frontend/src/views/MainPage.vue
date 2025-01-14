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
          <h2 class="titleDialog">Adicionar tarefa</h2>
          <input class="inputTitleField" v-model="nameTask" placeholder="Nome" />
          <p v-if="!nameTask && isFormSubmitted" class="hintText">Digite o nome da tarefa no campo de título</p>
          <textarea class="inputDescriptionField" v-model="descriptionTask" placeholder="Descrição"></textarea>
          <select v-model="statusTask">
            <option disabled value="">Selecione o status da atividade</option>
            <option>Não iniciado</option>
            <option>Em andamento</option>
            <option>Concluido</option>
          </select>
          <p v-if="!statusTask && isFormSubmitted" class="hintText">Selecione o status da atividade</p>
          <button class="buttonSave" @click="closeAndSaveDialog">Salvar</button> <!--adaptar para salvar--> 
          <button class="buttonCancel"  @click="closeDialog">cancelar</button>
        </div>
      </transition>
    </div>
  </div>
  <hr>

  <ul>
    <li v-for="(task, index) in tasks" :key="index">
      <h3>{{ task.title }}</h3>
      <p>{{ task.description }}</p>
      <p>Status: {{ task.status }}</p>
    </li>
  </ul>
</div>
</template>

<script setup>
import SvgIcon from '@jamescoyle/vue-icon';
import { mdiPlusCircleOutline } from '@mdi/js'; // Importação da biblioteca de ícones
import { ref } from 'vue';

// Estado do diálogo
const isDialogOpen = ref(false);

// Lista de tarefas
const tasks = ref([]);

// Dados da tarefa atual
const nameTask = ref('');
const descriptionTask = ref('');
const statusTask = ref('');
const isFormSubmitted = ref(false); // Para controlar se o formulário foi submetido
const path = mdiPlusCircleOutline; // Caminho do ícone SVG

const openDialog = () => {
  isDialogOpen.value = true;
};

const closeAndSaveDialog = () => {
  // Adicionar a tarefa à lista
  isFormSubmitted.value = true; 
  if (nameTask.value && statusTask.value) {
    tasks.value.push({
      title: nameTask.value,
      description: descriptionTask.value,
      status: statusTask.value,
    });
    resetForm(); // Reseta o formulário após salvar
    isDialogOpen.value = false;
  }
};

const closeDialog = () => {
  isDialogOpen.value = false;
  resetForm(); // Reseta o formulário ao fechar o diálogo
}
//Limpar os campos
const resetForm = () => {
  nameTask.value = "";
  descriptionTask.value = "";
  statusTask.value = "";
  isFormSubmitted.value = false;
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
  display: flex;
  flex-direction: column; 
  align-items: center; 
  background-color: white;
  padding: 20px;
  border-radius: 20px;
  width: 300px;
  height: 350px;
  text-align: center;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  transform: scale(0.8);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
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

.titleDialog {
  padding-bottom: 25px;
}

.inputTitleField,
.inputDescriptionField,
select {
  width: 80%; 
  margin-bottom: 15px; 
  padding: 10px; 
  border: 1px solid #ccc;
  border-radius: 14px; 
  box-sizing: border-box; 
}
.hintText {
  margin: 0;
  margin-top: -13px;
  padding: 0;
  color: red;
  font-size: 10px;
  text-align: start;
  
}
.buttonSave {
  width: 80%;
  padding: 10px;
  margin-top: 15px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 14px;
  cursor: pointer;
  font-size: 14px;
}

.buttonCancel {
    background: none;
    border: none;
    color: inherit; 
    font: inherit;
    padding: 0; 
    margin-top: 15px; 
    cursor: pointer; 
    color: #e75e54;
}

</style>
  