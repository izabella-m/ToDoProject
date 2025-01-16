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
          @input="searchTask"
        />     
       </div>
    </div>
    
    <div class="itemRight" @click="openDialog">
      <font-awesome-icon class="iconAdd" :icon="['fas', 'plus']" />
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

  <!--TESTE-->
  <div>
    <h1>Lista de Tarefas</h1>
    <ul v-if="tasks.length">
      <li v-for="task in tasks" :key="task.id">
        {{ task.title }}
      </li>
    </ul>
    <p v-else>Carregando tarefas...</p>
  </div>
</div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue';
import CardTaskComponent from '/src/components/CardTaskComponent.vue';
import { getTasks, getTaskById } from '../services/taskService'; 

const tasks = ref([]); // Armazena as tarefas
const searchQuery = ref(""); // Campo de busca

const filteredTasks = computed(() => {
  if (!Array.isArray(tasks.value)) {
    return []; // Retorna um array vazio se tasks.value não for um array
  }
  const query = searchQuery.value.toLowerCase();
  return tasks.value.filter((task) => {
    return (
      task.title.toLowerCase().includes(query) || // Busca no título
      String(task.id).includes(query)             // Busca no ID
    );
  });
});

const loadTasks = async () => {
    try {
        const response = await getTasks();
        console.log(response); 
        if (Array.isArray(response.dados)) {
            tasks.value = response.dados;
        } else {
            console.error("A resposta não contém um array em 'dados':", response);
        }
    } catch (error) {
        console.error("Erro ao carregar tarefas:", error);
    }
};

const searchTask = async () => {
  const query = searchQuery.value.trim();
  
  if (!query) {
    // Se o campo de busca estiver vazio, carrega todas as tarefas
    loadTasks();
  } else {
    // Verifica se a pesquisa é por ID
    const id = parseInt(query, 10);
    if (!isNaN(id)) {
      // Se for um número (ID), busca pela tarefa pelo ID
      try {
        const task = await getTaskById(id);
        tasks.value = task ? [task] : []; // Atualiza tarefas com o item encontrado ou vazio
      } catch (error) {
        console.error(`Erro ao buscar tarefa com ID ${id}:`, error);
        tasks.value = []; // Limpa se erro ocorrer
      }
    } else {
      // Caso contrário, faz a busca por todas as tarefas
      loadTasks();
    }
  }
};

onMounted(() => {
  loadTasks(); // Carrega as tarefas ao montar o componente
});

// Filtra tarefas com base na busca (JAVASCRIPT)
// const filteredTasks = computed(() => {
//     if (!Array.isArray(tasks.value)) {
//         return []; // Retorna um array vazio se tasks.value não for um array
//     }
//     const query = searchQuery.value.toLowerCase();
//     return tasks.value.filter((task) => {
//         return (
//             task.title.toLowerCase().includes(query) || // Busca no título
//             String(task.id).includes(query)             // Busca no ID
//         );
//     });
// });

const isDialogOpen = ref(false);
const nameTask = ref('');
const descriptionTask = ref('');
const statusTask = ref('Não iniciado');
const isFormSubmitted = ref(false); // Para controlar se o formulário foi submetido

// Mock tasks
// const tasks = ref([
//   { id: 1, title: "Comprar mantimentos", status: "Não iniciado", description: "Lista dcxdeswaaaaaaaaaaaaa cfedwhiygbfcvrewdpiy9obg;op9frd  fweiu9freh[e compras para o mercado" },
//   { id: 2, title: "Ler um livro", status: "Em andamento", description: "Livro de ficçãco científica" },
//   { id: 3, title: "Fazer exercícios", status: "Concluído", description: "Treino de academia" },
// ]);


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
}

.verticalBar {
  width: 4px;
  height: 40px;
  background-color: #f2f2f2;
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
  height: 40px;
  padding: 0px 0px 0px 40px;
  font-size: 16px;
  background-color: #f9f9f9;
  border: 1px solid #f2f2f2;
  border-radius: 12px;
  outline: none;
  transition: border-color 0.3s ease;
}

.iconMagnify {
  position: absolute;
  left: 10px;
  font-size: 18px;
  color: #aaa;
}

.itemLeft {
  display: flex;
  align-items: center; 
}

.itemRight {
  display: flex;
  align-items: center; 
  cursor: pointer;
  background-color: #01b894;
  border-radius: 16px;
  height: 40px;
  padding-left: 4px;
  padding-right: 20px;
}

.itemRight p {
  color: white;
  font-size: 16px;
  font-weight: 500;
}
.iconAdd {
  color: white;
  font-size: 16px;
  margin: 8px;
}

hr {
  display: flex;              
  width: 60%;                 
  margin: 0 auto;        
  border: 1px solid;
  color: #f2f2f2;
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
  padding: 5px;
  border-radius: 20px;
  width: 300px;
  height: 400px;
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
  padding-bottom: 20px;
  font-size: 20px;
}

.inputTitleField,
.inputDescriptionField,
select {
  width: 90%; 
  margin-bottom: 15px; 
  padding: 10px; 
  border: 1px solid #ccc;
  background-color: #f2f2f2;
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
  width: 90%;
  height: 40px;
  padding: 10px;
  margin-top: 15px;
  background-color: #01b894;
  color: white;
  border: none;
  border-radius: 14px;
  cursor: pointer;
  font-size: 16px;
  font-weight: 500;
}

.buttonCancel {
    background: none;
    border: none;
    color: inherit; 
    font: inherit;
    padding: 0; 
    margin-top: 15px; 
    margin-bottom: 15px;
    cursor: pointer; 
    color: #ccc;
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
  