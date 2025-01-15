<template>
<div class="mainPage">
  <div class="container">
    <div class="itemLeft">
      <div class="titleHeader">To Do List</div>
      <div class="verticalBar"></div>
      <div class="inputSearchContainer">
        <font-awesome-icon class="iconMagnify" icon="search" />

        <input
          type="text"
          class="inputSearchField"
          placeholder="Pesquisar tarefa"
          v-model="searchQuery"
        />     
       </div>
    </div>
    
    <div class="itemRight" @click="openDialog">
      <svg-icon class="iconAdd" type="mdi" :path="path"></svg-icon>
      <p>Adicionar item</p> 
    </div>

    <div v-if="isDialogOpen" class="dialog-overlay" @click.self="closeDialog">
      <transition name="dialog-fade">
        <div class="dialog-content">
          <h2 class="titleDialog">Adicionar tarefa</h2>

          <input class="inputTitleField" v-model="nameTask" placeholder="Nome"  maxlength="50"/>
          <div class="infoRow">
            <p v-if="!nameTask && isFormSubmitted" class="hintText">
              Digite o nome da tarefa
            </p>
            <span class="limite">{{ nameTask.length }}</span>/50
          </div>

          <textarea class="inputDescriptionField" v-model="descriptionTask" placeholder="Descrição"></textarea>
          <!-- <span class="limite">{{ descriptionTask.length }}</span>/50 -->

          <select v-model="statusTask">
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
  <div class="containerTasks">
    <CardTaskComponent :tasks="filteredTasks" @edit-action="handleEditAction" />
  </div>

  <div class="teste">
  </div>
</div>
</template>

<script setup>
import { ref, computed } from 'vue';
import CardTaskComponent from '/src/components/CardTaskComponent.vue';

// Estado do diálogo
const isDialogOpen = ref(false);

// Dados da tarefa atual
const nameTask = ref('');
const descriptionTask = ref('');
const statusTask = ref('Não iniciado');
const isFormSubmitted = ref(false); // Para controlar se o formulário foi submetido

// Mock tasks
const tasks = ref([
  { id: 1, title: "Comprar mantimentos", status: "Não iniciado", description: "Lista de compras para o mercado" },
  { id: 2, title: "Ler um livro", status: "Em andamento", description: "Livro de ficção científica" },
  { id: 3, title: "Fazer exercícios", status: "Concluído", description: "Treino de academia" },
]);


const searchQuery = ref("");

// Filtro
const filteredTasks = computed(() => {
  const query = searchQuery.value.toLowerCase();
  return tasks.value.filter((task) => {
    return (
      task.title.toLowerCase().includes(query) || // Busca no título
      String(task.id).includes(query)             // Busca no ID
    );
  });
});

const openEditDialog = () => {
  console.log("teste")
}

const openDialog = () => {
  statusTask.value = 'Não iniciado';
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

const handleEditAction = () => { // Importado com emit
  isDialogOpen.value = true; // Update de dados
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

.titleHeader {
  font-weight: bold;
  flex-grow: 1;  /* Faz o título ocupar o espaço restante na linha */
  width: 150px;
  color: ;
}

.verticalBar {
  width: 4px;
  height: 40px;
  background-color: #ccc;
  margin: 10px auto; 
  margin-left: 0px;
  margin-right: 25px;
}

.inputSearchContainer {
  position: relative; 
  display: flex;
  align-items: center;
  width: 100%;
}

.inputSearchField {
  width: 100%;
  padding: 10px 10px 10px 40px;
  font-size: 16px;
  border: 1px solid #ccc;
  border-radius: 12px;
  outline: none;
  transition: border-color 0.3s ease;
}

.inputSearchField:focus {
  border-color: #8e46cd;
}

.iconMagnify {
  position: absolute;
  left: 10px;
  font-size: 18px;
  color: #aaa;
}

.inputSearchContainer:hover .iconMagnify {
  color: #8e46cd; 
}

.itemLeft {
  display: flex;
  align-items: center; 
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
.infoRow {
  display: flex;
  justify-content: space-between; /* Alinha os itens às extremidades */
  align-items: center;
  font-size: 12px;
  min-height: 20px; /* Define uma altura mínima para evitar deslocamentos */
}

.hintText {
  color: red;
  margin: 0;
  white-space: nowrap; /* Impede quebra de linha */
  flex: 1;
}

.limite {
  color: #555;
  margin-left: auto;
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

.containerTasks {
  display: flex;
  justify-content: center; 
  align-items: center;
  width: 60%;
  max-width: 100%; 
  margin: 0 auto; 
  padding: 0; 
  height: auto;
  box-sizing: border-box; 
}

</style>
  